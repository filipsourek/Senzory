using Syncfusion.SfGauge.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Senzory.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarometerPage : ContentPage
    {

        NeedlePointer needlePointer;
        Label test;
        public BarometerPage()
        {
            Icon = "Barometer";
            Title = "Barometer";


            InitializeComponent();
            SfCircularGauge circularGauge = new SfCircularGauge();

            ObservableCollection<Scale> scales = new ObservableCollection<Scale>();
            Scale scale = new Scale();
            test = new Label();

            scale.StartValue = 800;
            scale.EndValue = 1200;

            needlePointer = new NeedlePointer();
            scale.Pointers.Add(needlePointer);

            scales.Add(scale);
            circularGauge.Scales = scales;

            this.Content = circularGauge;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitializeBarometer();
        }

        void InitializeBarometer()
        {
            if (!Barometer.IsMonitoring)
            {
                try
                {
                    Barometer.Start(SensorSpeed.UI);

                    Barometer.ReadingChanged += HandleBarometerReadingChanged;
                }
                catch (FeatureNotSupportedException)
                {
                    Debug.WriteLine("Barometer Unavailable");
                    Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Error", "Barometer sensor is not available", "Ok"); });
                }
            }

        }

        void HandleBarometerReadingChanged(object sender, BarometerChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                needlePointer.Value = e.Reading.PressureInHectopascals;
            });
        }
    }
}
