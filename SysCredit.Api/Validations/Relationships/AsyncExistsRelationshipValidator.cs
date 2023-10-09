namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class AsyncExistsRelationshipValidator<T> : AsyncPropertyValidator<T, long>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long RelationshipId, CancellationToken Cancellation)
    {
        var Relationship = await Context.RootContextData[nameof(RelationshipStore)].AsStore<Relationship>().FetchRelationshipByIdAsync(RelationshipId);
        return Relationship is not null;
    }

    /// <summary>
    ///     En caso de error lanza el mensaje con el codigo de error
    /// </summary>
    /// <param name="ErrorCode"></param>
    /// <returns></returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "El {PropertyName} no existe";
    }

    /// <summary>
    ///     Nombre del validador
    /// </summary>
    public override string Name => "AsyncExistsRelationshipValidator";
}
