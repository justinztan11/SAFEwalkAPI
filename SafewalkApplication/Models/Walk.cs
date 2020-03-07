using System;
using System.Collections.Generic;

namespace SafewalkApplication.Models
{
    public partial class Walk
    {
        public string Id { get; set; }
        public string UserEmail { get; set; }
        public string WalkerEmail { get; set; }
        public DateTime? Time { get; set; }
        public string StartText { get; set; }
        public decimal? StartLat { get; set; }
        public decimal? StartLng { get; set; }
        public string DestText { get; set; }
        public decimal? DestLat { get; set; }
        public decimal? DestLng { get; set; }
        public decimal? WalkerCurrLat { get; set; }
        public decimal? WalkerCurrLng { get; set; }
        public int? Status { get; set; }
    }
}
