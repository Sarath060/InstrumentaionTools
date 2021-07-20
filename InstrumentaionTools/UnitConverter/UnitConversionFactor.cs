using System;
using System.Collections.Generic;


namespace InstrumentaionTools.UnitConverter
{

    class UnitConversionFactor
    {
        public Dictionary<string, double> conversionFactors = new Dictionary<string, double>();
        public string unit = "";
        public double value = 0.0;
        public double ConvertUnit(double value, string fromunit, string tounit)
        {
            double convertedvalue = 0.0;

            double fromunitfactor = conversionFactors[fromunit];
            double tounitfactor = conversionFactors[tounit];

            convertedvalue = (value / fromunitfactor) * (tounitfactor);

            return convertedvalue;
        }

    }

    class Length : UnitConversionFactor
    {
        public Length()
        {
            conversionFactors = new Dictionary<string, double>
        {
            { "fm", 1000000000000000},
            { "pm" , 1000000000000},
            { "nm" , 1000000000},
            { "um" , 1000000},
            { "mm" , 1000},
            { "cm" , 100},
            { "m" , 1.0},
            { "dam" , 0.1},
            { "hm" , 0.01},
            { "km" , 0.001},
            { "Mm" , 0.000001},
            { "Gm" , 0.000000001},
            { "Tm" , 0.000000000001},
            { "Pm" , 0.000000000000001},

            { "inch" , 39.3701},
            { "ft" , 3.28084},
            { "yd" , 1.09361},
            { "mi" , 0.000621371},

            { "nautMi" , 1.0 / 1852.0},
            { "lightYear" , 1.0 / (9.4607304725808 * (Math.Pow(10,15))) }
        };
        }
    }

    class Pressure : UnitConversionFactor
    {

        public Pressure()
        {
            conversionFactors = new Dictionary<string, double>
            {
                { "bar" , 1.0},
                { "mbar" , 1000.0},
                { "ubar" , 1000000.0},
                { "Pa" , 100000.0},
                { "hPa" , 1000.0},
                { "kPa" , 100.0},
                { "MPa" , 0.1},
                { "kgcm2" , 1.01972},
                { "atm" , 0.986923},
                { "mmHg" , 750.062},
                { "mmH2O" , 10197.162129779},
                { "mH2O" , 10.197162129779},
                { "psi" , 14.5038},
                { "ftH2O" , 33.455256555148},
                { "inH2O" , 401.865},
                { "inHg" , 29.53}
            };
        }
    }

    class EngTime : UnitConversionFactor
    {
        public EngTime()
        {
            conversionFactors = new Dictionary<string, double>
            {
                { "ms" , 1000.0 },
                { "s" , 1.0 },
                { "minute" , 1.0 / 60.0 },
                { "hr" , 1.0 / 60.0 / 60.0 },
                { "day" , 1.0 / 60.0 / 60.0 / 24.0 }
            };
        }
    }

    class Current : UnitConversionFactor
    {
        public Current()
        {
            conversionFactors = new Dictionary<string, double>
            {
                { "ms" , 1000.0},
                { "s" , 1.0},
                { "minute" , 1.0 / 60.0},
                { "hr" , 1.0 / 60.0 / 60.0},
                { "day" , 1.0 / 60.0 / 60.0 / 24.0 }
            };
        }
    }

    class Mass : UnitConversionFactor
    {
        public Mass()
        {
            conversionFactors = new Dictionary<string, double>
            {
                { "kg" , 1.0},
                { "g" , 1000.0},
                { "mg" , 1000000.0},
                { "metricTon" , 1.0 / 1000.0},
                { "lb" , 2.2046226218},
                { "oz" , 35.274},
                { "grain" , 2.2046226218 * 7000.0},
                { "shortTon" , 1.0 / 907.185},
                { "longTon" , 1.0 / 1016.047},
                { "slug" , 1.0 / 14.5939029}
            };
        }
    }

    class Force : UnitConversionFactor
    {
        public Force()
        {
            conversionFactors = new Dictionary<string, double>
            {
                { "N" , 1.0},
                { "kN" , 1.0 / 1000.0},
                { "MN" , 1.0 / 1000000.0},
                { "GN" , 1.0 / 1000000000.0},
                { "gf" , 1.019716213e+2},
                { "kgf" , 1.019716213e-1},
                { "dyn" , 1e+5},
                { "J/m" , 1.0},
                { "J/cm" , 100.0},
                { "shortTonF" , 1.124045e-4},
                { "longTonF" , 1.003611e-4},
                { "kipf" , 2.248089e-4},
                { "lbf" , 2.248089431e-1},
                { "ozf" , 3.5969430896},
                { "pdf" , 7.2330138512}
            };
        }
    }

    class Power : UnitConversionFactor
    {
        public Power()
        {
            conversionFactors = new Dictionary<string, double>
            {
                { "kW" , 1.0},
                { "BTU/hr" , 3412.14},
                { "BTU/min" , 56.869},
                { "BTU/sec" , 0.94781666666},
                { "cal/sec" , 238.85},
                { "cal/min" , 238.85 * 60},
                { "cal/hr" , 238.85 * 60 * 60},
                { "erg/sec" , 10e9},
                { "erg/min" , 10e9 * 60},
                { "erg/hr" , 10e9 * 60 * 60},
                { "ftlb/sec" , 737.56},
                { "GW" , 1e-6},
                { "MW" , 1e-3},
                { "kCal/sec" , 0.24},
                { "kCal/min" , 0.24 * 60},
                { "kCal/hr" , 0.24 * 60 * 60},
                { "mW" , 1e6},
                { "W" , 1e3},
                { "VA" , 1e3},
                { "hp_mech" , 1.3410220888},
                { "hp_ele" , 1.3404825737},
                { "hp_metric" , 1.359621617304},
                { "metric_ton_ref" , 0.259},
                { "US_ton_ref" , 0.2843451361},
                { "J/sec" , 1000.0},
                { "J/min" , 1000.0 * 60},
                { "J/hr" , 1000.0 * 60 * 60},
                { "kgf-m/sec" , 101.97162129779}
            };
        }
    }

}