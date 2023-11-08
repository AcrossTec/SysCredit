namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;
using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
///     Clase validadora del Request para verificar la existencia de una relación de parentesco por medio del Id.
/// </summary>
/// <typeparam name="T">
///     Tipo del store.
/// </typeparam>
public class AsyncExistsRelationshipValidator<T> : AsyncPropertyValidator<T, long>
{
    /// <summary>
    ///     Valida si existe una relación de parentesco por medio del Id.
    /// </summary>
    /// <param name="Context">
    ///     Obtiene el objeto donde fue usado el validador.
    /// </param>
    /// <param name="RelationshipId">
    ///     El Id de la relación a validar.
    /// </param>
    /// <param name="Cancellation">
    ///     Método para cancelar la validación.
    /// </param>
    /// <returns>
    ///     Retorna true si la relación de parentesco existe.
    /// </returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long RelationshipId, CancellationToken Cancellation)
    { 
        var Relationship = await Context.RootContextData[nameof(RelationshipStore)].AsStore<Relationship>().FetchRelationshipByIdAsync(RelationshipId);
        return Relationship is not null;
    }

    /// <summary>
    ///     Nombre del validador.
    /// </summary>
    public override string Name => "AsyncExistsRelationshipValidator";
}
