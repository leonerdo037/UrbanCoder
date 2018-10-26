using Jarvis.Class;
using RestSharp;
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
using UrbanCoder.Classes;
using static UrbanCoder.Classes.Exceptions;

namespace UrbanCoder
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private bool? output;
        private UCD_Client APICall;

        public AddUser()
        {
            InitializeComponent();
            TB_Error.Visibility = Visibility.Collapsed;
        }

        public bool? ShowWindow(UCD_Client APIClient)
        {
            APICall = APIClient;
            // Centering Window
            double sWidth = SystemParameters.PrimaryScreenWidth;
            double sHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;
            Left = (sWidth / 2) - (windowWidth / 2);
            Top = (sHeight / 2) - (windowHeight / 2);
            ShowDialog();
            return output;
        }

        private void B_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void B_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Checking null values
                if (!string.IsNullOrEmpty(T_Username.Text) || !string.IsNullOrEmpty(T_Role.Text))
                {
                    // Saving
                    APICall.AddUserToTeam("UrbanCoder", T_Username.Text, T_Role.Text);
                    output = true;
                    Close();
                }
            }
            catch(UC_UnknownUser ex)
            {
                output = false;
                TB_Error.Text = ex.Message;
                TB_Error.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                output = false;
                TB_Error.Text = ex.Message;
                TB_Error.Visibility = Visibility.Visible;
            }
        }
    }
}
