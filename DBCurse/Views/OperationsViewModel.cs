using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using DBCurse.Models;

namespace DBCurse.Views
{
    class OperationsViewModel : ViewModelFilter
    {
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(OperationsViewModel), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as OperationsViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterOperation;
            }
        }

        public new ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly new DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(OperationsViewModel), new PropertyMetadata(null));

        public OperationsViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(DataModel.Operations);
            Items.Filter = FilterOperation;
        }
        private bool FilterOperation(object obj)
        {
            bool result = true;
            Operation client = (obj as Operation);
            if (!string.IsNullOrWhiteSpace(FilterText)
                && client != null
                && !client.OperationName.ToLower().Contains(FilterText.ToLower())
                && !client.Amount.ToString().ToLower().Contains(FilterText.ToLower())
                && !client.Date.ToLower().Contains(FilterText.ToLower()))
            {
                result = false;
            }
            return result;
        }
    }
}
