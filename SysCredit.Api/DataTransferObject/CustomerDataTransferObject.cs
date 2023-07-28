﻿namespace SysCredit.Api.DataTransferObject;

public record class CustomerDataTransferObject
{
    public long CustomerId { get; set; }

    public string Identification { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public string BussinessType { get; set; } = string.Empty;

    public string BussinessAddress { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public IEnumerable<GuarantorDataTransferObject> Guarantors { get; set; } = Enumerable.Empty<GuarantorDataTransferObject>();
}