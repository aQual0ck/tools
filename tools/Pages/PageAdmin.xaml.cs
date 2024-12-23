using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Win32;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Layout.Properties;

namespace tools.Pages
{
    /// <summary>
    /// Interaction logic for PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        private string _filepath;
        private AuxClasses.Tool tool;
        private AuxClasses.Users user;
        private int catid;
        private int roleid;
        public PageAdmin()
        {
            InitializeComponent();
            dgrTools.ItemsSource = AuxClasses.DBClass.entObj.Tool.ToList();
            dgrUsers.ItemsSource = AuxClasses.DBClass.entObj.Users.ToList();

            cmbCategory.SelectedValuePath = "CategoryName";
            cmbCategory.DisplayMemberPath = "CategoryName";
            var cat = AuxClasses.DBClass.entObj.Category.ToList();
            cat.Insert(0, new AuxClasses.Category { Id = 0, CategoryName = "Все категории" });
            cmbCategory.ItemsSource = cat;
            cmbCategory.SelectedIndex = 0;

            cmbRole.SelectedValuePath = "RoleName";
            cmbRole.DisplayMemberPath = "RoleName";
            var role = AuxClasses.DBClass.entObj.Roles.ToList();
            role.Insert(0, new AuxClasses.Roles { Id = 0, RoleName = "Все категории" });
            cmbRole.ItemsSource = role;
            cmbRole.SelectedIndex = 0;
        }

        private void menuAddTool_Click(object sender, RoutedEventArgs e)
        {
            AuxClasses.FrameClass.frmObj.Navigate(new PageAddTool());
        }

        private void menuLogOut_Click(object sender, RoutedEventArgs e)
        {
            AuxClasses.FrameClass.frmObj.Navigate(new PageLogin());
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void dgrTools_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgrTools.SelectedItem != null)
            {
                var id = TypeDescriptor.GetProperties(dgrTools.SelectedItem)["Id"].GetValue(dgrTools.SelectedItem);
                tool = AuxClasses.DBClass.entObj.Tool.FirstOrDefault(x => x.Id == (int)id);
                AuxClasses.FrameClass.frmObj.Navigate(new PageEditTools(tool));
            }
        }

        private void ApplyFilters()
        {
            if (cmbCategory.SelectedItem != null)
                catid = Convert.ToInt32(TypeDescriptor.GetProperties(cmbCategory.SelectedItem)["Id"].GetValue(cmbCategory.SelectedItem));
            else
                catid = 0;

            if (cmbRole.SelectedItem != null)
                roleid = Convert.ToInt32(TypeDescriptor.GetProperties(cmbRole.SelectedItem)["Id"].GetValue(cmbRole.SelectedItem));
            else
                roleid = 0;

            var query = AuxClasses.DBClass.entObj.Tool.AsQueryable();
            var query1 = AuxClasses.DBClass.entObj.Users.AsQueryable();

            if (catid != 0)
                query = query.Where(x => x.CategoryId == catid);

            if (!string.IsNullOrEmpty(txbSearchTools.Text))
                query = query.Where(x => x.ModelName.ToLower().Contains(txbSearchTools.Text.ToLower()));

            if (roleid != 0)
                query1 = query1.Where(x => x.RoleId == roleid);

            if (!string.IsNullOrEmpty(txbSearchUsers.Text))
                query1 = query1.Where(x => x.FullName.ToLower().Contains(txbSearchUsers.Text.ToLower()));

            dgrTools.ItemsSource = query.ToList();
            dgrUsers.ItemsSource = query1.ToList();
        }

        private void menuReport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Отчет";
            sfd.DefaultExt = ".pdf";

            PdfFont font = PdfFontFactory.CreateFont($"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\Assets\\arial.ttf", "Identity-H");

            bool? result = sfd.ShowDialog();

            if (result == true)
            {
                _filepath = sfd.FileName;

                using (PdfWriter writer = new PdfWriter(_filepath))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document doc = new Document(pdf, PageSize.DEFAULT);
                        doc.SetFont(font);
                        float[] columnWidths = { 10f, 20f, 20f, 10f, 10f, 30f };
                        Table table = new Table(UnitValue.CreatePercentArray(columnWidths)).UseAllAvailableWidth();

                        foreach (var column in dgrTools.Columns)
                        {
                            table.AddHeaderCell(new Cell().Add(new Paragraph(column.Header.ToString())));
                        }

                        foreach (var item in dgrTools.Items)
                        {
                            foreach (var column in dgrTools.Columns)
                            {
                                if (column.Header.ToString() != "Категория")
                                {
                                    var cellContent = TypeDescriptor.GetProperties(item)[$"{column.SortMemberPath}"].GetValue(item);
                                    string cellValue = cellContent != null ? cellContent.ToString() : string.Empty;
                                    table.AddCell(new Cell().Add(new Paragraph(cellValue)));
                                }
                                else if (column.Header.ToString() == "Категория")
                                {
                                    object cell = TypeDescriptor.GetProperties(item)["Category"].GetValue(item);
                                    string cellValue = TypeDescriptor.GetProperties(cell)["CategoryName"].GetValue(cell).ToString();
                                    table.AddCell(new Cell().Add(new Paragraph(cellValue)));
                                }
                            }
                        }

                        doc.Add(table);
                        doc.Close();
                    }
                }
                MessageBox.Show($"Отчет сохранен по данному пути: {_filepath}");
            }
        }

        private void txbSearchTools_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void menuAddUser_Click(object sender, RoutedEventArgs e)
        {
            AuxClasses.FrameClass.frmObj.Navigate(new PageAddUser());
        }

        private void txbSearchUsers_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void dgrUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgrUsers.SelectedItem != null)
            {
                var id = TypeDescriptor.GetProperties(dgrUsers.SelectedItem)["Id"].GetValue(dgrUsers.SelectedItem);
                user = AuxClasses.DBClass.entObj.Users.FirstOrDefault(x => x.Id == (int)id);
                AuxClasses.FrameClass.frmObj.Navigate(new PageEditUser(user));
            }
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }
    }
}
