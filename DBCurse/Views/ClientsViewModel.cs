using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using DBCurse.Models;

namespace DBCurse.Views
{
    class ClientsViewModel : ViewModelFilter
    {
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ClientsViewModel), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ClientsViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterClient;
            }
        }

        public new ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly new DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ClientsViewModel), new PropertyMetadata(null));

        public ClientsViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(DataModel.Clients);
            Items.Filter = FilterClient;
        }
        private bool FilterClient(object obj)
        {
            bool result = true;
            Client client = (obj as Client);
            if (!string.IsNullOrWhiteSpace(FilterText) 
                && client != null 
                && !client.Name.ToLower().Contains(FilterText.ToLower())
                && !client.Email.ToLower().Contains(FilterText.ToLower())
                && !client.Adress.ToLower().Contains(FilterText.ToLower()) 
                && !client.Contactperson.ToLower().Contains(FilterText.ToLower()) 
                && !client.Description.ToLower().Contains(FilterText.ToLower())
                && !client.Telephone.ToString().ToLower().Contains(FilterText.ToLower()))
            {
                result = false;
            }
            return result;
        }
    }
}
