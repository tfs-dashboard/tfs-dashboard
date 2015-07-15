using System;

namespace tfs_dashboard.Models.Errors
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException() {}

        public InvalidInputException(string message) : base(message) {}
    }
}