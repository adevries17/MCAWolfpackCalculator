namespace MCAWolfpackCalc {
    internal class Calculations {
        public static double CalculateTargetRange(double mastheight, double mils, bool? zoomed) {
            return zoomed switch {
                true => mastheight / mils * 4 * 0.9167,
                false => mastheight / mils * 0.9167,
                _ => 0,
            };
        }

        public static double CalculateTargetSpeed(double shiplength, double seconds) {
            return shiplength / seconds * 1.944;
        }
    }
}
