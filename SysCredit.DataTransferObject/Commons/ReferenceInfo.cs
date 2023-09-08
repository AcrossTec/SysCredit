﻿namespace SysCredit.DataTransferObject.Commons;

using SysCredit.Enums;

public record class ReferenceInfo : IDataTransferObject
{
    public long ReferenceId { get; set; }

    public string? Identification { get; set; }

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Address { get; set; }
}