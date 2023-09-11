namespace SysCredit.Api.Tests.Stores;

using Moq;

using SysCredit.Api.Stores;
using SysCredit.Api.Tests.Mocks;
using SysCredit.Models;

/// <summary>
///     Unit testing C# in .NET Core using dotnet test and xUnit
///     https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
/// </summary>
public class StoreTests
{
    private readonly Mock<IStore> StoreMock = new Mock<IStore>();

    public StoreTests()
    {
        StoreMock.Setup(Store => Store.Connection).Returns(new DbConnectionMock());
        StoreMock.Setup(Store => Store.Dispose()).Callback(delegate { });
    }

    [Fact]
    public void CreateStoreTest()
    {
        IStore Store = StoreMock.Object;
        Assert.True(true);
    }
}