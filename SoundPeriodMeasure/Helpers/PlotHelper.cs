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
            var plotModel = new PlotModel {Title = "Wyniki pomiarów"};

            plotModel.Axes.Add(new LinearAxis {Position = AxisPosition.Bottom});
            plotModel.Axes.Add(new LinearAxis {Position = AxisPosition.Left});
            plotModel.Background = OxyColors.White;
            plotModel.PlotAreaBorderThickness = new OxyThickness(2, 2, 2, 2);
            plotModel.PlotAreaBorderColor = OxyColors.Black;

            return plotModel;
        }

        public static LineSeries CreateLineSeries(AmplitudeInTime[] values, int lastFilledIndex = -1)
        {
            if (lastFilledIndex == -1 || values.Length - 1 < lastFilledIndex)
            {
                lastFilledIndex = values.Length;
            }

            var series = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerStroke = OxyColors.Black
            };

            //AmplitudeInTime[] valuesToPlot = values.Where(v => v != null && v.Amplitude > 0).ToArray();
            //int startOfPlot = -1;
            //bool valuesIncreasing = false;

            //for (int j = 0; j < lastFilledIndex; j++)
            //{
            //    if (valuesIncreasing)
            //    {
            //        valuesToPlot[j - startOfPlot] = values[j];
            //        continue;
            //    }
            //    if (Math.Abs(values[j].Amplitude) > 0.1)
            //    {
            //        startOfPlot = j;
            //        valuesToPlot = new AmplitudeInTime[lastFilledIndex - j];
            //        valuesToPlot[0] = values[j];
            //        valuesIncreasing = true;
            //    }
            //}
            //if (startOfPlot == -1)
            //{
            //    return series;
            //}

            int i = 0;
            foreach (var value in values)
            {
                series.Points.Add(new DataPoint(value.ElapsedMiliseconds, value.Amplitude));
                if (i++ > 9000)
                {
                    break;
                }
            }          

            return series;
        }

        //public static LineSeries CreateLineSeriesFromByte(byte[] values)
        //{
        //    var series = new LineSeries
        //    {
        //        MarkerType = MarkerType.Circle,
        //        MarkerSize = 2,
        //        MarkerStroke = OxyColors.Black
        //    };


        //    int startOfPlot = -1;
        //    byte[] valuesToPlot = new byte[0];
        //    bool valuesIncreasing = false;

        //    for (int j = 0; j < values.Length; j++)
        //    {
        //        if (valuesIncreasing)
        //        {
        //            valuesToPlot[j - startOfPlot] = values[j];
        //            continue;
        //        }
        //        if (Math.Abs(values[j]) > 0.1)
        //        {
        //            startOfPlot = j;
        //            valuesToPlot = new byte[values.Length - j];
        //            valuesToPlot[0] = values[j];
        //            valuesIncreasing = true;
        //        }
        //    }
        //    if (startOfPlot == -1)
        //    {
        //        return series;
        //    }

        //    int i = 0;
        //    foreach (var value in valuesToPlot)
        //    {
        //        series.Points.Add(new DataPoint(i, value));
        //        if (i++ > 90000)
        //        {
        //            break;
        //        }
        //    }

        //    return series;
        //}

    }
}