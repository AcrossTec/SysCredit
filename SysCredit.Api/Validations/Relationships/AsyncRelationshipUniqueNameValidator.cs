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
///     Clase validadora del nombre del Request para crear un parentesco entre el cliente y el fiador.
/// </summary>
/// <typeparam name="T">
///     Tipo del store.
/// </typeparam>
public class AsyncRelationshipUniqueNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    /// <summary>
    ///     Valida que el nombre del parentesco sea único.
    /// </summary>
    /// <param name="Context"> 
    ///     Obtiene el objeto donde fue usado el validador.
    /// </param>
    /// <param name="Name">
    ///     Nombre de la relación a validar.
    /// </param>
    /// <param name="Cancellation">
    ///     Método para cancelar la validación.
    /// </param>
    /// <returns>
    ///     Retorna true  si la relación de parentesco no existe.
    /// </returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Name, CancellationToken Cancellation)
    {
        var Relationship = await Context.RootContextData[nameof(RelationshipStore)].AsStore<Relationship>().FetchRelationshipByNameAsync(Name);
        return Relationship is null;
    }

    /// <summary>
    ///     Nombre del validador.
    /// </summary>
    public override string Name => "AsyncRelationshipUniqueNameValidator";
}

