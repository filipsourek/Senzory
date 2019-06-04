using System;
using System.Collections.Generic;
using System.Text;

namespace Senzory.Model
{
    public class MagnetometerModel
    {
        public double PrevodNaStupne(double x, double y)
        {
            return 180 - Math.Atan2(x, y) / 0.0174532925;
        }
    }
}
