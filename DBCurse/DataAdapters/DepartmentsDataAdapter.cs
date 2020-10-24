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
    class DepartmentsDataAdapter : DataAdapter
    {
        private ObservableCollection<Department> Departments;
        public DepartmentsDataAdapter(MySqlConnection connection, ObservableCollection<Department> departments) : base(connection)
        {
            Departments = departments;
        }
        private void Departments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Department);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Department);
                    break;
            }
        }
        private void Department_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name" || e.PropertyName == "LeaderName") this.Update(sender as Department);
        }
        public void Fill()
        {
            Departments.CollectionChanged -= Departments_CollectionChanged;

            List<Dictionary<string, string>> allDepartments = GetQueryResult("select d.ID, d.Name, e.Surname from Departments d left join Employees e on d.Leader=e.ID;");
            List<Dictionary<string, string>> allWorkersInDepartments = GetQueryResult("select IDEmployee, IDDepartment from EmployeesInDepartments;");

            foreach (Dictionary<string, string> d in allDepartments)
            {
                Department nd = new Department(long.Parse(d["ID"]), d["Name"], d["Surname"]);

                foreach (Dictionary<string, string> w in allWorkersInDepartments)
                {
                    if (long.Parse(w["IDDepartment"]) == nd.Id)
                    {
                        nd.WorkersCollection.Add(FindWorker(long.Parse(w["IDEmployee"])));
                    }
                }

                nd.PropertyChanged += Department_PropertyChanged;

                Departments.Add(nd);
            }
            Departments.CollectionChanged += Departments_CollectionChanged;
        }
        public void Add(Department department)
        {
            department.PropertyChanged += Department_PropertyChanged;
            department.Id = InsertRow($"insert into Departments (Name, Leader) values ('{department.Name}', '{GetWorkerID(department.LeaderName)}');");
        }
        public void Delete(Department department)
        {
            Execute($"delete from Departments where Id={department.Id}");
        }
        public void Update(Department department)
        {
            Execute($"update Departments set Name='{department.Name}', Leader='{GetWorkerID(department.LeaderName)}' where Id={department.Id};");
        }
        public Worker FindWorker(long id)
        {
            foreach (Worker worker in DataModel.Workers)
            {
                if (worker.Id == id)
                    return worker;
            }
            return null;
        }
        public long GetWorkerID(string surname)
        {
            foreach (Worker worker in DataModel.Workers)
            {
                if (worker.Surname == surname)
                    return worker.Id;
            }
            return -1;
        }
    }
}
