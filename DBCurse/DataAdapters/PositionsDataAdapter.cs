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
    class PositionsDataAdapter : DataAdapter
    {
        private ObservableCollection<Position> Positions;
        public PositionsDataAdapter(MySqlConnection connection, ObservableCollection<Position> positions) : base(connection)
        {
            Positions = positions;
        }
        private void Positions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Position);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Position);
                    break;
            }
        }
        private void Position_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name" || e.PropertyName == "Payment") this.Update(sender as Position);
        }
        public void Fill()
        {
            Positions.CollectionChanged -= Positions_CollectionChanged;

            List<Dictionary<string, string>> allPositions = GetQueryResult("select ID, NamePosition, HourlyPayment from Positions;");

            foreach (Dictionary<string, string> t in allPositions)
            {
                Position nw = new Position(long.Parse(t["ID"]), t["NamePosition"], t["HourlyPayment"]);

                nw.PropertyChanged += Position_PropertyChanged;

                Positions.Add(nw);
            }
            Positions.CollectionChanged += Positions_CollectionChanged;
        }
        public void Add(Position position)
        {
            position.PropertyChanged += Position_PropertyChanged;
            position.Id = InsertRow($"insert into Positions (NamePosition, HourlyPayment) values ('{position.Name}', '{position.Payment}');");
        }
        public void Delete(Position position)
        {
            Execute($"delete from Positions where Id={position.Id}");
        }
        public void Update(Position position)
        {
            Execute($"update Positions set NamePosition='{position.Name}', HourlyPayment='{position.Payment}' where Id={position.Id};");
        }
    }
}
