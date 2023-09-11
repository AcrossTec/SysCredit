namespace SysCredit.Api.Tests.Controllers;

using Microsoft.Extensions.Logging;

using Moq;

using SysCredit.Api.Controllers;
using SysCredit.Api.Interfaces;

using System.Threading.Tasks;

public class LoanTypeControllerTests
{
    private readonly Mock<ILoanTypeService> LoanTypeServiceMock = new Mock<ILoanTypeService>();
    private readonly Mock<ILogger<LoanTypeController>> LoggerMock = new Mock<ILogger<LoanTypeController>>();

    public LoanTypeControllerTests()
    {
    }

    [Fact]
    public async Task FetchLoanTypeAsyncTest()
    {
        var Controller = new LoanTypeController(LoanTypeServiceMock.Object, LoggerMock.Object);
        var Response = await Controller.FetchLoanTypeAsync();
    }
}
