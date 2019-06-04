using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Senzory.Model;


namespace Senzory.ViewModel
{
    public class BarometerViewModel : INotifyPropertyChanged
    {
        private double pressureInHectopascalsValue;

        public BarometerViewModel()
        {
            PressureInHectopascalsStartCommand = new Command((object speed) =>
            {
                if (!Barometer.IsMonitoring)
                {
                    try
                    {
                        Barometer.Start(SensorSpeed.UI);
                    }
                    catch (FeatureNotSupportedException)
                    {

                    }
                }

            });
            PressureInHectopascalsStopCommand = new Command(() =>
            {
                Barometer.Stop();
            });

            Barometer.ReadingChanged += Barometer_ReadingChanged;


        }

        public double PressureInHectopascalsValue
        {

            get { return pressureInHectopascalsValue; }
            set
            {
                pressureInHectopascalsValue = value;
                OnPropertyChanged("XMagneticFieldValue");
            }
        }

        public ICommand PressureInHectopascalsStopCommand { get; set; }

        public ICommand PressureInHectopascalsStartCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        private void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PressureInHectopascalsValue = e.Reading.PressureInHectopascals;
            });
        }
    }
}