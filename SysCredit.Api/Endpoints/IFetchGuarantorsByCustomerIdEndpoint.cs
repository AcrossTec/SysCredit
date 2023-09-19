using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

[assembly: Endpoint("/Api/Customer/{CustomerId}/Guarantors")]

namespace SysCredit.Api.Endpoints;

public interface Inject { }
public interface Inject<S1> { }
public interface Inject<S1, S2> { }
public interface Inject<S1, S2, S3> { }
public interface Inject<S1, S2, S3, S4> { }
public interface Inject<S1, S2, S3, S4, S5, S6> { }
public interface Inject<S1, S2, S3, S4, S5, S6, S7> { }


public interface Store { }
public interface Store<S1> { }
public interface Store<S1, S2> { }
public interface Store<S1, S2, S3> { }


public interface Option { }
public interface Option<S1> { }
public interface Option<S1, S2> { }
public interface Option<S1, S2, S3> { }



public interface Ok<in T> { }
public interface Create<in T> { }
public interface NoContent<in T> { }


public interface Request { }
public interface Request<R> { }


public interface Response<R> { }

#pragma warning disable CS9113 // Parameter is unread.

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed class EndpointAttribute<S, R, O>
    (string HttpMethod, string RouteTemplate, string DatabaseMethod)
    : Attribute
    where S : Inject
    where R : Request
    where O : Response<object>;

#pragma warning restore CS9113 // Parameter is unread.

/*
    [assembly:
    Endpoint
    <
        Inject< Store< Customer, Guarantor, Reference >, Option< SysCreditOption > >,
        Request<  ClassRequest  >,
        Response< Ok<> or Create<> or NoContent<> >
    >
    ( HttpMethods.Get, "/Api/Customer/{CustomerId}/Guarantors", DatabaseMethod: nameof(IStore<Customer>.Method ))]

    public partial class I<nameof(IStore<Customer>.Method)>Endpoint : IEndPoint
    {
        public const string Endpoint = <Route>;

        private readonly IStore<> <Name>Store;
        private readonly IStore<> <Name>Store;
        private readonly IStore<> <Name>Store; 
        ...

        

        public void Construct(
            ClassRequest 
            [FromService] IStore Store,
            [FromService] IOption<> Option1, [FromService] IOption<> Option2, ...)
        {
            <Name>Store = Store.GetStore<Name>();
            <Name>Store = Store.GetStore<Name>();
            <Name>Store = Store.GetStore<Name>();

        }
    }
*/

interface IEndpointWorkFlow
{
    ValueTask InjectContextDataAsync(IDictionary<string, object> ContextData);
}
