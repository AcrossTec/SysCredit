﻿namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;

/// <summary>
/// 
/// </summary>
/// <param name="RelationshipStore"></param>
/// <param name="Logger"></param>
[Service<IRelationshipService>]
[ErrorCategory(nameof(RelationshipService))]
[ErrorCodePrefix(RelationshipServicePrefix)]
public class RelationshipService(IStore<Relationship> RelationshipStore, ILogger<RelationshipService> Logger) : IRelationshipService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync()
    {
        Logger.LogInformation("[SERVICE] {Service}.{Method}()", nameof(RelationshipService), nameof(FetchRelationshipAsync));
        return RelationshipStore.FetchRelationshipAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="RelationshipId"></param>
    /// <returns></returns>
    public ValueTask<bool> ExistsRelationshipAsync(long RelationshipId)
    {
        return RelationshipStore.ExistsRelationshipAsync(RelationshipId);
    }

    [MethodId("A2368CB1-B428-472B-9BAC-C117A6F84808")]
    public ValueTask<RelationshipInfo?> FetchRelationshipByLoanTypeIdAsync(long LoanTypeId)
    {
        return RelationshipStore.FetchRelationshipByLoanTypeIdAsync(LoanTypeId);
    }
}
