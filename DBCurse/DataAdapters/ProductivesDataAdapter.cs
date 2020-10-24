using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using DBCurse.ProductiveModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DBCurse.DataAdapters
{
    class ProductivesDataAdapter : DataAdapter
    {
        private ObservableCollection<Productive> Productives;
        public ProductivesDataAdapter(MySqlConnection connection, ObservableCollection<Productive> productives) : base(connection)
            {
            Productives = productives;
        }
        private void Productives_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Productive);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Productive);
                    break;
            }
        }
        private void Productive_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ProjectValue" || e.PropertyName == "ProjectUrgency" || e.PropertyName == "WorkerBusy" || e.PropertyName == "WorkerPerformance" || e.PropertyName == "Priority") this.Update(sender as Productive);
        }
        public void Fill()
        {
            Productives.CollectionChanged -= Productives_CollectionChanged;

            List<Dictionary<string, string>> allProductives = GetQueryResult("select ID, ProjectValue, ProjectUrgency, EmployeeBusy, EmployeePerformance, Priority from Productives;");
            
            foreach (Dictionary<string, string> p in allProductives)
            {
                Productive np = new Productive(long.Parse(p["ID"]), p["ProjectUrgency"], p["ProjectValue"], p["EmployeeBusy"], p["EmployeePerformance"], p["Priority"]);

                np.PropertyChanged += Productive_PropertyChanged;

                Productives.Add(np);
            }
            Productives.CollectionChanged += Productives_CollectionChanged;
        }
        public void Add(Productive productive)
        {
            productive.PropertyChanged += Productive_PropertyChanged;
            productive.Id = InsertRow($"insert into Productives (ProjectValue, ProjectUrgency, EmployeeBusy, EmployeePerformance, Priority) values ('{productive.ProjectValue}', '{productive.ProjectUrgency}', '{productive.WorkerBusy}', '{productive.WorkerPerformance}', '{productive.Priority}');");
        }
        public void Delete(Productive productive)
        {
            Execute($"delete from Productives where Id={productive.Id}");
        }
        public void Update(Productive productive)
        {
            Execute($"update Productives set ProjectValue='{productive.ProjectValue}', ProjectUrgency='{productive.ProjectUrgency}', EmployeeBusy='{productive.WorkerBusy}', EmployeePerformance='{productive.WorkerPerformance}', Priority='{productive.Priority}' where Id={productive.Id};");
        }
    }
}
