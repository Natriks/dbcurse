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
    class StagesDataAdapter : DataAdapter
    {
        private ObservableCollection<Stage> Stages;
        public StagesDataAdapter(MySqlConnection connection, ObservableCollection<Stage> stages) : base(connection)
        {
            Stages = stages;
        }
        private void Stages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Stage);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Stage);
                    break;
            }
        }
        private void Stage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name" || e.PropertyName == "StartDate" || e.PropertyName == "EndDate") this.Update(sender as Stage);
        }
        public void Fill()
        {
            Stages.CollectionChanged -= Stages_CollectionChanged;

            List<Dictionary<string, string>> allStages = GetQueryResult("select ID, Name, StartDate, EndDate from Stages;");

            foreach (Dictionary<string, string> w in allStages)
            {
                Stage nw = new Stage(long.Parse(w["ID"]), w["Name"], Convert.ToDateTime(w["StartDate"]), Convert.ToDateTime(w["EndDate"]));

                nw.PropertyChanged += Stage_PropertyChanged;

                Stages.Add(nw);
            }
            Stages.CollectionChanged += Stages_CollectionChanged;
        }
        public void Add(Stage stage)
        {
            stage.PropertyChanged += Stage_PropertyChanged;
            stage.Id = InsertRow($"insert into Stages (Name, StartDate, EndDate) values ('{stage.Name}', '{stage.StartDate}', '{stage.EndDate}');");
        }
        public void Delete(Stage stage)
        {
            Execute($"delete from Stages where Id={stage.Id}");
        }
        public void Update(Stage stage)
        {
            Execute($"update Stages set Name='{stage.Name}', StartDate='{stage.StartDate}', EndDate='{stage.EndDate}' where Id={stage.Id};");
        }
    }
}
