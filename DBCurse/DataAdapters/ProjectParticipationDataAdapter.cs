using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DBCurse.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;


namespace DBCurse.DataAdapters
{
    class ProjectParticipationDataAdapter:DataAdapter
    {
        private ObservableCollection<ProjectParticipation> ProjectParticipations;
        public ProjectParticipationDataAdapter(MySqlConnection connection, ObservableCollection<ProjectParticipation> projectParticipations) : base(connection)
        {
            ProjectParticipations = projectParticipations;
        }
        private void ProjectParticipations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as ProjectParticipation);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as ProjectParticipation);
                    break;
            }
        }
        private void ProjectParticipation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IDProject" || e.PropertyName == "IDEmployee") this.Update(sender as ProjectParticipation);
        }
        public void Fill()
        {
            ProjectParticipations.CollectionChanged -= ProjectParticipations_CollectionChanged;

            List<Dictionary<string, string>> allProjectParticipations = GetQueryResult("select ID, IDProject, IDEmployee from ProjectParticipation;");

            foreach (Dictionary<string, string> t in allProjectParticipations)
            {
                ProjectParticipation pp = new ProjectParticipation(long.Parse(t["ID"]), int.Parse(t["IDProject"]), int.Parse(t["IDEmployee"]));

                    foreach (Worker worker in DataModel.Workers)
                    {
                        if (long.Parse(t["IDEmployee"]) == worker.Id)
                        {
                            worker.ProjectsCollection.Add(FindProject(long.Parse(t["IDProject"])));
                        }
                    }

                pp.PropertyChanged += ProjectParticipation_PropertyChanged;

                ProjectParticipations.Add(pp);
            }
            ProjectParticipations.CollectionChanged += ProjectParticipations_CollectionChanged;
        }
        public void Add(ProjectParticipation projectParticipation)
        {
            projectParticipation.PropertyChanged += ProjectParticipation_PropertyChanged;
            projectParticipation.Id = InsertRow($"insert into ProjectParticipation (IDProject, IDEmployee) values ('{projectParticipation.IDProject}', '{projectParticipation.IDWorker}');");
        }
        public void Delete(ProjectParticipation projectParticipation)
        {
            Execute($"delete from ProjectParticipation where ID={projectParticipation.Id}");
        }
        public void Update(ProjectParticipation projectParticipation)
        {
            Execute($"update ProjectParticipation set IDProject='{projectParticipation.IDProject}', IDAgreement='{projectParticipation.IDWorker}' where Id={projectParticipation.Id};");
        }
        private Project FindProject(long id)
        {
            foreach (Project project in DataModel.Projects)
            {
                if (project.Id == id)
                    return project;
            }
            return null;
        }
    }
}
