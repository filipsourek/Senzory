using Senzory.MenuItems;
using Senzory.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Senzory
{

    public partial class MainPage : MasterDetailPage
    {

        public List<PageItem> MenuList { get; set; }

        public MainPage()
        {
            InitializeComponent();

            MenuList = new List<PageItem>();

            var homePage = new PageItem() { Title = "Home Page", Icon = "", TargetType = typeof(HomePage) };
 

            MenuList.Add(homePage);
   

            navigationDrawerList.ItemsSource = MenuList;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(HomePage)));
        }


        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (PageItem)e.SelectedItem;
            Type page = item.TargetType;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}