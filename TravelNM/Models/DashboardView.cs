using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelNM.Models
{
    public class DashboardView
    {
        public int TotalCities { get; set; }

        public int TotalPackages { get; set; }

        public int TotaSoldPackages { get; set; }

        public int TotalCustomers { get; set; }

        public int TotaSoldPackagesPaid { get; set; }

        public int TotaSoldPackagesWaitingPayment { get; set; }

        public int TotaSoldPackagesCanceled { get; set; }
    }
}