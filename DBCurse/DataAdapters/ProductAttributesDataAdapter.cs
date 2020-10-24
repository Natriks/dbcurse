using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DBCurse.ProductiveModel;
using DBCurse.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DBCurse.DataAdapters
{
    class ProductAttributesDataAdapter : DataAdapter
    {
        private ObservableCollection<ProductAttribute> ProductAttributes;
        public ProductAttributesDataAdapter(MySqlConnection connection, ObservableCollection<ProductAttribute> productAttributes) : base(connection)
            {
            ProductAttributes = productAttributes;
        }
        private void ProductAttributes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this.Add(e.NewItems[0] as ProductAttribute);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this.Delete(e.OldItems[0] as ProductAttribute);
                    break;
            }
        }
        private void ProductAttribute_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name") this.Update(sender as ProductAttribute);
        }
        public void Fill()
        {
            ProductAttributes.CollectionChanged -= ProductAttributes_CollectionChanged;

            List<Dictionary<string, string>> allProductAttributes = GetQueryResult("select ID, Attribute from ProductiveAttributes;");

            foreach (Dictionary<string, string> p in allProductAttributes)
            {
                ProductAttribute np = new ProductAttribute(long.Parse(p["ID"]), p["Attribute"]);

                foreach (ProductAttributeValue v in DataModel.ProductAttributeValues)
                {
                    if (np.Id == v.IDAttribute)
                        np.AttributeValuesCollection.Add(v);
                }

                np.PropertyChanged += ProductAttribute_PropertyChanged;

                ProductAttributes.Add(np);
            }
            ProductAttributes.CollectionChanged += ProductAttributes_CollectionChanged;
        }
        public void Add(ProductAttribute productAttribute)
        {
            productAttribute.PropertyChanged += ProductAttribute_PropertyChanged;
            productAttribute.Id = InsertRow($"insert into ProductiveAttributes (Attribute) values ('{productAttribute.Name}');");
        }
        public void Delete(ProductAttribute productAttribute)
        {
            Execute($"delete from ProductiveAttributes where Id={productAttribute.Id}");
        }
        public void Update(ProductAttribute productAttribute)
        {
            Execute($"update ProductiveAttributes set Attribute='{productAttribute.Name}' where Id={productAttribute.Id};");
        }
    }
}
