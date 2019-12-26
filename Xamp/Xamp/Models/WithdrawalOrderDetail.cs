using System;
using System.Collections.Generic;
using System.Text;

namespace Xamp.Models
{
    public class WithdrawalOrderDetail
    {
        public int Id { get; set; }
        public string WithdrawalOrderNumber { get; set; }
        public DateTime WithdrawalOrderDate { get; set; }
        public short? IsApproved { get; set; }
        public string ApproveStatus
        {
            get
            {
                string retval = "Un-Approve";
                short isApproved = (short)(IsApproved == null ? 0 : IsApproved);

                if (isApproved == 0)
                {
                    retval = "Approve";
                }

                return retval;
            }
        }
        public List<WithdrawalOrderMaterial> WithdrawalOrderMaterial { get; set; }
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