using System;
namespace MasteringOOP.Polymorphism
{
    public class CryptoProcessor : PaymentProcessor
    {
        private string _walletAddress;

        public CryptoProcessor(string WalletAddress)
        {
            _walletAddress = WalletAddress;
        }

        public override void ValidateTransaction(decimal amount)
        {
            base.ValidateTransaction(amount);

            if(!_walletAddress.Contains("0x"))
                throw new ArgumentException("Must contain 0x inside your address...");
            else
                Console.WriteLine("Your address has valid...");

            
                
        }

        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Transferring {amount} USDT to wallet address {_walletAddress} via Ethereum network...");
               
        }
    }
}