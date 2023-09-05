namespace SysCredit.Api.Validations.Auth.Users;

using FluentValidation.Validators;
using SysCredit.Api.Extensions;
using SysCredit.Api.Stores.Auth;
using SysCredit.Models.Auth.Users;

public class AsyncUserUniqueEmailValidator<T> : AsyncPropertyValidator<T, string?>
{
    public override async Task<bool> IsValidAsync(FluentValidation.ValidationContext<T> Context, string? Value, CancellationToken cancellation)
    {
        var User = await Context.RootContextData[nameof(UserStore)].AsStore<User>().FetchUserByEmailAsync(Value);
        return User is null;
    }
    public override string Name => "AsyncUserUniqueEmailValidator";

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor.";
    }
}
