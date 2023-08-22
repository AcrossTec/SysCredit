namespace SysCredit.Mobile.Services.Https;

using DynamicData.Binding;

using Microsoft.Extensions.Logging;

using SysCredit.Helpers;

using SysCredit.Mobile.Models;
using SysCredit.Mobile.Models.Customers.Creates;
using SysCredit.Mobile.Settings;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SysCreditApiService : ISysCreditApiService
{
    private readonly HttpClient RestClient;
    private readonly JsonSerializerOptions SerializerOptions;
    private readonly ILogger<SysCreditApiService> Logger;

    public SysCreditApiService(ILogger<SysCreditApiService> Logger)
    {
        this.Logger = Logger;
        this.RestClient = new HttpClient();
        this.SerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonDefaultNamingPolicy.DefaultNamingPolicy,
            IncludeFields = false,
        };
    }

    public async ValueTask<EntityId?> InsertGuarantorAsync(CreateGuarantor Model)
    {
        var Uri = new Uri(string.Format(GlobalSetting.Instance.GuarantorEndpoint, string.Empty));

        try
        {
            string Json = JsonSerializer.Serialize(Model, SerializerOptions);
            StringContent Request = new StringContent(Json, Encoding.UTF8, "application/json");

            HttpResponseMessage HttpResponse = await RestClient.PostAsync(Uri, Request);

            if (HttpResponse.IsSuccessStatusCode)
            {
                string Content = await HttpResponse.Content.ReadAsStringAsync();
                var Response = JsonSerializer.Deserialize<Response<EntityId>>(Content, SerializerOptions)!;

                if (Response.Status.IsSuccess)
                {
                    return Response.Data!;
                }
            }
        }
        catch (Exception Ex)
        {
            Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
        }

        return null;
    }

    public async IAsyncEnumerable<Guarantor> SearchGuarantorsAsync(string? Query = null, int? Offset = null, int? Limit = null)
    {
        await ValueTask.CompletedTask;
        yield break;
    }

    public async ValueTask<IResponse<IObservableCollection<Relationship>>> FetchRelationshipsAsync()
    {
        var Uri = new Uri(string.Format(GlobalSetting.Instance.RelationshipsEndpoint, string.Empty));

        try
        {
            HttpResponseMessage HttpResponse = await RestClient.GetAsync(Uri);

            if (HttpResponse.IsSuccessStatusCode)
            {
                string Content = await HttpResponse.Content.ReadAsStringAsync();
                var Response = JsonSerializer.Deserialize<Response<ObservableCollectionExtended<Relationship>>>(Content, SerializerOptions)!;

                if (Response.Status.IsSuccess)
                {
                    return Response;
                }
            }
        }
        catch (Exception Ex)
        {
            Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
        }

        return new Response<ObservableCollectionExtended<Relationship>>();
    }
}
