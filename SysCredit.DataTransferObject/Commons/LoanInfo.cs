namespace SysCredit.DataTransferObject.Commons;

public record class LoanInfo
{
    public long LoanId { get; set; }

    public long LoanTypeId { get; set; }

    public string LoanTypeName { get; set; } = string.Empty;

    public long PaymentFrequencyId { get; set; }

    public string PaymentFrequencyName { get; set; } = string.Empty;

    public decimal LoanAmount { get; set; }

    public long QtyPayments { get; set; }

    public decimal PaymentValue { get; set; }

    public decimal FirstPaymentValue { get; set; }

    public decimal LoanRate { get; set; }

    public decimal TotalLoanAmount { get; set; }

    public DateTime PaymentAmountPerDay { get; set; }

    public DateTime DateFirstVisit { get; set; }

    public DateTime DateEndPayment { get; set; }

    public long Notes { get; set; }
}