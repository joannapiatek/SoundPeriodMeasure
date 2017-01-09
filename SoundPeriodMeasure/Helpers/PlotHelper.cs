using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace SoundPeriodMeasure.Helpers
{
    public static class PlotHelper
    {
        public static PlotModel CreatePlotModel()
        {
            var plotModel = new PlotModel { Title = "Wyniki pomiarów" };

            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 10, Minimum = 0 });
            plotModel.Background = OxyColors.White;
            plotModel.PlotAreaBorderThickness = new OxyThickness(2, 2, 2, 2);
            plotModel.PlotAreaBorderColor = OxyColors.Black;

            return plotModel;
        }

        public static LineSeries CreateLineSeries(double[] values)
        {
            var series = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerStroke = OxyColors.Black
            };


            int startOfPlot = -1;
            double[] valuesToPlot = new double[0];
            bool valuesIncreasing = false;

            for (int j = 0; j < values.Length; j++)
            {
                if (valuesIncreasing)
                {
                    valuesToPlot[j - startOfPlot] = values[j];
                    continue;
                }
                if (Math.Abs(values[j]) > 0.1)
                {
                    startOfPlot = j;
                    valuesToPlot = new double[values.Length - j];
                    valuesToPlot[0] = values[j];
                    valuesIncreasing = true;
                }
            }
            if (startOfPlot == -1)
            {
                return series;
            }

            int i = 0;
            foreach (var value in valuesToPlot)
            {
                series.Points.Add(new DataPoint(i, value));
                i++;
            }

            //series.Points.Add(new DataPoint(0.0, 6.0));
            //series.Points.Add(new DataPoint(1.4, 2.1));
            //series.Points.Add(new DataPoint(2.0, 4.2));
            //series.Points.Add(new DataPoint(3.3, 2.3));
            //series.Points.Add(new DataPoint(4.7, 7.4));
            //series.Points.Add(new DataPoint(6.0, 6.2));
            //series.Points.Add(new DataPoint(8.9, 8.9));
            //series.Points.Add(new DataPoint(0.0, 6.0));
            //series.Points.Add(new DataPoint(1.4, 2.1));
            //series.Points.Add(new DataPoint(2.0, 4.2));
            //series.Points.Add(new DataPoint(3.3, 2.3));
            //series.Points.Add(new DataPoint(4.7, 7.4));
            //series.Points.Add(new DataPoint(6.0, 6.2));
            //series.Points.Add(new DataPoint(8.9, 8.9));

            return series;
        }

        
    }
}