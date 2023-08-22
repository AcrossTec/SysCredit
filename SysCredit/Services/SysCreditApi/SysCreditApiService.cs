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

    public async ValueTask<IResponse<EntityId?>> InsertCustomerAsync(CreateCustomer Model)
    {
        var Uri = new Uri(string.Format(GlobalSetting.Instance.CustomerEndpoint, string.Empty));

        try
        {
            string Json = JsonSerializer.Serialize(Model, SerializerOptions);
            StringContent Request = new StringContent(Json, Encoding.UTF8, "application/json");

            HttpResponseMessage HttpResponse = await RestClient.PostAsync(Uri, Request);

            if (HttpResponse.IsSuccessStatusCode)
            {
                string Content = await HttpResponse.Content.ReadAsStringAsync();
                var Response = JsonSerializer.Deserialize<Response<EntityId>>(Content, SerializerOptions)!;
                return Response;
            }

            return new Response<EntityId?>
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = HttpResponse.ReasonPhrase
                }
            };
        }
        catch (Exception Ex)
        {
            Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
            return new Response<EntityId?>
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = Ex.Message
                }
            };
        }
    }

    public async ValueTask<IResponse<EntityId?>> InsertGuarantorAsync(CreateGuarantor Model)
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
                return Response;
            }

            return new Response<EntityId?>
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = HttpResponse.ReasonPhrase
                }
            };
        }
        catch (Exception Ex)
        {
            Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
            return new Response<EntityId?>
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = Ex.Message
                }
            };
        }
    }

    public async ValueTask<IResponse<IObservableCollection<Customer>>> SearchCustomersAsync(string? Query = null, int? Offset = null, int? Limit = null)
    {
        var Uri = new Uri(string.Format("{0}?Value={1}&Offset={2}&Limit={3}", GlobalSetting.Instance.CustomerSearchEndpoint, Query, Offset, Limit));

        try
        {
            HttpResponseMessage HttpResponse = await RestClient.GetAsync(Uri);

            if (HttpResponse.IsSuccessStatusCode)
            {
                string Content = await HttpResponse.Content.ReadAsStringAsync();
                var Response = JsonSerializer.Deserialize<Response<ObservableCollectionExtended<Customer>>>(Content, SerializerOptions)!;
                return Response;
            }

            return new Response<ObservableCollectionExtended<Customer>>
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = HttpResponse.ReasonPhrase
                }
            };
        }
        catch (Exception Ex)
        {
            Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
            return new Response<ObservableCollectionExtended<Customer>>
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = Ex.Message
                }
            };
        }
    }

    public async ValueTask<IResponse<IObservableCollection<Guarantor>>> SearchGuarantorsAsync(string? Query = null, int? Offset = null, int? Limit = null)
    {
        var Uri = new Uri(string.Format("{0}?Value={1}&Offset={2}&Limit={3}", GlobalSetting.Instance.GuarantorSearchEndpoint, Query, Offset, Limit));

        try
        {
            HttpResponseMessage HttpResponse = await RestClient.GetAsync(Uri);

            if (HttpResponse.IsSuccessStatusCode)
            {
                string Content = await HttpResponse.Content.ReadAsStringAsync();
                var Response = JsonSerializer.Deserialize<Response<ObservableCollectionExtended<Guarantor>>>(Content, SerializerOptions)!;
                return Response;
            }

            return new Response<ObservableCollectionExtended<Guarantor>>
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = HttpResponse.ReasonPhrase
                }
            };
        }
        catch (Exception Ex)
        {
            Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
            return new Response<ObservableCollectionExtended<Guarantor>>
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = Ex.Message
                }
            };
        }
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
                return Response;
            }

            return new Response<ObservableCollectionExtended<Relationship>>
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = HttpResponse.ReasonPhrase
                }
            };
        }
        catch (Exception Ex)
        {
            Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
            return new Response<ObservableCollectionExtended<Relationship>>
            {
                Status =
                {
                    HasError = true,
                    ErrorMessage = Ex.Message
                }
            };
        }
    }
}
