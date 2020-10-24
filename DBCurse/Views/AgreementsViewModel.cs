using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using DBCurse.Models;
using System.Windows;

namespace DBCurse.Views
{
    class AgreementsViewModel : ViewModelFilter
    {
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(AgreementsViewModel), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as AgreementsViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterAgreement;
            }
        }

        public new ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly new DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(AgreementsViewModel), new PropertyMetadata(null));
        public AgreementsViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(DataModel.Agreements);
            Items.Filter = FilterAgreement;
        }
        private bool FilterAgreement(object obj)
        {
            bool result = true;
            Agreement agreement = (obj as Agreement);
            if (!string.IsNullOrWhiteSpace(FilterText) 
                && agreement != null 
                && !agreement.Id.ToString().ToLower().Contains(FilterText.ToLower()) 
                && !agreement.ClientName.ToLower().Contains(FilterText.ToLower())  
                && !agreement.Description.ToLower().Contains(FilterText.ToLower()) 
                && !agreement.Date.ToLower().Contains(FilterText.ToLower()) 
                && !agreement.Deadline.ToLower().Contains(FilterText.ToLower()) 
                && !agreement.Amount.ToString().ToLower().Contains(FilterText.ToLower()))
            {
                result = false;
            }
            return result;
        }

    }
}
