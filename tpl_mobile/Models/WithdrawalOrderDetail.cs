using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tpl_mobile.Models
{
    public class WithdrawalOrderDetail
    {
        public int Id { get; set; }
        public string WithdrawalOrderNumber { get; set; }
        public DateTime WithdrawalOrderDate { get; set; }  
        public List<WithdrawalOrderMaterial> WithdrawalOrderMaterial { get; set; }
        public short? IsApproved { get; set; }
    }

    public class WithdrawalOrderMaterial
    {
        public string MaterialName { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Weight { get; set; }
        public string Batch { get; set; }
    }
}