namespace SysCredit.Api.Constants;

using Middlewares;

using Services;

using Stores;

/// <summary>
///     Prefijos usados para los códigos de error.
/// </summary>
public static class ErrorCodePrefix
{
    #region Services
    /// <summary>
    ///     Prefijo usado para los Servicios.
    /// </summary>
    private const string SERV = nameof(SERV);

    /// <summary>
    ///     Prefijo para la clase: <see cref="GuarantorService" />
    /// </summary>
    public const string GuarantorServicePrefix = $"{SERV}G";

    /// <summary>
    ///     Prefijo para la clase: <see cref="CustomerService" />
    /// </summary>
    public const string CustomerServicePrefix = $"{SERV}C";

    /// <summary>
    ///     Prefijo para la clase: <see cref="LoanTypeService" />
    /// </summary>
    public const string LoanTypeServicePrefix = $"{SERV}LT";

    /// <summary>
    ///     Prefijo para la clase: <see cref="PaymentFrequencyService" />
    /// </summary>
    public const string PaymentFrequencyServicePrefix = $"{SERV}PF";

    /// <summary>
    ///     Prefijo para la clase: <see cref="ReferenceService" />
    /// </summary>
    public const string ReferenceServicePrefix = $"{SERV}R";

    /// <summary>
    ///     Prefijo para la clase: <see cref="RelationshipService" />
    /// </summary>
    public const string RelationshipServicePrefix = $"{SERV}RS";

    /// <summary>
    ///     Prefijo para la clase: <see cref="AuthenticationService" />
    /// </summary>
    public const string AuthenticationServicePrefix = $"{SERV}AS";

    /// <summary>
    ///     Prefijo para la clase: <see cref="LoanService"/>
    /// </summary>
    public const string LoanServicePrefix = $"{SERV}L";
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

    /// <summary>
    ///     Prefijo para la clase: <see cref="ReferenceStore" />
    /// </summary>
    public const string ReferenceStorePrefix = $"{DATA}R";

    /// <summary>
    ///     Prefijo para la clase: <see cref="RelationshipStore" />
    /// </summary>
    public const string RelationshipStorePrefix = $"{DATA}RS";

    /// <summary>
    ///     Prefijo para la clase: <see cref="RoleStore" />
    /// </summary>
    public const string RoleStorePrefix = $"{DATA}ARS";

    /// <summary>
    ///     Prefijo para la clase: <see cref="UserStore" />
    /// </summary>
    public const string UserStorePrefix = $"{DATA}AUS";

    /// <summary>
    ///     Prefijo para la clase: <see cref="LoanStore"/>
    /// </summary>
    public const string LoanStorePrefix = $"{DATA}L";
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
