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
            var accelerometerPage = new PageItem() { Title = "Accelerometer", Icon = "", TargetType = typeof(AccelerometerPage) };
            var barometerPage = new PageItem() { Title = "Barometer", Icon = "", TargetType = typeof(BarometerPage) };



            MenuList.Add(homePage);
            MenuList.Add(accelerometerPage);
            MenuList.Add(barometerPage);




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