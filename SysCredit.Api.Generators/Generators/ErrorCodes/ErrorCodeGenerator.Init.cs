﻿namespace SysCredit.Api.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System.Text;

/// <summary>
///     Generador del Api estandar de los códigos de errores para SysCredit.Api.
/// </summary>
internal partial class ErrorCodeGenerator
{
    /// <summary>
    ///     Atributos que serán generados.
    /// </summary>
    private const string ErrorCodePrefixAttributeSource = @"
//------------------------------------------------------------------------------
// <auto-generated>                                                             
//     This code was generated by a tool.                                       
//     Runtime Version: 1.0.0.0                                                 
//                                                                              
//     Changes to this file may cause incorrect behavior and will be lost if    
//     the code is regenerated.                                                 
// </auto-generated>                                                            
//------------------------------------------------------------------------------

namespace SysCredit.Api.Attributes;

/// <summary>
///     Prefijos usados para los códigos de errores: <see cref=""SysCredit.Constants.ErrorCodes"" />.
/// </summary>
/// <param name=""Prefix"">
///     Prefijo que será usado para los campos de <see cref=""SysCredit.Constants.ErrorCodes"" />.
/// </param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ErrorCodePrefixAttribute(string Prefix) : Attribute
{
    /// <summary>
    ///     Nombre del prefijo usado para la lista de errores en <see cref=""SysCredit.Constants.ErrorCodes"" />.
    /// </summary>
    public readonly string Prefix = Prefix;
}";

    private const string ErrorCodeRangeAttributeSource = @"
//------------------------------------------------------------------------------
// <auto-generated>                                                             
//     This code was generated by a tool.                                       
//     Runtime Version: 1.0.0.0                                                 
//                                                                              
//     Changes to this file may cause incorrect behavior and will be lost if    
//     the code is regenerated.                                                 
// </auto-generated>                                                            
//------------------------------------------------------------------------------

namespace SysCredit.Api.Attributes;

/// <summary>
///     Estable el rango mínimo y máximo de los códigos de errores.
/// </summary>
/// <param name=""MinCodeNumber"">
///     Número de código de error mínimo.
/// </param>
/// <param name=""MaxCodeNumber"">
///     Número de código de error máximo.
/// </param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
public class ErrorCodeRangeAttribute(uint MinCodeNumber, uint MaxCodeNumber) : Attribute
{
    /// <summary>
    ///     Número de código de error mínimo.
    /// </summary>
    public readonly uint MinCodeNumber = MinCodeNumber;

    /// <summary>
    ///     Número de código de error máximo.
    /// </summary>
    public readonly uint MaxCodeNumber = MaxCodeNumber;
}";

    /// <summary>
    ///     Crea los atributos usados por este generador.
    /// </summary>
    /// <param name="Context">
    ///     Context passed to an incremental generator when it has registered an output via <see cref="IncrementalGeneratorInitializationContext.RegisterPostInitializationOutput(Action{IncrementalGeneratorPostInitializationContext})" />.
    /// </param>
    private static void AddErrorCodeAttributes(IncrementalGeneratorPostInitializationContext Context)
    {
        Context.AddSource(Helpers.FileName("ErrorCodeRangeAttribute"), SourceText.From(ErrorCodeRangeAttributeSource, Encoding.UTF8));
        Context.AddSource(Helpers.FileName("ErrorCodePrefixAttribute"), SourceText.From(ErrorCodePrefixAttributeSource, Encoding.UTF8));
    }
}
