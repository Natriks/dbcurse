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
    class InvestorsDataAdapter : DataAdapter
    {
        private ObservableCollection<Investor> Investors;
        public InvestorsDataAdapter(MySqlConnection connection, ObservableCollection<Investor> investors) : base(connection)
            {
            Investors = investors;
        }
        private void Investors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Investor);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Investor);
                    break;
            }
        }
        private void Investor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name" || e.PropertyName == "Telephone" || e.PropertyName == "Email" || e.PropertyName == "Adress" || e.PropertyName == "Contactperson" || e.PropertyName == "Description") this.Update(sender as Investor);
        }
        public void Fill()
        {
            Investors.CollectionChanged -= Investors_CollectionChanged;

            List<Dictionary<string, string>> allInvestors = GetQueryResult("select ID, Name, Telephone, Email, Address, ContactPerson, Description from Investors;");

            foreach (Dictionary<string, string> i in allInvestors)
            {
                Investor ni = new Investor(long.Parse(i["ID"]), i["Name"], int.Parse(i["Telephone"]), i["Email"], i["Address"], i["ContactPerson"], i["Description"]);

                ni.PropertyChanged += Investor_PropertyChanged;

                Investors.Add(ni);
            }
            Investors.CollectionChanged += Investors_CollectionChanged;
        }
        public void Add(Investor investor)
        {
            investor.PropertyChanged += Investor_PropertyChanged;
            investor.Id = InsertRow($"insert into Investors (Name, Telephone, Email, Address, ContactPerson, Description) values ('{investor.Name}', '{investor.Telephone}', '{investor.Email}', '{investor.Adress}', '{investor.Contactperson}', '{investor.Description}');");
        }
        public void Delete(Investor investor)
        {
            Execute($"delete from Investors where Id={investor.Id}");
        }
        public void Update(Investor investor)
        {
            Execute($"update Investors set Name='{investor.Name}', Telephone='{investor.Telephone}', Email='{investor.Email}', Address='{investor.Adress}', ContactPerson='{investor.Contactperson}', Description='{investor.Description}' where Id={investor.Id};");
        }
    }
}
