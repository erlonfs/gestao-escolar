using System;
using System.Threading.Tasks;

namespace CrossCutting
{
	public class TestAsyncHelper
	{
		public static void CallSync(Action target)
		{

			Task.Run(() => target()).Wait();

			//var task = new Task(target);
			//task.RunSynchronously();
		}
	}
}
