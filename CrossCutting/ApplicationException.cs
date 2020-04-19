using System;

namespace CrossCutting
{
	public class ApplicationException : Exception
    {
		public ApplicationException(string message) : base(message)
		{
		}
    }

	public class ServiceMessageBusUnavailableException : Exception
	{
		public ServiceMessageBusUnavailableException() : base()
		{
		}
	}
}
