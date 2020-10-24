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
    class InvestorsViewModel : ViewModelFilter
    {
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(InvestorsViewModel), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as InvestorsViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterInvestor;
            }
        }

        public new ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly new DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(InvestorsViewModel), new PropertyMetadata(null));

        public InvestorsViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(DataModel.Investors);
            Items.Filter = FilterInvestor;
        }
        private bool FilterInvestor(object obj)
        {
            bool result = true;
            Investor investor = (obj as Investor);
            if (!string.IsNullOrWhiteSpace(FilterText) 
                && investor != null && !investor.Name.ToLower().Contains(FilterText.ToLower()) 
                && !investor.Email.ToLower().Contains(FilterText.ToLower())
                && !investor.Contactperson.ToLower().Contains(FilterText.ToLower()) 
                && !investor.Adress.ToLower().Contains(FilterText.ToLower()) 
                && !investor.Description.ToLower().Contains(FilterText.ToLower())
                && !investor.Telephone.ToString().ToLower().Contains(FilterText.ToLower()))
            {
                result = false;
            }
            return result;
        }
    }
}
