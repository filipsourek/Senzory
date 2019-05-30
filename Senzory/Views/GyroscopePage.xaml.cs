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
    public partial class GyroscopePage : ContentPage
    {
        readonly CircularGaugeView xCircularGauge, yCircularGauge, zCircularGauge;

        public GyroscopePage()
        {
            Icon = "Gyroscope";
            Title = "Gyroscope";

            xCircularGauge = new CircularGaugeView("X", -20, 20);
            yCircularGauge = new CircularGaugeView("Y", -20, 20);
            zCircularGauge = new CircularGaugeView("Z", -20, 20);

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

            InitializeGyroscope();
        }

        void InitializeGyroscope()
        {
            if (!Gyroscope.IsMonitoring)
            {
                try
                {
                    Gyroscope.Start(SensorSpeed.Default);

                    Gyroscope.ReadingChanged += HandleGyroscopeReadingChanged;
                }
                catch (FeatureNotSupportedException)
                {
                    Debug.WriteLine("Gyroscope Unavailable");
                    Device.BeginInvokeOnMainThread(async () => { await DisplayAlert("Error", "Gyroscope sensor is not available", "Ok"); });

                }
            }

        }

        void HandleGyroscopeReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                xCircularGauge.Pointer.Value = e.Reading.AngularVelocity.X;
                yCircularGauge.Pointer.Value = e.Reading.AngularVelocity.Y;
                zCircularGauge.Pointer.Value = e.Reading.AngularVelocity.Z;
            });
        }
    }
}