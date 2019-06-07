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
        //List kam se ukládají jednotlivé stránky
        public List<PageItem> MenuList { get; set; }

        public MainPage()
        {
            InitializeComponent();

            MenuList = new List<PageItem>();

            //Vytvoøení jednotlivých stránek
            var homePage = new PageItem() { Title = "Home Page", Icon = "home.png", TargetType = typeof(HomePage) };
            var accelerometerPage = new PageItem() { Title = "Accelerometer", Icon = "accelerator.png", TargetType = typeof(AccelerometerPage) };
            var barometerPage = new PageItem() { Title = "Barometer", Icon = "", TargetType = typeof(BarometerPage) };
            var gyroscoperPage = new PageItem() { Title = "Gyroscope", Icon = "", TargetType = typeof(GyroscopePage) };
            var magnetometerPage = new PageItem() { Title = "Magnetometer", Icon = "", TargetType = typeof(MagnetometerPage) };

            //Pøidání stránek do listu
            MenuList.Add(homePage);
            MenuList.Add(accelerometerPage);
            MenuList.Add(barometerPage);
            MenuList.Add(gyroscoperPage);
            MenuList.Add(magnetometerPage);


            navigationDrawerList.ItemsSource = MenuList;
            //Nastavení hlavní stránky
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