namespace SysCredit.Api.Validations.Authentications.Users;

using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

public class AsyncUserUniquePhoneValidator<T> : AsyncPropertyValidator<T, string?>
{
    public override Task<bool> IsValidAsync(FluentValidation.ValidationContext<T> Context, string? Value, CancellationToken cancellation)
    {
        // var User = await Context.RootContextData[nameof(UserStore)].AsStore<User>().FetchUserByPhoneAsync(Value);
        // return User is null;
        return Task.FromResult(false);
    }

    public override string Name => "AsyncUserUniquePhoneValidator";

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor.";
    }
}
