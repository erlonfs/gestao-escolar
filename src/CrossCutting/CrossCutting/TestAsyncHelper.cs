using System;
using System.Threading.Tasks;

namespace CrossCutting
{
	public class TestAsyncHelper
	{
		public static void CallSync(Action target)
		{
			var task = new Task(target);
			task.RunSynchronously();
		}
	}
}
