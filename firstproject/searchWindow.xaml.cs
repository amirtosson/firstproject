﻿using System;
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
            string help;
            for (int i = 0; i < 20; ++i)
            {
                help = RandomString(randNum.Next(4, 9));
                items.Add(new User()
                {
                    Name = help + "_" + randNum.Next(1, 600).ToString(),
                    Age = randNum.Next(10, 60),
                    Email = help.ToLower() + i.ToString() + "@bedan.com"
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
                return ((item as User).Name.IndexOf(searchFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
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
                mainitems.Add(new User { Name =_inputWindow.new_name, Age = _inputWindow.new_age, Email = _inputWindow.new_email});
            }
            _inputWindow.Close();
            System.Windows.Forms.MessageBox.Show(_inputWindow.new_name+" has been added to your list", "Confirmation of adding a new user ");

        }

    }
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
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