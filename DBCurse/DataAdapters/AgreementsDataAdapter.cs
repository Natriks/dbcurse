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
    class AgreementsDataAdapter : DataAdapter
    {
        private ObservableCollection<Agreement> Agreements;
        public AgreementsDataAdapter(MySqlConnection connection, ObservableCollection<Agreement> agreements) : base(connection)
        {
            Agreements = agreements;
        }
        private void Agreements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Agreement);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Agreement);
                    break;
            }
        }
        private void Agreement_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ClientName" || e.PropertyName == "Date" || e.PropertyName == "Deadline" || e.PropertyName == "Amount" || e.PropertyName == "Description") this.Update(sender as Agreement);
        }

        public void Fill()
        {
            Agreements.CollectionChanged -= Agreements_CollectionChanged;

            List<Dictionary<string, string>> allAgreements = GetQueryResult("select a.IDAgreement, c.Name, a.Date, a.Deadline, a.Amount, a.Description from Agreements a left join Clients c on a.IdClient=c.ID;");
            
            foreach (Dictionary<string, string> a in allAgreements)
            {
                Agreement na = new Agreement(long.Parse(a["IDAgreement"]), a["Name"], Convert.ToDateTime(a["Date"]), Convert.ToDateTime(a["Deadline"]), int.Parse(a["Amount"]), a["Description"]);

                na.PropertyChanged += Agreement_PropertyChanged;

                Agreements.Add(na);
            }

            Agreements.CollectionChanged += Agreements_CollectionChanged;
        }
        public void Add(Agreement agreement)
        {
            agreement.PropertyChanged += Agreement_PropertyChanged;
            agreement.Id = InsertRow($"insert into Agreements (IDClient, Date, Deadline, Amount, Description) values ('{GetClientId(agreement.ClientName)}', '{agreement.Date}', '{agreement.Deadline}', '{agreement.Amount}', '{agreement.Description}');");
        }
        public void Delete(Agreement agreement)
        {
            Execute($"delete from Agreements where IDAgreement={agreement.Id}");
        }
        public void Update(Agreement agreement)
        {
            Execute($"update Agreements set IDClient='{GetClientId(agreement.ClientName)}', Date='{agreement.Date}', Deadline='{agreement.Deadline}', Amount='{agreement.Amount}', Description='{agreement.Description}' where IDAgreement={agreement.Id};");
        }
        private long GetClientId(string name)
        {
            foreach (Client c in DataModel.Clients)
            {
                if (c.Name == name)
                    return c.Id;
            }
            return 0;
        }
    }
}
