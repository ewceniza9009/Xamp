using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tpl_mobile.Models
{
    public class WithdrawalOrder
    {
        public int Id { get; set; }
        public string WarehouseCode { get; set; }
        public DateTime WithdrawalOrderDate { get; set; }
        public string WithdrawalOrderNumber { get; set; }
        public Decimal NetWeight { get; set; }
        public short? IsApproved { get; set; }
    }
}