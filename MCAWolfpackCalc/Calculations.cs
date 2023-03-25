using System;

namespace MCAWolfpackCalc
{
    internal class Calculations
    {
        public static double CalculateTargetRange(double mastheight, double mils, bool? zoomed)
        {
            return zoomed switch
            {
                true => Math.Round(mastheight / mils * 4 * 0.9167, 1),
                false => Math.Round(mastheight / mils * 0.9167, 1),
                _ => 0,
            };
        }

        public static double CalculateTargetSpeed(double shiplength, double seconds)
        {
            return Math.Round(shiplength / seconds * 1.944, 1);
        }
    }
}
