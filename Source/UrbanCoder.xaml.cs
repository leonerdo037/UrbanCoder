using Jarvis.Class;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using UrbanCoder.Classes;
using UrbanCoder.UI_Elements;
using static UrbanCoder.Classes.Exceptions;

namespace UrbanCoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UCD_Client APICall;
        private BackgroundWorker worker;
        // Data Variables
        private UCD_Data.APIOutput teamInfo;
        private UCD_Data.APIOutput resourceInfo;
        private PaginatedDataGrid userDataGrid;

        public MainWindow()
        {
            InitializeComponent();
            Properties.Settings.Default.ucd_url = "";
            Properties.Settings.Default.userName = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Staging Background Worker
            if (worker == null)
            {
                worker = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            }
            // Setting Visibility of TabItems
            IsLoggedIn(false);
        }

        #region Background Worker
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Calling Method
            Dispatcher.Invoke(() =>
            {
                Effect = new BlurEffect();
                switch (e.Argument.ToString())
                {
                    case "Login":
                        Login();
                        break;
                    case "CreateData":
                        CreateData();
                        break;
                    default:
                        break;
                }
            });
        }

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Effect = null;
        }
        #endregion

        // Method to update user data
        private void FillUsers()
        {
            teamInfo = APICall.GetTeamInfo("UrbanCoder");
            // Listing Users
            if (SP_DGVHolder.Children.Count == 1 && teamInfo.Table != null)
            {
                userDataGrid = new PaginatedDataGrid(teamInfo.Table);
                userDataGrid.Name = "userData";
                userDataGrid.DGV.MouseDoubleClick += DataGrid_DoubleClick;
                userDataGrid.B_Refresh.Click += Refresh_Click;
                SP_DGVHolder.Children.Add(userDataGrid);
            }
            else if (teamInfo.Table != null)
            {
                userDataGrid.UpdateData(teamInfo.Table);
            }
        }

        #region Login Logic
        private void T_URI_GotFocus(object sender, RoutedEventArgs e)
        {
            L_URIOverlay.Visibility = Visibility.Hidden;
        }

        private void T_URI_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(T_URI.Text))
            {
                L_URIOverlay.Visibility = Visibility.Visible;
            }
        }

        private void T_Username_GotFocus(object sender, RoutedEventArgs e)
        {
            L_UserOverlay.Visibility = Visibility.Hidden;
        }

        private void T_Username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(T_Username.Text))
            {
                L_UserOverlay.Visibility = Visibility.Visible;
            }
        }

        private void T_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            L_PassOverlay.Visibility = Visibility.Hidden;
        }

        private void T_Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(T_Password.Password))
            {
                L_PassOverlay.Visibility = Visibility.Visible;
            }
        }

        // Method to Toggle Visibility
        private void IsLoggedIn(bool value)
        {
            if (value)
            {
                // Control Visibilities
                TI_Hosts.Visibility = Visibility.Visible;
                TI_Jobs.Visibility = Visibility.Visible;
                TI_Status.Visibility = Visibility.Visible;
                SP_Settings.Visibility = Visibility.Visible;
                T_URI.IsEnabled = false;
                T_Username.IsEnabled = false;
                T_Password.IsEnabled = false;
                EX_Login.IsExpanded = false;
                B_Login.Content = "Logout";
                Properties.Settings.Default.userName = T_URI.Text;
                Properties.Settings.Default.ucd_url = T_Username.Text;
                Properties.Settings.Default.Save();
                // Populating UI
                //LB_Users.ItemsSource = teamInfo.Data.roleMappings.Select(dn => dn.user.displayName).ToList();
            }
            else if (!value)
            {
                // Control Visibilities
                L_PassOverlay.Visibility = Visibility.Visible;
                TI_Hosts.Visibility = Visibility.Collapsed;
                TI_Jobs.Visibility = Visibility.Collapsed;
                TI_Status.Visibility = Visibility.Collapsed;
                SP_Settings.Visibility = Visibility.Collapsed;
                T_Password.Clear();
                T_URI.IsEnabled = true;
                T_Username.IsEnabled = true;
                T_Password.IsEnabled = true;
                // Checking for saved settings
                if (string.IsNullOrEmpty(Properties.Settings.Default.ucd_url) || string.IsNullOrEmpty(Properties.Settings.Default.userName))
                {
                    T_URI.Clear();
                    T_Username.Clear();
                    L_URIOverlay.Visibility = Visibility.Visible;
                    L_UserOverlay.Visibility = Visibility.Visible;
                }
                else
                {
                    T_URI.Text = Properties.Settings.Default.userName;
                    T_Username.Text = Properties.Settings.Default.ucd_url;
                    L_URIOverlay.Visibility = Visibility.Collapsed;
                    L_UserOverlay.Visibility = Visibility.Collapsed;
                }
                B_Login.Content = "Login";
            }
        }

        // Method to show login related error message
        private void ShowErrorMessage(string errMessage = null)
        {
            if (string.IsNullOrEmpty(errMessage))
            {
                TB_LoginInvalid.Visibility = Visibility.Collapsed;
            }
            else
            {
                TB_LoginInvalid.Text = errMessage;
                TB_LoginInvalid.Visibility = Visibility.Visible;
            }
        }

        // Method to create team
        private void CreateTeam()
        {
            try
            {
                APICall.CreateTeam("UrbanCoder", "The team used by the UrbanCoder application.");
                // Updating teamInfo with newly created team details.
                teamInfo = APICall.GetTeamInfo("UrbanCoder").APIData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Create Base Resource
        private void CreateParentResource()
        {
            try
            {
                APICall.CreateResource("UrbanCoder", "The parent resource used by the UrbanCoder application.");
                APICall.CreateResourceProperty("UrbanCoder", "Test", "Value");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Login()
        {
            try
            {
                ShowErrorMessage();
                // Validating Login Prerequisites
                if (string.IsNullOrEmpty(T_URI.Text) || string.IsNullOrEmpty(T_Username.Text) || string.IsNullOrEmpty(T_Password.Password))
                {
                    ShowErrorMessage("Enter the URL, Username and Password !");
                    return;
                }
                // Creating API Client
                APICall = new UCD_Client(T_URI.Text, T_Username.Text, T_Password.Password, CB_Cert.IsChecked.Value);
                // Fetching UrbanCoder team information
                teamInfo = APICall.GetTeamInfo("UrbanCoder");
                // Checking if user is part of UrbanCoder team
                bool addToTeam = true;
                foreach (UCD_Data.TeamInfo.RoleMapping userData in teamInfo.APIData.roleMappings)
                {
                    if (userData.user.name == T_Username.Text)
                    {
                        addToTeam = false;
                        break;
                    }
                }
                // Adding user to Team
                if (addToTeam)
                {
                    APICall.AddUserToTeam("UrbanCoder", T_Username.Text, "Administrator");
                }
                // Successfully Logged In
                IsLoggedIn(true);
                ShowErrorMessage();
                FillUsers();
                // Checking if UrbanCoder pre-requisites are met.(Resource data etc)
                resourceInfo = APICall.ListResources("UrbanCoder");
            }
            catch(UC_LoginFailed)
            {
                ShowErrorMessage("Invalid Username or Password !");
            }
            catch(UC_TeamNotFound)
            {
                // First Login detected, trying to create a Team
                CreateTeam();
            }
            catch(UC_UnknownUser)
            {
                ShowErrorMessage("You do not belong to the UrbanCoder Team in UCD !");
            }
            catch (UC_ResourceNotFound)
            {
                // Enabling create data options as the resource is not there.
                SP_CreateDataHolder.IsEnabled = true;
                CreateParentResource();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error in Login: " + ex.Message);
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            switch (B_Login.Content)
            {
                case "Login":
                    // Running Background Worker
                    if (!worker.IsBusy)
                    {
                        worker.RunWorkerAsync("Login");
                    }
                    else
                    {
                        MessageBox.Show("Login already initiated !");
                    }
                    break;
                case "Logout":
                    IsLoggedIn(false);
                    break;
            }
        }
        #endregion

        #region Data Logic
        private void DataGrid_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(userDataGrid.currentRow != null)
            {
                MessageBox.Show(userDataGrid.currentRow["No"].ToString());
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            // Fetching UserControl name
            var parent = VisualTreeHelper.GetParent(sender as Button);
            while (!(parent is UserControl))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            // Switching to the respective method
            switch ((parent as PaginatedDataGrid).Name)
            {
                case "userData":
                    FillUsers();
                    break;
                default:
                    break;
            }
        }

        private void CreateData()
        {
            
        }

        private void CreateData_Click(object sender, RoutedEventArgs e)
        {
            // Running Background Worker
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync("CreateData");
            }
            else
            {
                MessageBox.Show("UrbanCoder is busy, try again after some time !");
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUser msg = new AddUser();
            msg.ShowWindow(APICall);
            FillUsers();
        }
    }
}
