using DomObjectImport.WorkClas;
using ExcelDataReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;
using System.ComponentModel;
using System.Drawing;


namespace DomObjectImport

{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            Errorlist.IsEnabled = false;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (tableCollection == null)
            {
                MessageBox.Show("Neesat izvelejies excel failu");
                    return;
            }

            try
            {

                excelvertibas.exceltabula = ((DataView) DataGridView1.ItemsSource).ToTable(); // aizsuta excel tabulas logu ar veiktam izmainam uz datatable mainigo no kura nolasa objektus 
            
            }
            catch(Exception ex)
            {
                MessageBox.Show("neesat izvēlējies excel darba lapu");
                return;

            }



            if (checkbox.IsChecked == true)
            {
                serverurl.serviceurl = "https://svc.proc.dom.lndb.lv/";                     

            }
            else
            {
                serverurl.serviceurl = "https://svc.proc.test.lndb.lv/";

            }
            if (verify_only.IsChecked == true)
            {
                excelvertibas.verify_only = true;
            }
            else
            {
                excelvertibas.verify_only = false;

            }
            ProcesClass.AddDomObject(); ///domobjektimporta funkcija
            Errorlist.IsEnabled = true; // iesledz output list pogu
            //Sheetcombobox.Items.Clear();
            rowcoloring();
            //excelvertibas.exceltabula.Clear();
            //excelvertibas.exceltabula = dt.Copy();

            
            //excelvertibas.sheetname = Sheetcombobox.SelectedItem.ToString();
        }

        
        DataTableCollection tableCollection;

        

        

        private void choose_file(object sender, RoutedEventArgs e)
        {
            DataGridView1.Columns.Clear();
            DataGridView1.ItemsSource = null;
            DataGridView1.Items.Clear();
            DataGridView1.Items.Refresh();

            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
             Nullable<bool> result = openFileDlg.ShowDialog();
             openFileDlg.DefaultExt = ".xlsx";
            openFileDlg.Filter = "Excel documents (.xlsx)|*.xlsx";
            if (result == true)
            {
                excelvertibas.Pathtoexcel = openFileDlg.FileName;
                try
                {
                    using (var stream = File.Open(openFileDlg.FileName,FileMode.Open,FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                        DataSet result1 = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                        });
                        tableCollection = result1.Tables;
                        
                        Sheetcombobox.Items.Clear();
                        foreach (DataTable table in tableCollection)
                            Sheetcombobox.Items.Add(table.TableName);




                        }

                    }
                }
                catch(System.IO.IOException)
                {
                    MessageBox.Show("Aizveriet izveleto excel failu excel redaktorā pirms uzsākat objektu importu.","IOException");
                }
                catch(ExcelDataReader.Exceptions.HeaderException)
                {
                    MessageBox.Show("Nav excel fails");
                }
                

            }
            





        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            DialogResult result = folderDlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                excelvertibas.SourcePath = folderDlg.SelectedPath;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Neesat izvelejies failu atrasanas mapi");

            }


        }
        DataTable dt;
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            try
            {

            dt = tableCollection[Sheetcombobox.SelectedItem.ToString()];
            DataGridView1.ItemsSource = dt.DefaultView;
            
            //excelvertibas.DataTableExcel = tableCollection[excelvertibas.sheetname];
            }
            catch (NullReferenceException)
            {
                
            }

            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Outputlist outputlist = new Outputlist();
            outputlist.Show();
            String[] errorlists = excelvertibas.errorlist.ToArray();
            System.IO.File.WriteAllLines(@".\errorlist.txt", errorlists);
            outputlist.errorlist.Text = String.Join("\n", errorlists);
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }
        void rowcoloring() //funckija kas iekraso rindas ar OK:false vertibu sarkana krasa 
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt = excelvertibas.exceltabula.Clone();
            int numRedRows = 0;


            DataTable krasutabula = excelvertibas.exceltabula;
            for (int i = 0; i < krasutabula.Rows.Count; i++) {
                DataGridRow rowclean = (DataGridRow) DataGridView1.ItemContainerGenerator.ContainerFromIndex(i);
                if (rowclean == null) { 
                    DataGridView1.UpdateLayout();
                    DataGridView1.ScrollIntoView(DataGridView1.Items[i]);
                    rowclean = (DataGridRow) DataGridView1.ItemContainerGenerator.ContainerFromIndex(i);
                }

                rowclean.Background = System.Windows.Media.Brushes.White;

                foreach (string Item in excelvertibas.getexternalid) {
                    if (Item == krasutabula.Rows[i].Field<string>("externalID").Trim() && krasutabula.Rows[i].Field<string>("externalID").Trim() != null) {
                        DataGridRow row = (DataGridRow) DataGridView1.ItemContainerGenerator.ContainerFromIndex(i);
                        if (row != null) {
                            row.Background = System.Windows.Media.Brushes.Red;
                            numRedRows++;
                            dt.ImportRow(krasutabula.Rows[i]);
                        }
                    }
                }
            }

            for (int i = 0; i < krasutabula.Rows.Count; i++) {
                DataGridRow rowclean = (DataGridRow) DataGridView1.ItemContainerGenerator.ContainerFromIndex(i);
                if (rowclean == null) { 
                    DataGridView1.UpdateLayout();
                    DataGridView1.ScrollIntoView(DataGridView1.Items[i]);
                    rowclean = (DataGridRow) DataGridView1.ItemContainerGenerator.ContainerFromIndex(i);
                }

                rowclean.Background = System.Windows.Media.Brushes.White;

                foreach (string Item in excelvertibas.getexternalid) {
                    if (Item == krasutabula.Rows[i].Field<string>("externalID").Trim() && krasutabula.Rows[i].Field<string>("externalID").Trim() != null) {
                        if (rowclean != null) {
                            rowclean.Background = System.Windows.Media.Brushes.Red;
                        }
                    }
                }
                if (rowclean.Background == System.Windows.Media.Brushes.White) {
                    dt.ImportRow(krasutabula.Rows[i]);
                }
            }
            // Change sorted view
            DataGridView1.ItemsSource = dt.DefaultView;

            for (int i = 0; i < numRedRows; i++) {
                DataGridRow rowclean = (DataGridRow) DataGridView1.ItemContainerGenerator.ContainerFromIndex(i);

                if (rowclean == null) { 
                    DataGridView1.UpdateLayout();
                    DataGridView1.ScrollIntoView(DataGridView1.Items[i]);
                    rowclean = (DataGridRow) DataGridView1.ItemContainerGenerator.ContainerFromIndex(i);
                }
                rowclean.Background = System.Windows.Media.Brushes.Red;
            }
            excelvertibas.rowcoloring = true;
        }
    }
}
