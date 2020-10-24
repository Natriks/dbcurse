using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DBCurse.ProductiveModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DBCurse.DataAdapters
{
    class ProductAttrValuesDataAdapter : DataAdapter
    {
        private ObservableCollection<ProductAttributeValue> ProductAttributeValues;
        public ProductAttrValuesDataAdapter(MySqlConnection connection, ObservableCollection<ProductAttributeValue> productAttributeValues) : base(connection)
        {
            ProductAttributeValues = productAttributeValues;
        }
        private void ProductAttributeValues_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as ProductAttributeValue);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as ProductAttributeValue);
                    break;
            }
        }
        private void ProductAttributeValue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value" || e.PropertyName == "StartDate" || e.PropertyName == "EndDate") this.Update(sender as ProductAttributeValue);
        }
        public void Fill()
        {
            ProductAttributeValues.CollectionChanged -= ProductAttributeValues_CollectionChanged;

            List<Dictionary<string, string>> allProductAttributeValues = GetQueryResult("select ID, IDAttribute, Value, Min, Max from ProductiveAttributeValues;");

            foreach (Dictionary<string, string> w in allProductAttributeValues)
            {
                ProductAttributeValue nw = new ProductAttributeValue(long.Parse(w["ID"]), Convert.ToInt32(w["IDAttribute"]), w["Value"], Convert.ToInt32(w["Min"]), Convert.ToInt32(w["Max"]));

                nw.PropertyChanged += ProductAttributeValue_PropertyChanged;

                ProductAttributeValues.Add(nw);
            }
            ProductAttributeValues.CollectionChanged += ProductAttributeValues_CollectionChanged;
        }
        public void Add(ProductAttributeValue productAttributeValue)
        {
            productAttributeValue.PropertyChanged += ProductAttributeValue_PropertyChanged;
            productAttributeValue.Id = InsertRow($"insert into ProductiveAttributeValues (IDAttribute, Value, Min, Max) values ('{productAttributeValue.IDAttribute}', '{productAttributeValue.NameValue}', '{productAttributeValue.Min}', '{productAttributeValue.Max}');");
        }
        public void Delete(ProductAttributeValue productAttributeValue)
        {
            Execute($"delete from ProductiveAttributeValues where Id={productAttributeValue.Id}");
        }
        public void Update(ProductAttributeValue productAttributeValue)
        {
            Execute($"update ProductiveAttributeValues set IDAttribute='{productAttributeValue.IDAttribute}', Value='{productAttributeValue.NameValue}', Min='{productAttributeValue.Min}', Max='{productAttributeValue.Max}' where Id={productAttributeValue.Id};");
        }
    }
}
