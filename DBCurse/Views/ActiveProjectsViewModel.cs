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
    class ActiveProjectsViewModel : ViewModelFilter
    {
        public new ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly new DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ActiveProjectsViewModel), new PropertyMetadata(null, Items_Changed));

        private static void Items_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ActiveProjectsViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterProject;
            }
        }

        public ActiveProjectsViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(DataModel.Projects);
            Items.Filter = FilterProject;
        }
        private bool FilterProject(object obj)
        {
            bool result = true;
            Project project = (obj as Project);
            if (IsPast(project.EndDate))
            {
                result = false;
            }
            return result;
        }
        private bool IsPast(string date)
        {
            int years = int.Parse(date[0].ToString() + date[1].ToString() + date[2].ToString() + date[3].ToString());
            int month = int.Parse(date[5].ToString() + date[6].ToString());
            int days = int.Parse(date[8].ToString() + date[9].ToString());
            if (years < DateTime.Now.Year)
                return true;
            if (years == DateTime.Now.Year)
                if (month < DateTime.Now.Month)
                    return true;
            if (years == DateTime.Now.Year)
                if (month == DateTime.Now.Month)
                    if (days < DateTime.Now.Day)
                        return true;
            return false;
        }
    }
}
