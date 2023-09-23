﻿namespace SysCredit.Api.Generators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static Constants;
using static System.String;

public class SourceBuilder
{
    private string? AutoGenerated = null, Namespace = null;
    private readonly List<string> Usings = new(), StaticUsings = new();
    private readonly List<ClassBuilder> Classes = new();

    private SourceBuilder() { }

    public static SourceBuilder Create() => new();

    public SourceBuilder DeclareAutoGenerated()
    {
        this.AutoGenerated = Constants.AutoGenerated;
        return this;
    }

    public SourceBuilder DeclareNamespace(string Namespace)
    {
        this.Namespace = $"namespace {Namespace};";
        return this;
    }

    public SourceBuilder DeclareUsing(string Using)
    {
        this.Usings.Add($"using {Using};");
        return this;
    }

    public SourceBuilder DeclareStaticUsing(string Using)
    {
        this.StaticUsings.Add($"using static {Using};");
        return this;
    }

    public ClassBuilder AddClass(string Name)
    {
        var Builder = new ClassBuilder(Name);
        this.Classes.Add(Builder);
        return Builder;
    }

    public string Build()
    {
        var Builder = new StringBuilder();

        if (AutoGenerated is not null)
        {
            Builder.AppendLine(AutoGenerated);
        }

        if (Namespace is not null)
        {
            Builder.AppendLine($"{Namespace}{NewLine}");
        }

        if (Usings.Any())
        {
            Usings.Sort();
            Builder.AppendLine($"{Join(NewLine, Usings)}{NewLine}");
        }

        if (StaticUsings.Any())
        {
            StaticUsings.Sort();
            Builder.AppendLine($"{Join(NewLine, StaticUsings)}{NewLine}");
        }

        Builder.AppendLine(Join(NewLine, Classes.Select(Class => Class.Build())));
        return Builder.ToString();
    }
}

public class ClassBuilder(string Name)
{
    private int TabCount = 0;
    private bool IsPublic, IsStatic, IsAbstract, IsSealed, IsPartial;
    private readonly List<FieldBuilder> Fields = new();

    public ClassBuilder Public()
    {
        this.IsPublic = true;
        return this;
    }

    public ClassBuilder Internal()
    {
        this.IsPublic = false;
        return this;
    }

    public ClassBuilder Partial()
    {
        this.IsPartial = true;
        return this;
    }

    public ClassBuilder Static()
    {
        this.IsStatic = true;
        this.IsAbstract = this.IsSealed = false;
        return this;
    }

    public ClassBuilder Abstract()
    {
        this.IsAbstract = true;
        this.IsStatic = this.IsSealed = false;
        return this;
    }

    public ClassBuilder Sealed()
    {
        this.IsSealed = true;
        this.IsStatic = this.IsAbstract = false;
        return this;
    }

    public ClassBuilder PrefixTabCount(int TabCount)
    {
        this.TabCount = TabCount;
        return this;
    }

    public ClassBuilder AddFieldFactory(Action<ClassBuilder> Factory)
    {
        Factory.Invoke(this);
        return this;
    }

    public ClassBuilder AddFieldFactory<TContext>(Action<ClassBuilder, TContext> Factory, TContext Context)
    {
        Factory.Invoke(this, Context);
        return this;
    }

    public FieldBuilder AddField(string Name)
    {
        FieldBuilder Builder = new(Name);
        this.Fields.Add(Builder);
        return Builder;
    }

    public string Build()
    {
        var Builder = new StringBuilder();
        string Tabs = new(' ', TabCount * Tab.Length);

        Builder.Append(Tabs);
        Builder.Append($"{(this.IsPublic ? "public" : "internal")}");
        Builder.Append($"{(this.IsStatic ? " static" : Empty)}");
        Builder.Append($"{(this.IsSealed ? " sealed" : Empty)}");
        Builder.Append($"{(this.IsAbstract ? " abstract" : Empty)}");
        Builder.Append($"{(this.IsPartial ? " partial" : Empty)} class {Name}{NewLine}");
        Builder.AppendLine($"{Tabs}{{");
        Builder.AppendLine(Join(NewLine, this.Fields.Select(Field => $"{Tabs}{Tab}{Field.Build()}")));
        Builder.AppendLine($"{Tabs}}}");

        return Builder.ToString();
    }
}

public class FieldBuilder(string FieldName)
{
    private bool IsPublic, IsPrivate, IsProtected, IsInternal;
    private bool IsStatic, IsReadOnly, IsConst;
    private string? FieldType, FieldValue;

    public FieldBuilder Public()
    {
        this.IsPublic = true;
        this.IsPrivate = this.IsProtected = this.IsInternal = false;
        return this;
    }

    public FieldBuilder Private()
    {
        this.IsPrivate = true;
        this.IsPublic = this.IsProtected = this.IsInternal = false;
        return this;
    }

    public FieldBuilder Protected()
    {
        this.IsProtected = true;
        this.IsPublic = this.IsPrivate = this.IsInternal = false;
        return this;
    }

    public FieldBuilder Internal()
    {
        this.IsInternal = true;
        this.IsPublic = this.IsPrivate = this.IsProtected = false;
        return this;
    }

    public FieldBuilder Static()
    {
        this.IsStatic = true;
        this.IsConst = false;
        return this;
    }

    public FieldBuilder ReadOnly()
    {
        this.IsReadOnly = true;
        this.IsConst = false;
        return this;
    }

    public FieldBuilder Const()
    {
        this.IsConst = true;
        this.IsStatic = this.IsReadOnly = false;
        return this;
    }

    public FieldBuilder OfType(string Type)
    {
        this.FieldType = Type;
        return this;
    }

    public FieldBuilder WithValue(string Value)
    {
        this.FieldValue = Value;
        return this;
    }

    public string Build()
    {
        var Builder = new StringBuilder();
        Builder.Append(IsPublic ? "public" : IsPrivate ? "private" : IsProtected ? "protected" : IsInternal ? "internal" : Empty);

        if (IsConst)
        {
            Builder.Append(" const");
        }
        else
        {
            Builder.Append(IsStatic ? " static" : Empty);
            Builder.Append(IsReadOnly ? " readonly" : Empty);
        }

        Builder.Append($" {FieldType} {FieldName} = {FieldValue};");
        return Builder.ToString();
    }
}
