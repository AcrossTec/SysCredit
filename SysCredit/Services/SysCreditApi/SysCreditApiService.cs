namespace SysCredit.Mobile.Services.Https;

using DynamicData.Binding;

using Microsoft.Extensions.Logging;

using SysCredit.Helpers;

using SysCredit.Mobile.Models;
using SysCredit.Mobile.Models.Customers.Creates;
using SysCredit.Mobile.Settings;
using SysCredit.Mobile.Mockup;

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
                var Response = JsonSerializer.Deserialize<Response<EntityId?>>(Content, SerializerOptions)!;
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
        try
        {
            // Validar el garante
            var validationMessage = GuarantorRepository.ValidateGuarantor(Model);
            if (validationMessage != null)
            {
                return await Task.FromResult(new Response<EntityId?>
                {
                    Status = { HasError = true, ErrorMessage = validationMessage }
                });
            }

            // Inserta el garante en la lista
            GuarantorRepository.AddGuarantor(Model);

            return await Task.FromResult(new Response<EntityId?>
            {
                Status = { HasError = false },
                Data = new EntityId { Id = GuarantorRepository.GuarantorsList.Count }
            });
        }
        catch (Exception Ex)
        {
            Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
            return await Task.FromResult(new Response<EntityId?>
            {
                Status = { HasError = true, ErrorMessage = Ex.Message }
            });
        }

        //var Uri = new Uri(string.Format(GlobalSetting.Instance.GuarantorEndpoint, string.Empty));

        //try
        //{
        //    string Json = JsonSerializer.Serialize(Model, SerializerOptions);
        //    StringContent Request = new StringContent(Json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage HttpResponse = await RestClient.PostAsync(Uri, Request);

        //    if (HttpResponse.IsSuccessStatusCode)
        //    {
        //        string Content = await HttpResponse.Content.ReadAsStringAsync();
        //        var Response = JsonSerializer.Deserialize<Response<EntityId?>>(Content, SerializerOptions)!;
        //        return Response;
        //    }

        //    return new Response<EntityId?>
        //    {
        //        Status =
        //        {
        //            HasError = true,
        //            ErrorMessage = HttpResponse.ReasonPhrase
        //        }
        //    };
        //}
        //catch (Exception Ex)
        //{
        //    Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
        //    return new Response<EntityId?>
        //    {
        //        Status =
        //        {
        //            HasError = true,
        //            ErrorMessage = Ex.Message
        //        }
        //    };
        //}
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
        try
        {
            var guarantors = GuarantorRepository.SearchGuarantors(Query, Offset, Limit);

            return await Task.FromResult(new Response<ObservableCollectionExtended<Guarantor>>
            {
                Status = { HasError = false },
                Data = new ObservableCollectionExtended<Guarantor>(guarantors)
            });
        }
        catch (Exception Ex)
        {
            Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
            return await Task.FromResult(new Response<ObservableCollectionExtended<Guarantor>>
            {
                Status = { HasError = true, ErrorMessage = Ex.Message }
            });
        }

        //var Uri = new Uri(string.Format("{0}?Value={1}&Offset={2}&Limit={3}", GlobalSetting.Instance.GuarantorSearchEndpoint, Query, Offset, Limit));

        //try
        //{
        //    HttpResponseMessage HttpResponse = await RestClient.GetAsync(Uri);

        //    if (HttpResponse.IsSuccessStatusCode)
        //    {
        //        string Content = await HttpResponse.Content.ReadAsStringAsync();
        //        var Response = JsonSerializer.Deserialize<Response<ObservableCollectionExtended<Guarantor>>>(Content, SerializerOptions)!;
        //        return Response;
        //    }

        //    return new Response<ObservableCollectionExtended<Guarantor>>
        //    {
        //        Status =
        //        {
        //            HasError = true,
        //            ErrorMessage = HttpResponse.ReasonPhrase
        //        }
        //    };
        //}
        //catch (Exception Ex)
        //{
        //    Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
        //    return new Response<ObservableCollectionExtended<Guarantor>>
        //    {
        //        Status =
        //        {
        //            HasError = true,
        //            ErrorMessage = Ex.Message
        //        }
        //    };
        //}
    }

    public async ValueTask<IResponse<IObservableCollection<Relationship>>> FetchRelationshipsAsync()
    {
        return await ValueTask.FromResult<IResponse<IObservableCollection<Relationship>>>(
            new Response<IObservableCollection<Relationship>>
            {
                Data = new ObservableCollectionExtended<Relationship>
                {
                    new Relationship { RelationshipId = 1, Name = "Padre" },
                    new Relationship { RelationshipId = 2, Name = "Madre" },
                    new Relationship { RelationshipId = 3, Name = "Hermano" },
                    new Relationship { RelationshipId = 4, Name = "Hermana" },
                    new Relationship { RelationshipId = 5, Name = "Tio" },
                    new Relationship { RelationshipId = 6, Name = "Tia" },
                    new Relationship { RelationshipId = 7, Name = "Primo" },
                    new Relationship { RelationshipId = 8, Name = "Prima" },
                    new Relationship { RelationshipId = 9, Name = "Abuelo" },
                    new Relationship { RelationshipId = 10, Name = "Abuela" },
                    new Relationship { RelationshipId = 11, Name = "Sobrino" },
                    new Relationship { RelationshipId = 12, Name = "Sobrina" },
                    new Relationship { RelationshipId = 13, Name = "Amigo" },
                    new Relationship { RelationshipId = 14, Name = "Amiga" },
                    new Relationship { RelationshipId = 15, Name = "Conocido" },
                    new Relationship { RelationshipId = 16, Name = "Conocida" },
                    new Relationship { RelationshipId = 17, Name = "Vecino" },
                    new Relationship { RelationshipId = 18, Name = "Vecina" },
                    new Relationship { RelationshipId = 19, Name = "Otro" },
                    new Relationship { RelationshipId = 20, Name = "Otra" }
                }
            }
        );

        //var Uri = new Uri(string.Format(GlobalSetting.Instance.RelationshipsEndpoint, string.Empty));

        //try
        //{
        //    HttpResponseMessage HttpResponse = await RestClient.GetAsync(Uri);

        //    if (HttpResponse.IsSuccessStatusCode)
        //    {
        //        string Content = await HttpResponse.Content.ReadAsStringAsync();
        //        var Response = JsonSerializer.Deserialize<Response<ObservableCollectionExtended<Relationship>>>(Content, SerializerOptions)!;
        //        return Response;
        //    }

        //    return new Response<ObservableCollectionExtended<Relationship>>
        //    {
        //        Status =
        //        {
        //            HasError = true,
        //            ErrorMessage = HttpResponse.ReasonPhrase
        //        }
        //    };
        //}
        //catch (Exception Ex)
        //{
        //    Logger.LogError(new EventId(Ex.HResult, Ex.GetType().Name), Ex, Ex.Message);
        //    return new Response<ObservableCollectionExtended<Relationship>>
        //    {
        //        Status =
        //        {
        //            HasError = true,
        //            ErrorMessage = Ex.Message
        //        }
        //    };
        //}
    }
}
