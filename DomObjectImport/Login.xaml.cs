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
using DomObjectImport.WorkClas;


namespace DomObjectImport
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Authlogin.Password = passwordbox.Password.ToString();
            Authlogin.Username = username.Text.ToString();
            if (Loginauth.AuthorizeLogin(Authlogin.Username,Authlogin.Password) == true)
            {
                excelvertibas.username = Authlogin.Username;
                excelvertibas.password = Authlogin.Password;
                MainWindow Importwindow = new MainWindow();
                Importwindow.Show();
                this.Close();
           }
         else
           {
                MessageBox.Show("Logins nav autorizets");

           }
           
                

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        

        private void keyboardpress_keyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Convert.ToChar(13)))
            {
                Button_Click(sender, e);
            }
        }
    }
}
