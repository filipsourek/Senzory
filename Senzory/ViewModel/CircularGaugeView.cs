using Syncfusion.SfGauge.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;

namespace Senzory
{
    public class CircularGaugeView : SfCircularGauge
    {
        public CircularGaugeView(string headerText, double startValue, double endValue)
        {
            Pointer = new NeedlePointer { AnimationDuration = 0.5, Color = Color.Black };

            var header = new Header
            {
                Text = headerText,
                ForegroundColor = Color.Black
            };

            var circularGaugeScale = new Scale
            {
                Interval = (endValue - startValue) / 10,
                StartValue = startValue,
                EndValue = endValue,
                ShowTicks = true,
                ShowLabels = true,
                RimColor = Color.Black,

                Pointers = { Pointer },
                MinorTicksPerInterval = 4,
            };

            Scales = new ObservableCollection<Scale> { circularGaugeScale };
            Headers = new ObservableCollection<Header> { header };
        }

        public NeedlePointer Pointer { get; }
    }
}
