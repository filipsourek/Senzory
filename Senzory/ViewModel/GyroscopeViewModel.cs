using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Senzory.ViewModel
{
    class GyroscopeViewModel : INotifyPropertyChanged
    {
        private double xAngularVelocityValue, yAngularVelocityValue, zAngularVelocityValue;

        public GyroscopeViewModel()
        {
            AngularVelocityStartCommand = new Command((object speed) =>
            {
                if (!Gyroscope.IsMonitoring)
                {
                    try
                    {
                        Gyroscope.Start(SensorSpeed.UI);
                    }
                    catch (FeatureNotSupportedException)
                    {

                    }
                }

            });
            AngularVelocityStopCommand = new Command(() =>
            {
                Gyroscope.Stop();
            });

            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;

        }



        public double XAngularVelocityValue
        {

            get { return xAngularVelocityValue; }
            set
            {
                xAngularVelocityValue = value;
                OnPropertyChanged("XAngularVelocityValue");
            }
        }


        public double YAngularVelocityValue
        {

            get { return yAngularVelocityValue; }
            set
            {
                yAngularVelocityValue = value;
                OnPropertyChanged("YAngularVelocityValue");
            }
        }


        public double ZAngularVelocityValue
        {

            get { return zAngularVelocityValue; }
            set
            {
                zAngularVelocityValue = value;
                OnPropertyChanged("ZAngularVelocityValue");
            }
        }


        public ICommand AngularVelocityStopCommand { get; set; }

        public ICommand AngularVelocityStartCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                XAngularVelocityValue = e.Reading.AngularVelocity.X;
                YAngularVelocityValue = e.Reading.AngularVelocity.Y;
                ZAngularVelocityValue = e.Reading.AngularVelocity.Z;
            });
        }

    }
}

