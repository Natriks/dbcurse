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
    class ProjectsViewModel : ViewModelFilter
    {
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ProjectsViewModel), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ProjectsViewModel;
            if (current != null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterProject;
            }
        }

        public new ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly new DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ProjectsViewModel), new PropertyMetadata(null));

        public ProjectsViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(DataModel.Projects);
            Items.Filter = FilterProject;
        }
        private bool FilterProject(object obj)
        {
            bool result = true;
            Project project = (obj as Project);
            if (!string.IsNullOrWhiteSpace(FilterText) 
                && project != null 
                && !project.Name.ToLower().Contains(FilterText.ToLower()) 
                && !project.ResponsibleName.ToLower().Contains(FilterText.ToLower())
                && !project.StartDate.ToLower().Contains(FilterText.ToLower()) 
                && !project.EndDate.ToLower().Contains(FilterText.ToLower()) 
                && !project.TypeName.ToLower().Contains(FilterText.ToLower())
                && !project.AgreementID.ToString().ToLower().Contains(FilterText.ToLower()) 
                && !project.Description.ToLower().Contains(FilterText.ToLower()))
            {
                result = false;
            }
            return result;
        }
    }
}
