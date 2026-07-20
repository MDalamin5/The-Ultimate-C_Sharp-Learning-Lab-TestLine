using System;

namespace MasteringOOP.ShoppingCartProject;

// 1. Inherit from the base Exception class
public class InvalidCartItemException : Exception
{
    // 2. Pass the message to the base class
    public InvalidCartItemException(string message) : base(message)
    {
    }

    // 3. Optional: Add a constructor that takes an InnerException
    public InvalidCartItemException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}