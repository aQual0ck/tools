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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using System.ComponentModel;

namespace tools.Pages
{
    /// <summary>
    /// Interaction logic for PageAddTool.xaml
    /// </summary>
    public partial class PageAddTool : Page
    {
        private AuxClasses.Tool tool;
        public PageAddTool()
        {
            InitializeComponent();
            cmbCategory.SelectedValuePath = "CategoryName";
            cmbCategory.DisplayMemberPath = "CategoryName";
            cmbCategory.ItemsSource = AuxClasses.DBClass.entObj.Category.ToList();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AuxClasses.FrameClass.frmObj.GoBack();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string dateOp = dpOperating.SelectedDate?.ToString(App.DateFormat);
            DateTime dtOp = DateTime.Parse(dateOp);
            string dateDec = dpDecom.SelectedDate?.ToString(App.DateFormat);
            DateTime dtDec = DateTime.Parse(dateDec);
            int catid = Convert.ToInt32(TypeDescriptor.GetProperties(cmbCategory.SelectionBoxItem)["Id"].GetValue(cmbCategory.SelectionBoxItem));
            tool = new AuxClasses.Tool()
            {
                CategoryId = catid,
                ModelName = txbModelName.Text,
                OperatingSince = dtOp,
                DecomissionedSince = dtDec,
                SerialNumber = txbSN.Text,
                Notes = txbNotes.Text
            };
            AuxClasses.DBClass.entObj.Tool.Add(tool);
            AuxClasses.DBClass.entObj.SaveChanges();
            MessageBox.Show("Добавлено");
        }
    }
}
