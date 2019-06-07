using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Senzory.ViewModel
{
    public class MagnetometerViewModel : INotifyPropertyChanged
    {
        private double xMagneticFieldValue, yMagneticFieldValue, zMagneticFieldValue, result;

        public MagnetometerViewModel()
        {
            MagneticFieldStartCommand = new Command((object speed) =>
            {
                if (!Magnetometer.IsMonitoring)
                {
                    try
                    {
                        Magnetometer.Start(SensorSpeed.UI);
                    }
                    catch (FeatureNotSupportedException)
                    {

                    }
                }

            });
            MagneticFieldStopCommand = new Command(() =>
            {
                Magnetometer.Stop();
            });

            Magnetometer.ReadingChanged += Magnetometer_ReadingChanged;


        }

        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }

        public double XMagneticFieldValue
        {

            get { return xMagneticFieldValue; }
            set
            {
                xMagneticFieldValue = value;
                OnPropertyChanged("XMagneticFieldValue");
            }
        }
        public double YMagneticFieldValue
        {

            get { return yMagneticFieldValue; }
            set
            {
                yMagneticFieldValue = value;
                OnPropertyChanged("YMagneticFieldValue");
            }
        }

        public double ZMagneticFieldValue
        {

            get { return zMagneticFieldValue; }
            set
            {
                zMagneticFieldValue = value;
                OnPropertyChanged("ZMagneticFieldValue");
            }
        }

        public Command MagneticFieldStopCommand { get; set; }

        public Command MagneticFieldStartCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }


        private void Magnetometer_ReadingChanged(object sender, MagnetometerChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                XMagneticFieldValue = e.Reading.MagneticField.X;
                YMagneticFieldValue = e.Reading.MagneticField.Y;
                ZMagneticFieldValue = e.Reading.MagneticField.Z;

                Result = 180 - Math.Atan2(XMagneticFieldValue, YMagneticFieldValue) / 0.0174532925;
            });
        }
    }
}