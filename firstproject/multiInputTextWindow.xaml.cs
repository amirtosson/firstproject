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
        public string new_name= " ";
        public string new_email = " ";
        public string new_age = " ";
        public string new_date = " ";
        public string new_time = " ";
        public string new_mat = " ";
        public string new_fac = " ";
        public string new_org = " ";
        public multiInputTextWindow( )
        {
            InitializeComponent();
        }
        public bool inputCheck(string str)
        {
            if(new_time != " ")

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
            new_org = userOrgAdd.Text;
            new_fac = userFacAdd.Text;
            new_date = userDateAdd.Text;
            new_mat = userMatAdd.Text;
            new_time = userTimeAdd.Text;
            all_ok = true;
            this.Close();
            
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void now_Click(object sender, RoutedEventArgs e)
        {
           //dateString=  DateTime.Today.Day.ToString()+"_"+DateTime.Today.Month.ToString()+"_"+DateTime.Today.Year.ToString()+"_"+ DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString();

            userDateAdd.Text= DateTime.Today.Year.ToString() + "_" + DateTime.Today.Month.ToString() + "_" + DateTime.Today.Day.ToString();
            userTimeAdd.Text= DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString();
        }
    }

    public class NewUser
    {
        string name="Default";
        string email= "Default";
        string date = "Default";
        string time = "Default";
        string mat = "Default";
        string fac = "Default";
        string org = "Default";
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
        public string userMat
        {
            get { return mat; }
            set { mat = value; }
        }
        public string userOrg
        {
            get { return org; }
            set { org = value; }
        }
        public string userFac
        {
            get { return fac; }
            set { fac = value; }
        }
        public string userDate
        {
            get { return date; }
            set { date = value; }
        }
        public string userTime
        {
            get { return time; }
            set { time = value; }
        }


    }
}
