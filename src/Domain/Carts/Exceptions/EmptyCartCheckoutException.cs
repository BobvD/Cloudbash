using System;

namespace Cloudbash.Domain.Carts.Exceptions
{
    public class EmptyCartCheckOutException : Exception
    {
        public EmptyCartCheckOutException(): base(ErrorMessage())
        {
        }

        private static string ErrorMessage()
        {
            return "Cannot check out a empty cart.";
        }
    }
}
