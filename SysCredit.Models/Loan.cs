namespace SysCredit.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///     Modelo de la tabla Loan.
/// </summary>
public record class Loan : Entity
{
    /// <summary>
    ///    Id de Prestamo. 
    /// </summary>
    public long LoanId { get; set; }

    /// <summary>
    ///     Id del Tipo de prestamo.
    /// </summary>
    public long LoanTypeId { get; set; }

    /// <summary>
    ///     Id de la Frequencia de pago.
    /// </summary>
    public long PaymentFrequencyId { get; set; }
    
    /// <summary>
    ///     Id del Cliente
    /// </summary>
    public long CustomerId { get; set; }

    /// <summary>
    ///     Monto del préstamo.
    /// </summary>
    public decimal LoanAmount { get; set; }

    /// <summary>
    ///     Cantidad de pagos programados.
    /// </summary>
    public int QtyPayments { get; set; }

    /// <summary>
    ///     Valor de cada pago programado.
    /// </summary>
    public decimal PaymentValue { get; set; }

    /// <summary>
    ///     Valor del primer pago programado.
    /// </summary>
    public decimal FirstPaymentValue { get; set; }

    /// <summary>
    ///     Tasa de interés del préstamo.
    /// </summary>
    public decimal LoanRate { get; set; }

    /// <summary>
    ///     Monto total del préstamo incluyendo intereses.
    /// </summary>
    public decimal TotalLoanAmount { get; set; }

    /// <summary>
    ///     Monto del pago diario.
    /// </summary>
    public decimal PaymentAmountPerDay { get; set; }

    /// <summary>
    ///     Monto del pago mensual.
    /// </summary>
    public decimal PaymentAmountPerMonth { get; set; }

    /// <summary>
    ///     Fecha de la primera visita o pago programado.
    /// </summary>
    public DateTime DateFirstVisit { get; set; }

    /// <summary>
    ///     Fecha de finalización del pago del préstamo.
    /// </summary>
    public DateTime DateEndPayment { get; set; }

    /// <summary>
    ///     Notas o comentarios adicionales.
    /// </summary>
    public string Notes { get; set; } = String.Empty;

    /// <summary>
    ///     Fecha en que se otorgó el préstamo.
    /// </summary>
    public DateTime LoanDate { get; set; }
}