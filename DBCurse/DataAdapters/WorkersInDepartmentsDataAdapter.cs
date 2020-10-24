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
    class WorkersInDepartmentsDataAdapter:DataAdapter
    {
        private ObservableCollection<WorkersInDepartment> WorkersInDepartments;
        public WorkersInDepartmentsDataAdapter(MySqlConnection connection, ObservableCollection<WorkersInDepartment> workersInDepartments) : base(connection)
        {
            WorkersInDepartments = workersInDepartments;
        }
        private void WorkersInDepartments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as WorkersInDepartment);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as WorkersInDepartment);
                    break;
            }
        }
        private void WorkersInDepartment_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IDDepartment" || e.PropertyName == "IDWorker") this.Update(sender as WorkersInDepartment);
        }
        public void Fill()
        {
            WorkersInDepartments.CollectionChanged -= WorkersInDepartments_CollectionChanged;

            List<Dictionary<string, string>> allWorkersInDepartments = GetQueryResult("select ID, IDEmployee, IDDepartment from EmployeesInDepartments;");

            foreach (Dictionary<string, string> t in allWorkersInDepartments)
            {
                WorkersInDepartment pp = new WorkersInDepartment(long.Parse(t["ID"]), int.Parse(t["IDEmployee"]), int.Parse(t["IDDepartment"]));

                pp.PropertyChanged += WorkersInDepartment_PropertyChanged;

                WorkersInDepartments.Add(pp);
            }
            WorkersInDepartments.CollectionChanged += WorkersInDepartments_CollectionChanged;
        }
        public void Add(WorkersInDepartment workersInDepartment)
        {
            workersInDepartment.PropertyChanged += WorkersInDepartment_PropertyChanged;
            workersInDepartment.Id = InsertRow($"insert into EmployeesInDepartments (IDEmployee, IDDepartment) values ('{workersInDepartment.IDWorker}', '{workersInDepartment.IDDepartment}');");
        }
        public void Delete(WorkersInDepartment workersInDepartment)
        {
            Execute($"delete from EmployeesInDepartments where ID={workersInDepartment.Id}");
        }
        public void Update(WorkersInDepartment workersInDepartment)
        {
            Execute($"update EmployeesInDepartments set IDEmployee='{workersInDepartment.IDWorker}', IDDepartment='{workersInDepartment.IDDepartment}' where ID={workersInDepartment.Id};");
        }
    }
}
