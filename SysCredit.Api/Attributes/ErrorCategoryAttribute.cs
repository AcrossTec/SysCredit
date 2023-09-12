﻿namespace SysCredit.Api.Attributes;

/// <summary>
/// 
/// </summary>
/// <param name="Category"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ErrorCategoryAttribute(string Category) : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public readonly string Category = Category;
}
