namespace SysCredit.Api.Extensions;

using SysCredit.Api.Middlewares;

/// <summary>
///     Configuraciones extras del Servidor antes de ser ejecutado.
/// </summary>
public static class WebApplicationExtensions
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    /// <summary>
    ///     Configura el uso de SwaggerUI.
    /// </summary>
    /// <param name="App">
    ///     Objeto <see cref="WebApplication" /> que contiene toda la información relacionada al servidor.
    /// </param>
    /// <returns>
    ///     Regresa el mismo valor de <paramref name="App" /> para habilitar la invocación en cadena.
    /// </returns>
    public static WebApplication UseSysCreditSwaggerUI(this WebApplication App)
    {
        if (App.Environment.IsDevelopment())
        {
            App.UseSwagger();
            App.UseSwaggerUI();
        }

        App.MapSwagger();
        return App;
    }

    /// <summary>
    ///     Configura los Middleware usados por SysCredit.
    /// </summary>
    /// <param name="App">
    ///     Objeto <see cref="WebApplication" /> que contiene toda la información relacionada al servidor.
    /// </param>
    /// <returns>
    ///     Regresa el mismo valor de <paramref name="App" /> para habilitar la invocación en cadena.
    /// </returns>
    public static WebApplication UseSysCreditMiddlewares(this WebApplication App)
    {
        App.UseMiddleware<SysCreditMiddleware>();
        return App;
    }
}
