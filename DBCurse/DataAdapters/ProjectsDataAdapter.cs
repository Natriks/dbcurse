using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using DBCurse.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DBCurse.DataAdapters
{
    class ProjectsDataAdapter : DataAdapter
    {
        private ObservableCollection<Project> Projects;
        public ProjectsDataAdapter(MySqlConnection connection, ObservableCollection<Project> projects) : base(connection)
        {
            Projects = projects;
        }
        private void Projects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Project);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Project);
                    break;
            }
        }
        private void Project_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name" || e.PropertyName == "TypeName" || e.PropertyName == "AgreementID" || e.PropertyName == "ResponsibleName" || e.PropertyName == "StartDate" || e.PropertyName == "EndDate" || e.PropertyName == "Description" || e.PropertyName == "WorkersCollection") this.Update(sender as Project);
        }
        public void Fill()
        {
            Projects.CollectionChanged -= Projects_CollectionChanged;

            List<Dictionary<string, string>> allProjects = GetQueryResult("select p.ID, p.Name, t.NameType, a.IDAgreement, e.Surname, p.StartDate, p.EndDate, p.Description from Projects p left join ProjectTypes t on p.IDType=t.ID left join Agreements a on p.IDAgreement=a.IDAgreement left join Employees e on p.Responsible=e.ID;");
            List<Dictionary<string, string>> allWorkersInProjects = GetQueryResult("select IDProject, IDEmployee from ProjectParticipation;");
            List<Dictionary<string, string>> allStagesInProjects = GetQueryResult("select IDProject, IDStage from ProjectStages;");

            foreach (Dictionary<string, string> p in allProjects)
            {
                Project np = new Project(long.Parse(p["ID"]), p["Name"], p["NameType"], int.Parse(p["IDAgreement"]), p["Surname"], Convert.ToDateTime(p["StartDate"]), Convert.ToDateTime(p["EndDate"]), p["Description"]);

                foreach (Dictionary<string, string> w in allWorkersInProjects)
                {
                    if (long.Parse(w["IDProject"]) == np.Id)
                    {
                        np.WorkersCollection.Add(FindWorker(long.Parse(w["IDEmployee"])));
                    }
                }
                foreach (Dictionary<string, string> s in allStagesInProjects)
                {
                    if (long.Parse(s["IDProject"]) == np.Id)
                    {
                        np.StagesCollection.Add(FindStage(long.Parse(s["IDStage"])));
                    }
                }
                np.PropertyChanged += Project_PropertyChanged;

                Projects.Add(np);
            }
            Projects.CollectionChanged += Projects_CollectionChanged;
        }
        public void Add(Project project)
        {
            project.PropertyChanged += Project_PropertyChanged;
            project.Id = InsertRow($"insert into Projects (Name, IDType, IDAgreement, Responsible, StartDate, EndDate, Description) values ('{project.Name}', '{GetProjectTypeId(project.TypeName)}', '{project.AgreementID}', '{GetWorkerId(project.ResponsibleName)}', '{project.StartDate}', '{project.EndDate}', '{project.Description}');");
        }
        public void Delete(Project project)
        {
            Execute($"delete from Projects where Id={project.Id}");
        }
        public void Update(Project project)
        {
            Execute($"update Projects set Name='{project.Name}', IDType='{GetProjectTypeId(project.TypeName)}', IDAgreement='{project.AgreementID}', Responsible='{GetWorkerId(project.ResponsibleName)}', StartDate='{project.StartDate}', EndDate='{project.EndDate}', Description='{project.Description}' where Id={project.Id};");
        }
        private long GetProjectTypeId(string name)
        {
            foreach (ProjectType pt in DataModel.ProjectTypes)
            {
                if (pt.Name == name)
                    return pt.Id;
            }
            DataModel.ProjectTypes.Add(new ProjectType(DataModel.ProjectTypes.Count, name));
            System.Windows.MessageBox.Show("Тип проекта не существует, создан пустой объект.");
                return DataModel.ProjectTypes.Count - 1;
        }

        private long GetWorkerId(string surname)
        {
            foreach (Worker worker in DataModel.Workers)
            {
                if (worker.Surname == surname)
                    return worker.Id;
            }
            DataModel.Workers.Add(new Worker());
            System.Windows.MessageBox.Show("Работник не существует, создан пустой объект.");
            return DataModel.Workers.Count - 1;
        }
        private Worker FindWorker(long id)
        {
            foreach (Worker worker in DataModel.Workers)
            {
                if (worker.Id == id)
                    return worker;
            }
            return null;
        }
        private Stage FindStage(long id)
        {
            foreach (Stage stage in DataModel.Stages)
            {
                if (stage.Id == id)
                    return stage;
            }
            return null;
        }
    }
}

