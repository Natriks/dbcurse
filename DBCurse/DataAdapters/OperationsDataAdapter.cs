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
    class OperationsDataAdapter : DataAdapter
    {
        private ObservableCollection<Operation> Operations;
        public OperationsDataAdapter(MySqlConnection connection, ObservableCollection<Operation> operations) : base(connection)
        {
            Operations = operations;
        }
        private void Operations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Operation);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Operation);
                    break;
            }
        }
        private void Operation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "OperationName" || e.PropertyName == "Amount" || e.PropertyName == "Date") this.Update(sender as Operation);
        }
        public void Fill()
        {
            Operations.CollectionChanged -= Operations_CollectionChanged;

            List<Dictionary<string, string>> allOperations = GetQueryResult("select f.ID, o.Name, f.Amount, f.Date from FinancialAccounting f left join OperationTypes o on f.IDOperationType=o.ID;");

            foreach (Dictionary<string, string> o in allOperations)
            {
                Operation no = new Operation(long.Parse(o["ID"]), o["Name"], int.Parse(o["Amount"]), Convert.ToDateTime(o["Date"]));

                no.PropertyChanged += Operation_PropertyChanged;

                Operations.Add(no);
            }
            Operations.CollectionChanged += Operations_CollectionChanged;
        }
        public void Add(Operation operation)
        {
            operation.PropertyChanged += Operation_PropertyChanged;
            operation.Id = InsertRow($"insert into FinancialAccounting (IDOperationType, Amount, Date) values ('{GetOperationIdByName(operation.OperationName)}', '{operation.Amount}', '{operation.Date}');");
        }
        public void Delete(Operation operation)
        {
            Execute($"delete from FinancialAccounting where ID={operation.Id}");
        }
        public void Update(Operation operation)
        {
            Execute($"update FinancialAccounting set IDOperationType='{GetOperationIdByName(operation.OperationName)}', Amount='{operation.Amount}', Date='{operation.Date}' where ID={operation.Id};");
        }
        private long GetOperationIdByName(string name)
        {
            if (name == "Доход")
                return 1;
            if (name == "Расход")
                return 2;
            return 1;
        }
    }
}
