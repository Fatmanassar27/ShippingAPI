namespace ShippingAPI.DTOS.DashBoardDTOs
{
    public class DashboardStatsDto
    {
        public int NewOrders { get; set; } // جديد
        public int PendingOrders { get; set; } // قيد الانتظار
        public int DeliveredOrders { get; set; } // تم التسليم
        public int DeliveredToRepresentativeOrders { get; set; } // تم التسليم للمندوب
        public int UnreachableOrders { get; set; } // لا يمكن الوصول
        public int PostponedOrders { get; set; } // تم التأجيل
        public int PartiallyDeliveredOrders { get; set; } // تم التسليم جزئيا
        public int CancelledByUserOrders { get; set; } // تم الإلغاء من قبل المستخدم
        public int RejectedWithPaymentOrders { get; set; } // تم الرفض مع الدفع
        public int RejectedWithPartialPaymentOrders { get; set; } // رفض مع سداد جزء
        public int RejectedWithoutPaymentOrders { get; set; } // رفض ولم يتم الدفع
        public decimal TotalRevenue { get; set; }
        public decimal TotalCost { get; set; }
    }
}
