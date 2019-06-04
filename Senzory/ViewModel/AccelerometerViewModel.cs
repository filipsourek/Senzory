using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Senzory.ViewModel
{
    public class AccelerometerViewModel : INotifyPropertyChanged
    {
        private double xAccelerationValue, yAccelerationValue, zAccelerationValue;

        public AccelerometerViewModel()
        {
            AccerelationStartCommand = new Command((object speed) =>
            {
                if (!Accelerometer.IsMonitoring)
                {
                    try
                    {
                        Accelerometer.Start(SensorSpeed.UI);
                    }
                    catch (FeatureNotSupportedException)
                    {

                    }
                }
            });
            AccerelationStopCommand = new Command(() =>
            {
                Accelerometer.Stop();
            });

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;

        }


        public double XAccelerationValue
        {

            get { return xAccelerationValue; }
            set
            {
                xAccelerationValue = value;
                OnPropertyChanged("XAccelerationValue");
            }
        }


        public double YAccelerationValue
        {

            get { return yAccelerationValue; }
            set
            {
                yAccelerationValue = value;
                OnPropertyChanged("YAccelerationValue");
            }
        }


        public double ZAccelerationValue
        {

            get { return zAccelerationValue; }
            set
            {
                zAccelerationValue = value;
                OnPropertyChanged("ZAccelerationValue");
            }
        }


        public ICommand AccerelationStopCommand { get; set; }

        public ICommand AccerelationStartCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        
        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                XAccelerationValue = e.Reading.Acceleration.X;
                YAccelerationValue = e.Reading.Acceleration.Y;
                ZAccelerationValue = e.Reading.Acceleration.Z;
            });
        }   
    }
}