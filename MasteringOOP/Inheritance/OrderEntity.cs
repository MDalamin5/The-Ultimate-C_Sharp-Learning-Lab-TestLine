using System;

namespace MasteringOOP.Inheritance{

    public class OrderEntity : AuditableEntity
    {
        public decimal TotalAmount {get; private set;}
        public string OrderStatus {get; private set;}

        public OrderEntity(decimal totalAmount, string orderStatus, string createdBy) :base (createdBy)
        {
            TotalAmount = totalAmount;
            OrderStatus = orderStatus;
        }

        public override string ToString()
        {
            return $"[{Id}] {TotalAmount} ({OrderStatus}) - Created By: {CreatedBy} at {CreatedAt:yyyy-MM-dd HH:mm}";
        }
    }
}
