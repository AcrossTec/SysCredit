﻿namespace SysCredit.Api.Models;

public record class Guarantor : Entity
{
    public long GuarantorId { get; set; }

    public string Identification { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public string BussinessType { get; set; } = string.Empty;

    public string BussinessAddress { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public long RelationshipId { get; set; }
}