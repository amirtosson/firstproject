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
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using Path = System.IO.Path;

namespace firstproject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class BG: System.Windows.Controls.Control, INotifyPropertyChanged
    {
        string name;
        public BG()
        {
            BG_T = @"Resources\fkp-logo.jpg";
        }
        public string BG_T
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(BG_T));

            }
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
    public class River: INotifyPropertyChanged
    {
        string name;
        int milesLong;


        public string Title_P
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Title_P));

            }
        }

        public int MilesLong
        {
            get { return milesLong; }
            set
            {
                milesLong = value;
                RaisePropertyChanged(nameof(MilesLong));

            }
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }


    public partial class MainWindow : Window
	{
        static string sourceDir = @"C:\Users\user\Desktop\Data";

        public MainWindow()
		{
			InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
           // theRiver.Title_P = "out of the second panal.";
           // theRiver.MilesLong = 0;
            _bg_.BG_T = @"Resources\fkp-logo.jpg";
        }


        public int tic;
        public int toc;
        public static int second_conter;
        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			Title = e.GetPosition(this).ToString();
		}

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dl = new OpenFileDialog();
            dl.ShowDialog();
            System.Windows.MessageBox.Show(dl.FileName, dl.SafeFileName);

            multiInputTextWindow _inputWindow = new multiInputTextWindow();
            _inputWindow.ShowDialog();
            string ID="j";
            if (_inputWindow.all_ok)
            {
                ID = _inputWindow.new_date +
                        "_" + _inputWindow.new_time + "_" + _inputWindow.new_name + "_" +
                        _inputWindow.new_mat + "_" + _inputWindow.new_fac + "_" + _inputWindow.new_org + ".txt";
                string fullnamehrlp =  _inputWindow.new_date +
                        "_" + _inputWindow.new_time + "_" + _inputWindow.new_name + "_" +
                        _inputWindow.new_mat + "_" + _inputWindow.new_fac + "_" + _inputWindow.new_org + "_Read_Me.txt";
                using (StreamWriter writer2 = new StreamWriter(Path.Combine(sourceDir, fullnamehrlp)))
                {
                    writer2.WriteLine(ID);
                    writer2.WriteLine(" has been conducted at " + _inputWindow.new_fac + " under supervision of " + _inputWindow.new_name + ".");
                }

                System.Windows.Forms.MessageBox.Show(_inputWindow.new_name + " has been added to your list", "Confirmation of adding a new user ", MessageBoxButtons.OK);

            }
            _inputWindow.Close();



            File.Copy(dl.FileName, Path.Combine(sourceDir, ID), true);


            // _bg_.BG_T = @"Resources\" + dl.SafeFileName;


        }

        private void button_2_Click(object sender, RoutedEventArgs e)
        {
            searchWindow _sF= new searchWindow();
            _sF.Show();
        }

        private void button_4_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Bye","Closing the App" );
            this.Close();
        }

        private void bdr_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // theRiver.Title_P = "in the second panel and X-pixel= "+ e.GetPosition(this).X.ToString() + " Y-pixel= "+ e.GetPosition(this).Y.ToString() +".";

        }

        private void StackPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // theRiver.Title_P = "out of the second panal";
            toc = DateTime.Now.Second;
            second_conter += (toc - tic);
           /// theRiver.MilesLong = second_conter;
        }

        private void StackPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            tic = DateTime.Now.Second;
        }
    }
}
