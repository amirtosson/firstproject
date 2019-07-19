using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Microsoft.VisualBasic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace firstproject
{
    /// <summary>
    /// Interaction logic for searchWindow.xaml
    /// </summary>

    public partial class searchWindow : Window
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private static Random randNum = new Random();
        public static ObservableCollection<User> mainitems = new ObservableCollection<User>();

        public searchWindow()
        {
            InitializeComponent();
            makeList(ref mainitems);
            lvDataBinding.ItemsSource = mainitems;
        }
        public void makeList(ref ObservableCollection<User> items)
        {
           string help, help1, help3, help4, help5, help6;
            int help2;
            string dateString;
            string[] mat = {"Cl", "Al", "GaAs", "Cu", "Ni", "Fe" };
            string[] fac = { "ESRF", "EDDI", "Petra", "HomeLab"};
            string[] org = { "Siegen", "Hamburg", "Dortmund", "Frankfurt" };
            DirectoryInfo di = new DirectoryInfo(@"C:\Users\user\Desktop\Data\");
            FileInfo[] files = di.GetFiles("*.txt");
            String pattern = @"_"; 
     
            
            
             
            for (int i = 0; i < 20; ++i)
            {
                help = RandomString(randNum.Next(4, 9));
                help1 = help + "_" + randNum.Next(1, 6).ToString();
                help2 = randNum.Next(10, 60);
                help3 = help.ToLower() + i.ToString() + "@server.com";
                help4 = mat[randNum.Next(0, 5)];
                help5 = fac[randNum.Next(0, 3)];
                help6 = org[randNum.Next(0, 3)];

                dateString=  DateTime.Today.Day.ToString()+"_"+DateTime.Today.Month.ToString()+"_"+DateTime.Today.Year.ToString()+"_"+ DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString();
                


                items.Add(new User()
                {
                    Scientist = help,
                    ExpID = help1,
                   Date = "DD_MM_YYYY",
                   Email = help3,
                   Material = help4,
                    RadFacility = help5,
                    Organization = help6,
                    fullName= dateString + "_" + help1 + "_" + help2.ToString() + "_" + help4 + "_" + help5 + "_" + help6
                });

                StreamWriter writer = new StreamWriter(dateString + "_" + help1 + "_" + help2.ToString() + "_" + help4 + "_" + help5 + "_" + help6);
                using (StreamWriter writer2 = new StreamWriter(dateString + "_" + help1 + "_" + help2.ToString() + "_" + help4 + "_" + help5 + "_" + help6 + "_Read_Me.txt"))
                {
                    writer2.Write(help1);
                   writer2.WriteLine(" has been conducted at " + help5 + " under supervision of " + help + ".");
                   writer2.WriteLine("The date: " + help2.ToString());
                    writer2.WriteLine("The organization: " + help6);
                    writer2.WriteLine("The used material: " + help4);
               }

            }
			for (int numOfFiles = 0; numOfFiles < files.Length; ++numOfFiles)
			{
				string[] names = Regex.Split(files[numOfFiles].ToString(), pattern);
				items.Add(new User()
				{
					Scientist = names[5],
					ExpID = names[5].ToUpper() + names[8] + names[3] + "_" + names[4],
					Date = names[0] + "_" + names[1] + "_" + names[2],
					Email = names[5] + names[5] + "@hotmail.com",
					Material = names[8],
					RadFacility = names[9],
					Organization = names[10],
					fullName = files[numOfFiles].ToString()
				});



			}
		}
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[randNum.Next(s.Length)]).ToArray());
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                lvDataBinding.Items.SortDescriptions.Clear();
            }
            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            lvDataBinding.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));

        }

        private void lvDataBinding_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            User sel_user = (User)lvDataBinding.SelectedItems[0];
            System.Windows.Forms.DialogResult m  ;
            string msg = "Are you sure that you want to see the Email ?";
            string title = "Information" ;
            System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.YesNo;
            m =System.Windows.Forms.MessageBox.Show(msg,title,buttons);
            if (m == System.Windows.Forms.DialogResult.Yes)
            {
                System.Windows.Forms.MessageBox.Show(sel_user.Email,"The Email");
            }
        }
        private bool itemFilter(object item)
        {
            if (String.IsNullOrEmpty(searchFilter.Text))
                return true;
            else 
                return ((item as User).fullName.IndexOf(searchFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void searchFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView _pView_ = (CollectionView)CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource);
            _pView_.Filter = itemFilter;
            CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource).Refresh();

        }

        private void addNew_Click(object sender, RoutedEventArgs e)
        {

            multiInputTextWindow _inputWindow = new multiInputTextWindow();
            _inputWindow.ShowDialog();

            if (_inputWindow.all_ok)
            {
                string ID = _inputWindow.new_name.ToUpper() + _inputWindow.new_mat + _inputWindow.new_time;
                string fullnamehrlp = _inputWindow.new_date +
                        "_" + _inputWindow.new_time + "_" + _inputWindow.new_name + "_" +
                        _inputWindow.new_mat + "_" + _inputWindow.new_fac + "_" + _inputWindow.new_org + "_Read_Me.txt";
                mainitems.Add(new User { ExpID =ID, Date = _inputWindow.new_date,
                    Email = _inputWindow.new_email, Material= _inputWindow.new_mat,
                    RadFacility = _inputWindow.new_fac, Organization= _inputWindow.new_org,
                    Scientist = _inputWindow.new_name, Time=_inputWindow.new_time, fullName=fullnamehrlp });
                using (StreamWriter writer2 = new StreamWriter(fullnamehrlp))
                {
                    writer2.WriteLine(ID);
                    writer2.WriteLine(" has been conducted at " + _inputWindow.new_fac + " under supervision of " + _inputWindow.new_name + ".");
                }

                    System.Windows.Forms.MessageBox.Show(_inputWindow.new_name + " has been added to your list", "Confirmation of adding a new user ", MessageBoxButtons.OK);

            }
            _inputWindow.Close();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            User sel_user = (User)lvDataBinding.SelectedItems[0];
            string text = File.ReadAllText(@"C: \Users\Amitos\Desktop\WPF and XAML\1\firstproject\firstproject\bin\Debug\" +sel_user.fullName );
           

                System.Windows.Forms.MessageBox.Show(text);



        }
    }
    public class User
    {
        public string ExpID { get; set; }
        public string Date { get; set; }
        public string Email { get; set; }
        public string RadFacility { get; set; }
        public string Organization { get; set; }
        public string Material { get; set; }
        public string Scientist { get; set; }
        public string Time { get; set; }

        public string fullName { get; set; }

        public string item;
    }
    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
            Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
            Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            this.Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                (
                    AdornedElement.RenderSize.Width - 15,
                    (AdornedElement.RenderSize.Height - 5) / 2
                );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (this.Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(System.Windows.Media.Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }
}
