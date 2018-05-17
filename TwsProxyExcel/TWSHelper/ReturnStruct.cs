using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWSHelper
{
    class OrderStatusReturnStruct
    {
        public int orderId;
        public string status;
        public double filled;
        public double remaining;
        public double avgFillPrice;
        public int permId;
        public int parentId;
        public double lastFillPrice;
        public int clientId;
        public string whyHeld;

        public OrderStatusReturnStruct(int orderId, string status, double filled, double remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld)
        {
            this.orderId = orderId;
            this.status = status;
            this.filled = filled;
            this.remaining = remaining;
            this.avgFillPrice = avgFillPrice;
            this.permId = permId;
            this.parentId = parentId;
            this.lastFillPrice = lastFillPrice;
            this.clientId = clientId;
            this.whyHeld = whyHeld;
        }
        public OrderStatusReturnStruct() { }
    }
}
