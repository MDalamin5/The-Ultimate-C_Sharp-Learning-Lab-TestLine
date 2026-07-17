using System;
namespace MasteringOOP.Interface;

    // An interface has NO access modifiers (public/private) on its members.
    // It has NO code body. It is purely a contract.
    public interface INotificationService
    {
        void SendReceipt(string customerEmail, decimal amount);
    }
