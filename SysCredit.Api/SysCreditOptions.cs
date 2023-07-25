using SysCredit.Api.ViewModels;

namespace SysCredit.Api;

public class SysCreditOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public PagingOptions PagingOptions { get; set; } = new();
}

