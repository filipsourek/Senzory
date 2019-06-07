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
        //privátní proměnné
        private double xAccelerationValue, yAccelerationValue, zAccelerationValue;

        public AccelerometerViewModel()
        {
            //Command pro zapnutí snímání ze sensoru
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
            //Command pro vypnutí snímání ze sensoru
            AccerelationStopCommand = new Command(() =>
            {
                Accelerometer.Stop();
            });

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;

        }

        //property pro X hodnotu
        public double XAccelerationValue
        {

            get { return xAccelerationValue; }
            set
            {
                xAccelerationValue = value;
                OnPropertyChanged("XAccelerationValue");
            }
        }

        //property pro Y hodnotu
        public double YAccelerationValue
        {

            get { return yAccelerationValue; }
            set
            {
                yAccelerationValue = value;
                OnPropertyChanged("YAccelerationValue");
            }
        }

        //property pro Y hodnotu
        public double ZAccelerationValue
        {

            get { return zAccelerationValue; }
            set
            {
                zAccelerationValue = value;
                OnPropertyChanged("ZAccelerationValue");
            }
        }

        //Commandy
        public Command AccerelationStopCommand { get; set; }

        public Command AccerelationStartCommand { get; set; }
        

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        

        //Event pokud se změní nějaká hodnota, tak je pošle
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