using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace StockControl
{
    public static class SettingsParams
    {
        public static double Tax { get; set; } = 0.17;
        public static CultureInfo Culture { get; set; } = new CultureInfo("es-US");
    }
}
