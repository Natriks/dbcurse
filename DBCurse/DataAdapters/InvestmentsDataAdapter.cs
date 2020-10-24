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
    class InvestmentsDataAdapter : DataAdapter
    {
        private ObservableCollection<Investment> Investments;
        public InvestmentsDataAdapter(MySqlConnection connection, ObservableCollection<Investment> investments) : base(connection)
        {
            Investments = investments;
        }
        private void Investments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Investment);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Investment);
                    break;
            }
        }
        private void Investment_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "NameInvestor" || e.PropertyName == "Amount") this.Update(sender as Investment);
        }
        public void Fill()
        {
            Investments.CollectionChanged -= Investments_CollectionChanged;

            List<Dictionary<string, string>> allInvestments = GetQueryResult("select i.ID, v.Name, i.Amount from Investments i left join Investors v on i.IDInvestor=v.ID;");

            foreach (Dictionary<string, string> i in allInvestments)
            {
                Investment ni = new Investment(long.Parse(i["ID"]), i["Name"], int.Parse(i["Amount"]));

                ni.PropertyChanged += Investment_PropertyChanged;

                Investments.Add(ni);
            }
            Investments.CollectionChanged += Investments_CollectionChanged;
        }
        public void Add(Investment investment)
        {
            investment.PropertyChanged += Investment_PropertyChanged;
            investment.Id = InsertRow($"insert into Investments (Name, Amount) values ('{investment.NameInvestor}', '{investment.Amount}');");
            //operation.PropertyChanged += Operation_PropertyChanged;
            //operation.Id = InsertRow($"insert into Operations (Name, Amount, Date) values (\"{operation.OperationName}\", \"{operation.Amount}\", \"{operation.Date}\");");
        }
        public void Delete(Investment investment)
        {
            Execute($"delete from Investments where Id={investment.Id}");
        }
        public void Update(Investment investment)
        {
            Execute($"update Investments set Name='{investment.NameInvestor}', Amount='{investment.Amount}' where Id={investment.Id};");
            //Execute($"update Operations set Name=\"{operation.OperationName}\", Amount=\"{operation.Amount}\", Date=\"{operation.Date}\" where Id={operation.Id};");
        }
    }
}

