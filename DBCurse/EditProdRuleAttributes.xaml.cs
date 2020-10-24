using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DBCurse.Models;
using DBCurse.ProductiveModel;

namespace DBCurse
{
    /// <summary>
    /// Логика взаимодействия для EditProdRuleAttributes.xaml
    /// </summary>
    public partial class EditProdRuleAttributes : Window
    {
        public EditProdRuleAttributes()
        {
            InitializeComponent();
            Loaded += EditProdRuleAttributes_Loaded;
        }

        private void EditProdRuleAttributes_Loaded(object sender, RoutedEventArgs e)
        {
            //foreach (ProductAttribute pv in DataModel.ProductAttributes)
            //{
                //comboBox.Items.Add(pv.Name);
                //if (comboBox_Copy.Items.Count == 0)
                //    foreach (ProductAttributeValue pav in pv.AttributeValuesCollection)
                //    {
                //        comboBox_Copy.Items.Add(pav.NameValue);
                //    }
            //}
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
