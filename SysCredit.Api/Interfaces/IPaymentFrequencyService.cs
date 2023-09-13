﻿namespace SysCredit.Api.Interfaces;

using SysCredit.Api.ViewModels.PaymentFrequencies;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

public interface IPaymentFrequencyService
{
    IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync();

    ValueTask<PaymentFrequencyInfo> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId);

    ValueTask<IServiceResult<bool>> UpdatePaymentFrequencyAsync(long PaymentFrequencyId, UpdatePaymentFrequencyRequest Request);

    IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync();

    ValueTask<IServiceResult<EntityId?>> InsertPaymentFrequencyAsync(CreatePaymentFrequencyRequest ViewModel);
}