using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace UrbanCoder.UI_Elements
{
    /// <summary>
    /// Interaction logic for PaginatedDataGrid.xaml
    /// </summary>
    public partial class PaginatedDataGrid : UserControl
    {
        public DataTable DT;
        public DataRowView currentRow;
        private int currentPage;

        public PaginatedDataGrid(DataTable inputDT)
        {
            InitializeComponent();
            UpdateData(inputDT);
        }

        public void UpdateData(DataTable inputDT)
        {
            try
            {
                DT = inputDT;
                // Populating Page Numbers
                if ((DT.Rows.Count % 10) == 0)
                {
                    L_TotalPages.Content = DT.Rows.Count / 10;
                }
                else
                {
                    L_TotalPages.Content = (DT.Rows.Count / 10) + 1;
                }
                // Populating DGV Columns & Rows
                DGV.Columns.Clear();
                foreach (DataColumn DC in inputDT.Columns)
                {
                    DataGridTextColumn DGC = new DataGridTextColumn
                    {
                        Header = DC.ColumnName,
                        Binding = new Binding(DC.ColumnName),
                        IsReadOnly = true
                    };
                    DGV.Columns.Add(DGC);
                }
                Navigate(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Navigate(int Page)
        {
            try
            {
                // Changing navigation button visibility
                if (Page + (int)L_TotalPages.Content == 2) // Has only one page
                {
                    // Disabling All
                    B_First.IsEnabled = false;
                    B_Previous.IsEnabled = false;
                    B_Last.IsEnabled = false;
                    B_Next.IsEnabled = false;
                }
                else if (Page == 1) // In first page
                {
                    // Toggle Visibility
                    B_First.IsEnabled = false;
                    B_Previous.IsEnabled = false;
                    B_Last.IsEnabled = true;
                    B_Next.IsEnabled = true;
                }
                else if(Page == (int)L_TotalPages.Content) // In last page
                {
                    // Toggle Visibility
                    B_Last.IsEnabled = false;
                    B_Next.IsEnabled = false;
                    B_First.IsEnabled = true;
                    B_Previous.IsEnabled = true;
                }
                else
                {
                    // Enabling All
                    B_First.IsEnabled = true;
                    B_Previous.IsEnabled = true;
                    B_Last.IsEnabled = true;
                    B_Next.IsEnabled = true;
                }
                DataTable tempDT = DT.AsEnumerable().Skip((Page * 10) - 10).Take(10).CopyToDataTable();
                DGV.ItemsSource = tempDT.DefaultView;
                T_PageNumber.Text = Page.ToString();
                currentPage = Page;
                L_Records.Content = DT.Rows.Count.ToString() + " Record(s) - ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NavButtons_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch ((sender as Button).Content)
                {
                    case "<<": // First Page
                        Navigate(1);
                        break;
                    case ">":  // Next Page
                        Navigate(Convert.ToInt32(T_PageNumber.Text) + 1);
                        break;
                    case ">>": // Last Page
                        Navigate((int)L_TotalPages.Content);
                        break;
                    default:  // Previous Page
                        Navigate(Convert.ToInt32(T_PageNumber.Text) - 1);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void T_PageNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int pageNumber = Convert.ToInt32(T_PageNumber.Text);
                    int totalPages = (int)L_TotalPages.Content;
                    // Checking if page number is valid
                    if (pageNumber >= 1 && pageNumber <= totalPages)
                    {
                        Navigate(pageNumber);
                    }
                    else
                    {
                        // Reverting page number
                        T_PageNumber.Text = currentPage.ToString();
                    }
                }
            }
            catch { }
        }

        private void T_PageNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            // Reverting page number
            T_PageNumber.Text = currentPage.ToString();
        }

        private void DGV_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(DGV.CurrentCell == null)
            {
                currentRow = null;
            }
            else
            {
                // Setting selected item to a public variable for the parent element to use
                currentRow = DGV.CurrentItem as DataRowView;
            }
        }
    }
}
