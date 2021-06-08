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
        public static CultureInfo Culture { get; set; } = new CultureInfo("en-US");
        public static double MaterialHandlerWage { get; set; } = 20000;
        public static double WarehouseWorkerWage { get; set; } = 10000;
        public static double WarehousePackerWage { get; set; } = 15000;
    }
}
