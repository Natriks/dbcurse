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
    class WorkersViewModel : ViewModelFilter
    {
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(WorkersViewModel), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as WorkersViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterWorker;
            }
        }

        public new ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly new DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(WorkersViewModel), new PropertyMetadata(null));

        public WorkersViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(DataModel.Workers);
            Items.Filter = FilterWorker;
        }
        private bool FilterWorker(object obj)
        {
            bool result = true;
            Worker worker = (obj as Worker);
            if (!string.IsNullOrWhiteSpace(FilterText) 
                && worker != null 
                && !worker.Name.ToLower().Contains(FilterText.ToLower()) 
                && !worker.Surname.ToLower().Contains(FilterText.ToLower()) 
                && !worker.Patronomic.ToLower().Contains(FilterText.ToLower()) 
                && !worker.PositionName.ToLower().Contains(FilterText.ToLower()) 
                && !worker.Address.ToLower().Contains(FilterText.ToLower()) 
                && !worker.Email.ToLower().Contains(FilterText.ToLower())
                && !worker.Telephone.ToString().ToLower().Contains(FilterText.ToLower()) 
                && !worker.PassportNumber.ToString().ToLower().Contains(FilterText.ToLower()) 
                && !worker.Birthday.ToLower().Contains(FilterText.ToLower()))
            {
                result = false;
            }
            return result;
        }
    }
}
