using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCurse.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DBCurse.ProductiveModel
{
    class ProductAttribute : INotifyPropertyChanged
    {
        private string name;
        public long Id { get; set; }
        private ObservableCollection<ProductAttributeValue> attributeValuesCollection;
        public String Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        public ObservableCollection<ProductAttributeValue> AttributeValuesCollection
        {
            get { return attributeValuesCollection; }
            set { attributeValuesCollection = value; OnPropertyChanged("AttributeValuesCollection"); }
        }
        public ProductAttribute()
        {
            this.Name = "";
            this.AttributeValuesCollection = new ObservableCollection<ProductAttributeValue>();
        }
        public ProductAttribute(long id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.AttributeValuesCollection = new ObservableCollection<ProductAttributeValue>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
