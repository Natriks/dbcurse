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
    class ProjectTypesDataAdapter : DataAdapter
    {
        private ObservableCollection<ProjectType> ProjectTypes;
        public ProjectTypesDataAdapter(MySqlConnection connection, ObservableCollection<ProjectType> projectTypes) : base(connection)
        {
            ProjectTypes = projectTypes;
        }
        private void ProjectTypes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as ProjectType);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as ProjectType);
                    break;
            }
        }
        private void ProjectType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name") this.Update(sender as ProjectType);
        }
        public void Fill()
        {
            ProjectTypes.CollectionChanged -= ProjectTypes_CollectionChanged;

            List<Dictionary<string, string>> allProjectTypes = GetQueryResult("select ID, NameType from ProjectTypes;");

            foreach (Dictionary<string, string> t in allProjectTypes)
            {
                ProjectType nw = new ProjectType(long.Parse(t["ID"]), t["NameType"]);

                nw.PropertyChanged += ProjectType_PropertyChanged;

                ProjectTypes.Add(nw);
            }
            ProjectTypes.CollectionChanged += ProjectTypes_CollectionChanged;
        }
        public void Add(ProjectType projectType)
        {
            projectType.PropertyChanged += ProjectType_PropertyChanged;
            projectType.Id = InsertRow($"insert into ProjectTypes (NameType) values ('{projectType.Name}');");
        }
        public void Delete(ProjectType projectType)
        {
            Execute($"delete from ProjectTypes where Id={projectType.Id}");
        }
        public void Update(ProjectType projectType)
        {
            Execute($"update ProjectTypes set NameType='{projectType.Name}' where Id={projectType.Id};");
        }
    }
}
