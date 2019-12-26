using System;
using System.Collections.Generic;
using System.Text;

namespace Xamp.Models
{
    class WithdrawalOrder
    {
        public int Id { get; set; }
        public string WarehouseCode { get; set; }
        public DateTime WithdrawalOrderDate { get; set; }
        public string WithdrawalOrderNumber { get; set; }
        public Decimal NetWeight { get; set; }
        public short? IsApproved { get; set; }
        public string ApproveStatus
        {
            get
            {
                string retval = "Un-Approve";
                short isApproved = (short)(IsApproved == null ? 0 : IsApproved);

                if (isApproved == -1)
                {
                    retval = "Approve";
                }

                return retval;
            }
        }
    }
}
