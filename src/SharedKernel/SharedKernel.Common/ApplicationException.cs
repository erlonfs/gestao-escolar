using System;

namespace SharedKernel.Common
{
	public class ApplicationException : Exception
    {
		public ApplicationException(string message) : base(message)
		{
		}
    }
}
