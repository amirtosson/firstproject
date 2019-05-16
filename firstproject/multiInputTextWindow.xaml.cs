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

namespace firstproject
{
    /// <summary>
    /// Interaction logic for multiInputTextWindow.xaml
    /// </summary>
    public partial class multiInputTextWindow : Window
    {
        public bool all_ok = false;
        public string new_name;
        public string new_email;
        public int new_age;
        public multiInputTextWindow( )
        {
            InitializeComponent();
        }
        public bool AgeTextCheck(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        private void add_ok_Click(object sender, RoutedEventArgs e)
        {
            new_name = userNameAdd.Text;
            new_email = userEmailAdd.Text;
            if(AgeTextCheck(userAgeAdd.Text))  new_age = int.Parse(userAgeAdd.Text);
            else MessageBox.Show("Invalid enteries.!!", "Error");
            if ((new_age < 100) && (new_age > 0))
            {
                all_ok = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Age ! .!!", "Error");
            }

        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class NewUser
    {
        string name="Default";
        string email= "Default";
        int age=0; 
        public string userName
        {
            get { return name; }
            set { name = value; }
        }
        public string userEmail
        {
            get { return email; }
            set { email = value; }
        }
        public int userAge
        {
            get { return age; }
            set { age = value; }
        }

    }
}
