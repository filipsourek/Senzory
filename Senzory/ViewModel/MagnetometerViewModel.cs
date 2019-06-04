using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Senzory.Model;


namespace Senzory.ViewModel
{
    public class MagnetometerViewModel : INotifyPropertyChanged
    {
        private double xMagneticFieldValue, yMagneticFieldValue, zMagneticFieldValue, result;
        //private MagnetometerModel magnetometerModel;

        //public Command PrevodNaStupne { get; private set; }

        public MagnetometerViewModel()
        {
            //PrevodNaStupne = new Command(Stupne);
            //magnetometerModel = new MagnetometerModel();
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

        /*
        private void Stupne()
        {
            Vypis(magnetometerModel.PrevodNaStupne(XMagneticFieldValue, yMagneticFieldValue));
        }
        */
        

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

        public ICommand MagneticFieldStopCommand { get; set; }

        public ICommand MagneticFieldStartCommand { get; set; }

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