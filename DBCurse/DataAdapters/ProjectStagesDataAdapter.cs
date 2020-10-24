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
    class ProjectStagesDataAdapter : DataAdapter
    {
        private ObservableCollection<ProjectStage> ProjectStages;
        public ProjectStagesDataAdapter(MySqlConnection connection, ObservableCollection<ProjectStage> projectStages) : base(connection)
            {
            ProjectStages = projectStages;
        }
        private void ProjectStages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as ProjectStage);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as ProjectStage);
                    break;
            }
        }
        private void ProjectStage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IDProject" || e.PropertyName == "IDStage") this.Update(sender as ProjectStage);
        }
        public void Fill()
        {
            ProjectStages.CollectionChanged -= ProjectStages_CollectionChanged;

            List<Dictionary<string, string>> allProjectStages = GetQueryResult("select ID, IDProject, IDStage from ProjectStages;");

            foreach (Dictionary<string, string> p in allProjectStages)
            {
                ProjectStage np = new ProjectStage(long.Parse(p["ID"]), int.Parse(p["IDProject"]), int.Parse(p["IDStage"]));

                np.PropertyChanged += ProjectStage_PropertyChanged;

                ProjectStages.Add(np);
            }
            ProjectStages.CollectionChanged += ProjectStages_CollectionChanged;
        }
        public void Add(ProjectStage projectStage)
        {
            projectStage.PropertyChanged += ProjectStage_PropertyChanged;
            projectStage.Id = InsertRow($"insert into ProjectStages (IDProject, IDStage) values ('{projectStage.IDProject}', '{projectStage.IDStage}');");
        }
        public void Delete(ProjectStage projectStage)
        {
            Execute($"delete from ProjectStages where Id={projectStage.Id}");
        }
        public void Update(ProjectStage projectStage)
        {
            Execute($"update ProjectStages set IDProject='{projectStage.IDProject}', IDStage='{projectStage.IDStage}' where Id={projectStage.Id};");
        }
    }
}
