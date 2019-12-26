using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tpl_mobile.Models
{
    public class InventorySummaryModel
    {
        public int MaterialId { get; set; }
        public string Material { get; set; }
        public decimal Quantity { get; internal set; }
        public decimal Weight { get; internal set; }
    }
}