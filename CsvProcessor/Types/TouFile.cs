using CsvHelper.Configuration.Attributes;
using CsvProcessor.Interfaces;
using System;

namespace CsvProcessor.Types
{
    public class TouFile : CsvFileBase, ICsvFile
    {
        public decimal Energy { get; set; }
        [Name("Maximum Demand")]
        public decimal MaximumDemand { get; set; }
        [Name("Time of Max Demand")]
        public DateTime TimeOfmaxDemand { get; set; }
        public string Period { get; set; }
        [Name("DLS Active")]
        public bool DlsActive { get; set; }
        [Name("Billing Reset Count")]
        public int BillingResetCount { get; set; }
        [Name("Billing Reset Date/Time")]
        public DateTime BillingResetDateTime { get; set; }
        public string Rate { get; set; }
    }
}
