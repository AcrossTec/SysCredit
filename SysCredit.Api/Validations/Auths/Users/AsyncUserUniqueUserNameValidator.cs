namespace SysCredit.Api.Validations.Auths.Users;

using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

public class AsyncUserUniqueUserNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    public override async Task<bool> IsValidAsync(FluentValidation.ValidationContext<T> Context, string? Value, CancellationToken cancellation)
    {
        var User = await Context.RootContextData[nameof(UserStore)].AsStore<User>().FetchUserByUserNameAsync(Value);
        return User is null;
    }

    public override string Name => "AsyncUserUniqueUserNameValidator";

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor.";
    }
}