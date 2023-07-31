﻿namespace SysCredit.Api.DataTransferObject;

using SysCredit.Api.Enums;

public class GuarantorDataTransferObject : IDataTransferObject
{
    public long GuarantorId { get; set; }

    public string Identification { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public string? Email { get; set; }

    public string Address { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public string BussinessType { get; set; } = string.Empty;

    public string BussinessAddress { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public long RelationshipId { get; set; }

    public string RelationshipName { get; set; } = string.Empty;
}