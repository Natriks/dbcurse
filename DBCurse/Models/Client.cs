using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DBCurse.Models
{
    public class Client : INotifyPropertyChanged
    {
        private string name, email, adress, description, contactperson;
        private int telephone;
        public long Id { get; set; }
        public String Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public String Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }
        public String Adress
        {
            get { return adress; }
            set { adress = value; OnPropertyChanged("Adress"); }
        }
        public String Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }
        public String Contactperson
        {
            get { return contactperson; }
            set { contactperson = value; OnPropertyChanged("Contactperson"); }
        }
        public Int32 Telephone
        {
            get { return telephone; }
            set { telephone = value; OnPropertyChanged("Telephone"); }
        }
        public Client()
        {
            this.Name = "";
            this.Email = "";
            this.Adress = "";
            this.Description = "Отсутствует.";
            this.Contactperson = "";
            this.Telephone = 0;
        }

        public Client(long id, string name, string email, int telephone, string adress, string contactperson, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Adress = adress;
            this.Description = description;
            this.Contactperson = contactperson;
            this.Telephone = int.Parse(("89" + telephone.ToString()));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
