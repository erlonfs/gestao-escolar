using Xunit;

namespace Demo.GestaoEscolar.WebApplication.Test
{
    [CollectionDefinition("Integration test collection")]
    public class IntegrationTestCollection : ICollectionFixture<TestSetup>
    {
    }
}
