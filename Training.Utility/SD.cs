using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Utility
{
    public static class SD
    {
        public const string ManagerRole = "Manager";
        public const string FrontDeskRole = "Front";
        public const string KitchenRole = "Kitchen";
        public const string CutromerRole = "Customer";

        public const string StatusPending = "Pending_Payment";
        public const string StatusApproved = "Payment_Approved"; //payment aproved
        public const string StatusRejected = "Payment_Rejected"; //payment rejected
        public const string StatusProcessing = "Being Prepared";
        public const string StatusReady = "Ready for delivery";
        public const string StatusDelivered = "Delivered";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";
        
    }
}
