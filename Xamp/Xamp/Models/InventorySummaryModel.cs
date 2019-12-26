using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xamp.Models
{
    public class InventorySummaryModel
    {
        public int MaterialId { get; set; }
        public string Material
        {
            get
            {
                return _Material.ToUpper();
            }
            set
            {
                _Material = value;
            }
        }
        private string _Material;
        public decimal Quantity { get; set; }
        public decimal Weight { get; set; }
    }
}