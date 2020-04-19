using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Demo.GestaoEscolar.WebApplication.Test
{
	public abstract class TestBase : IClassFixture<ProgramTest<FakeStartup>>
	{
		protected WebApplicationFactory<FakeStartup> Factory { get; }

		public TestBase(ProgramTest<FakeStartup> factory)
		{
			Factory = factory;
		}
	}
}
