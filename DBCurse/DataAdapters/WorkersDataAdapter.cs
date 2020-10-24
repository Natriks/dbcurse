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
    class WorkersDataAdapter : DataAdapter
    {
        private ObservableCollection<Worker> Workers;
        public WorkersDataAdapter(MySqlConnection connection, ObservableCollection<Worker> workers) : base(connection)
        {
            Workers = workers;
        }
        private void Workers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Worker);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Worker);
                    break;
            }
        }
        private void Worker_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Surname" || e.PropertyName == "Name" || e.PropertyName == "Patronomic" || e.PropertyName == "Birthday" || e.PropertyName == "NamePosition" || e.PropertyName == "HourlyPayment" || e.PropertyName == "PassportNumber" || e.PropertyName == "Email" || e.PropertyName == "Telephone" || e.PropertyName == "Address" || e.PropertyName == "Performance") this.Update(sender as Worker);
        }
        public void Fill()
        {
            Workers.CollectionChanged -= Workers_CollectionChanged;

            List<Dictionary<string, string>> allWorkers = GetQueryResult("select e.ID, e.Surname, e.Name, e.Patronomic, e.Birthday, p.NamePosition, p.HourlyPayment, e.PassportNumber, e.Email, e.Telephone, e.Address, e.Performance from Employees e left join Positions p on e.IDPosition=p.ID;");
            List<Dictionary<string, string>> allWorkersInProjects = GetQueryResult("select IDProject, IDEmployee from ProjectParticipation;");

            foreach (Dictionary<string, string> w in allWorkers)
            {
                Worker nw = new Worker(long.Parse(w["ID"]), w["Name"], w["Surname"], w["Patronomic"], Convert.ToDateTime(w["Birthday"]), w["NamePosition"], int.Parse(w["HourlyPayment"]), int.Parse(w["PassportNumber"]), w["Email"], int.Parse(w["Telephone"]), w["Address"], int.Parse(w["Performance"]));

                nw.PropertyChanged += Worker_PropertyChanged;

                Workers.Add(nw);
            }
            Workers.CollectionChanged += Workers_CollectionChanged;
        }
        public void Add(Worker worker)
        {
            worker.PropertyChanged += Worker_PropertyChanged;
            worker.Id = InsertRow($"insert into Employees (Surname, Name, Patronomic, Birthday, IDPosition, PassportNumber, Email, Telephone, Address, Performance) values ('{worker.Surname}', '{worker.Name}', '{worker.Patronomic}', '{worker.Birthday}', '{GetPositionId(worker.PositionName)}', '{worker.PassportNumber}', '{worker.Email}', '{worker.Telephone}', '{worker.Address}', '{worker.Performance}');");
        }
        public void Delete(Worker worker)
        {
            Execute($"delete from Employees where Id={worker.Id}");
        }
        public void Update(Worker worker)
        {
            Execute($"update Employees set Surname='{worker.Surname}', Name='{worker.Name}', Patronomic='{worker.Patronomic}', Birthday='{worker.Birthday}', IDPosition='{GetPositionId(worker.PositionName)}', PassportNumber='{worker.PassportNumber}', Email='{worker.Email}', Telephone='{worker.Telephone}', Address='{worker.Address}', Performance='{worker.Performance}' where Id={worker.Id};");
        }
        private long GetPositionId(string name)
        {
            foreach (Position position in DataModel.Positions)
            {
                if (position.Name == name)
                    return position.Id;
            }
            DataModel.Positions.Add(new Position());
            System.Windows.MessageBox.Show("Должность не существует, создан пустой объект.");
            return DataModel.Workers.Count - 1;
        }
    }
}
