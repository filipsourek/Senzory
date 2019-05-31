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
    public partial class MagnetometerPage : ContentPage
    {

        NeedlePointer needlePointer;
        public MagnetometerPage()
        {
            Icon = "Magnetometer";
            Title = "Magnetometer";

            InitializeComponent();
            SfCircularGauge circularGauge = new SfCircularGauge();

            ObservableCollection<Scale> scales = new ObservableCollection<Scale>();
            Scale scale = new Scale();

            scale.StartValue = 0;
            scale.EndValue = 350;
            scale.StartAngle = -90;
            scale.SweepAngle = 350;


            needlePointer = new NeedlePointer();
            scale.Pointers.Add(needlePointer);

            scales.Add(scale);
            circularGauge.Scales = scales;

            this.Content = circularGauge;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitializeMagnetometer();
        }

        void InitializeMagnetometer()
        {
            if (!Magnetometer.IsMonitoring)
            {                
                try
                {
                    Magnetometer.Start(SensorSpeed.UI);

                    Magnetometer.ReadingChanged += HandleMagnetometerReadingChanged;
                }
                catch (FeatureNotSupportedException)
                {
                    Debug.WriteLine("Magnetometer Unavailable");
                    Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Error", "Magnetometer sensor is not available", "Ok"); });

                }
            }

        }

        void HandleMagnetometerReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                double x = e.Reading.MagneticField.X;
                double y = e.Reading.MagneticField.Y;
                double z = e.Reading.MagneticField.Z;
                double result = 180 - Math.Atan2(x, y) / 0.0174532925;
                needlePointer.Value = result;
            });
        }
    }
}
