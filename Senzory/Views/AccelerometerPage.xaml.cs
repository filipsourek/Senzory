using System;
using System.Collections.Generic;
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
    public partial class AccelerometerPage : ContentPage
    {
        readonly CircularGaugeView xCircularGauge, yCircularGauge, zCircularGauge;

        public AccelerometerPage()
        {
            Icon = "Accelerometer";
            Title = "Accelerometer";

            xCircularGauge = new CircularGaugeView("X", -1.5, 1.5);
            yCircularGauge = new CircularGaugeView("Y", -1.5, 1.5);
            zCircularGauge = new CircularGaugeView("Z", -1.5, 1.5);

            var grid = new Grid
            {
                Margin = new Thickness(0, 20),
                RowDefinitions = {
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
            },
                ColumnDefinitions = {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
            }
            };
            grid.Children.Add(xCircularGauge, 0, 0);
            grid.Children.Add(yCircularGauge, 0, 1);
            grid.Children.Add(zCircularGauge, 0, 2);

            Content = grid;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitializeAccelerometer();
        }

        void InitializeAccelerometer()
        {
            try
            {
                if (!Accelerometer.IsMonitoring)
                {
                    Accelerometer.Start(SensorSpeed.Default);
                }

                Accelerometer.ReadingChanged += HandleAccelerometerReadingChanged;
            }
            catch (FeatureNotSupportedException)
            {
                Debug.WriteLine("Accelerometer Unavailable");
                Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Error", "Accelerometer sensor is not available", "Ok"); });

            }
        }

        void HandleAccelerometerReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                xCircularGauge.Pointer.Value = e.Reading.Acceleration.X;
                yCircularGauge.Pointer.Value = e.Reading.Acceleration.Y;
                zCircularGauge.Pointer.Value = e.Reading.Acceleration.Z;
            });
        }
    }
}
