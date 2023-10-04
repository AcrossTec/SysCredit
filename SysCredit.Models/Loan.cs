namespace SysCredit.Models;

public record class Loan : Entity
{
    public long LoanId { get; set; }

    public long LoanTypeId { get; set; }

    public long PaymentFrequencyId { get; set; }

    public long CustomerId { get; set; }

    public decimal LoanAmount { get; set; }

    public int QtyPayments { get; set; }

    public decimal PaymentValue { get; set; }

    public decimal FirstPaymentValue { get; set; }

    public decimal LoanRate { get; set; }

    public decimal TotalLoanAmount { get; set; }

    public decimal PaymentAmountPerDay { get; set; }

    public decimal PaymentAmountPerMonth { get; set; }

    public DateTime DateFirstVisit { get; set; }

    public DateTime DateEndPayment { get; set; }

    public string Notes { get; set; } = String.Empty;

    public DateTime LoanDate { get; set; }
}
