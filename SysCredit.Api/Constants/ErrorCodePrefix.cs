namespace SysCredit.Api.Constants;

using Middlewares;

using Managers;

using Stores;

/// <summary>
///     Prefijos usados para los códigos de error.
/// </summary>
public static class ErrorCodePrefix
{
    #region Managers
    /// <summary>
    ///     Prefijo usado para los Servicios.
    /// </summary>
    private const string SERV = nameof(SERV);

    /// <summary>
    ///     Prefijo para la clase: <see cref="GuarantorManager" />
    /// </summary>
    public const string GuarantorManagerPrefix = $"{SERV}G";

    /// <summary>
    ///     Prefijo para la clase: <see cref="CustomerManager" />
    /// </summary>
    public const string CustomerManagerPrefix = $"{SERV}C";

    /// <summary>
    ///     Prefijo para la clase: <see cref="LoanTypeManager" />
    /// </summary>
    public const string LoanTypeManagerPrefix = $"{SERV}LT";

    /// <summary>
    ///     Prefijo para la clase: <see cref="PaymentFrequencyManager" />
    /// </summary>
    public const string PaymentFrequencyManagerPrefix = $"{SERV}PF";

    /// <summary>
    ///     Prefijo para la clase: <see cref="ReferenceManager" />
    /// </summary>
    public const string ReferenceManagerPrefix = $"{SERV}R";

    /// <summary>
    ///     Prefijo para la clase: <see cref="RelationshipManager" />
    /// </summary>
    public const string RelationshipManagerPrefix = $"{SERV}RS";

    // /// <summary>
    // ///     Prefijo para la clase: <see cref="AuthenticationService" />
    // /// </summary>
    // public const string AuthenticationServicePrefix = $"{SERV}AS";
    #endregion

    #region Stores
    /// <summary>
    ///     Prefijo usado para los Stores.
    /// </summary>
    private const string DATA = nameof(DATA);

    /// <summary>
    ///     Prefijo para la clase: <see cref="CustomerStore" />
    /// </summary>
    public const string CustomerStorePrefix = $"{DATA}C";

    /// <summary>
    ///     Prefijo para la clase: <see cref="GuarantorStore" />
    /// </summary>
    public const string GuarantorStorePrefix = $"{DATA}G";

    /// <summary>
    ///     Prefijo para la clase: <see cref="LoanTypeStore" />
    /// </summary>
    public const string LoanTypeStorePrefix = $"{DATA}LT";

    /// <summary>
    ///     Prefijo para la clase: <see cref="PaymentFrequencyStore" />
    /// </summary>
    public const string PaymentFrequencyStorePrefix = $"{DATA}PF";

    // /// <summary>
    // ///     Prefijo para la clase: <see cref="ReferenceStore" />
    // /// </summary>
    // public const string ReferenceStorePrefix = $"{DATA}R";

    /// <summary>
    ///     Prefijo para la clase: <see cref="RelationshipStore" />
    /// </summary>
    public const string RelationshipStorePrefix = $"{DATA}RS";

    // /// <summary>
    // ///     Prefijo para la clase: <see cref="RoleStore" />
    // /// </summary>
    // public const string RoleStorePrefix = $"{DATA}ARS";

    // /// <summary>
    // ///     Prefijo para la clase: <see cref="UserStore" />
    // /// </summary>
    // public const string UserStorePrefix = $"{DATA}AUS";

    #endregion

    /// <summary>
    ///     Prefijo para la clase: <see cref="SysCreditMiddleware" />.
    /// </summary>
    public const string InternalServerErrorPrefix = "MID";

    /// <summary>
    ///     Prefijo para todas las clases derivadas: <see cref="FluentValidation.AbstractValidator{T}" />
    /// </summary>
    public const string AbstractValidatorPrefix = "ERR";
}
