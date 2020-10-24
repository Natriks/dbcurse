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
    class ClientsDataAdapter : DataAdapter
    {
        private ObservableCollection<Client> Clients;
        public ClientsDataAdapter(MySqlConnection connection, ObservableCollection<Client> clients) : base(connection)
        {
            Clients = clients;
        }
        private void Clients_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as Client);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as Client);
                    break;
            }
        }
        private void Client_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Будем вызывать только если изменилось имя группы
            if (e.PropertyName == "Name" || e.PropertyName == "Email" || e.PropertyName == "Telephone" || e.PropertyName == "Adress" || e.PropertyName == "Contactperson" || e.PropertyName == "Description") this.Update(sender as Client);
        }

        public void Fill()
        {
            Clients.CollectionChanged -= Clients_CollectionChanged;
            List<Dictionary<string, string>> allClients = GetQueryResult("select ID, Name, Email, Telephone, Address, ContactPerson, Description from Clients;");
            foreach (Dictionary<string, string> c in allClients)
            {
                Client nc = new Client(long.Parse(c["ID"]), c["Name"], c["Email"], int.Parse(c["Telephone"]), c["Address"], c["ContactPerson"], c["Description"]);
                nc.PropertyChanged += Client_PropertyChanged;
                Clients.Add(nc);
            }
            Clients.CollectionChanged += Clients_CollectionChanged;
        }
        public void Add(Client client)
        {
            client.PropertyChanged += Client_PropertyChanged;
            client.Id = InsertRow($"insert into Clients (Name, Email, Telephone, Address, ContactPerson, Description) values ('{client.Name}', '{client.Email}', '{client.Telephone}', '{client.Adress}', '{client.Contactperson}', '{client.Description}');");
        }
        public void Delete(Client client)
        {
            Execute($"delete from Clients where Id={client.Id}");
        }
        public void Update(Client client)
        {
            Execute($"update Clients set Name='{client.Name}', Email='{client.Email}', Telephone='{client.Telephone}', Address='{client.Adress}', ContactPerson='{client.Contactperson}', Description='{client.Description}' where Id={client.Id};");
        }
    }
}
