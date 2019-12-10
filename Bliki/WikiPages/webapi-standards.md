<!-- TITLE: Web API Standards -->
<!-- SUBTITLE: Conventions to be followed in all Web API service projects -->

# Web Services
- All web service projects should be implemented as self-hosted windows services (using Microsoft's Web API middleware for OWIN instead of ASP.NET)
- There should only be one web-service project per product.  The service should be named `Idn[Product]Service` (e.g. `IdnRmsService`).  Any web-service methods which are common to multiple products should be put in `IdnCoreService`.
- URIs for the Web API should be defined by a `RouteAttribute` on each and every controller method, using a surface-level constant for the URI string.  There should be no `RoutePrefixAttribute` on the controller class.  The constants should each be fully defined as a single string literal without concatenations (e.g. `Login = "api/security/login/"` instead of `Login = uriPrefix + "login/"`).
- All URIs and the controller methods attached to them should read like actions (i.e. verbs), where applicable, and should be as similar to each other as possible (e.g. `api/master-search/names/search/` for `NamesController.Search`)
- All parameters should be passed in the body of the HTTP message, as properties of a DTO encoded as `application/json` content, even when the method only takes one parameter.  The parameters should never be passed in the URL, nor should they be serialized directly, without a DTO container.
- All controller methods should be marked with either `HttpPostAttribute` (if clients do not need to cache the response), or `HttpGetAttribute` (if clients will need to cache the response).  Other HTTP methods like `DELETE` and `PUT` should not be used.
- The tools in `IDN.Communication.WebApi` should be used, where applicable, rather than the Microsoft Web API framework's automatic serialization and processing
- The controller classes should only contain communication-related logic (e.g. serialization, authentication, HTTP, encoding).  All other logic should be encapsulated in business or data access layer libraries.
- The names of the controller methods do not need to include the `Async` suffix, even if they are asynchronous, since nothing should ever call them directly by name
- The URIs for the resources should be in all-lowercase, they should use dashes to separate words, they should start with `api/`, and they should end in a slash (e.g. `api/master-search/names/single-search/`)
- The root URL for the service, as stored in the `Central.config`, should return a working web page when accessed via a web browser (it should respond to `GET` requests with an HTML file).  The web page should display basic information about the service, such as its name, version, and up-time.
- The service should not encrypt any of its communication directly.  If encryption is needed, the host machine should be configured to support or require HTTPS.  The agency installing the service on their own server is expected to provide and maintain the required certificate.  See [this document](configuring-self-hosted-web-api-services-to-use-https) for more details on how to setup HTTPS on Windows.

# Clients
- All .NET clients should make calls to the Web API services via shared `Communication` classes
- The `Communication` classes should be in `Communication` namespaces (e.g. `IDN.Security.Communication.LoginCommunication`)
- The names of the methods in the `Communication` class should closely match the name of the controller method in the web service (e.g. `LoginCommunication.LoginAsync` calls `LoginController.Login`)
- The public methods in the `Communication` class should almost always be asynchronous and include the `Async` suffix in their name
- The `Communication` classes should use the same surface-level constants for the URIs as the web service's controllers do in their `RouteAttribute`
-  The tools in `IDN.Communication.WebApi` should be used, where applicable, to construct, send, receive, and read the HTTP messages

# DTOs
- All DTO classes should be shared between the service project and the client-side communication projects via common `Surface` projects.  All DTOs should be declared in a `Surface.Dtos` namespace, without being under a layer-specific parent namespace (e.g. should be `IDN.Reports.Surface.Dtos` rather than `IDN.Reports.Communication.Surface.Dtos`).  If the same class is used both as a DTO and as a data object in the data-access layer, it should stay in the `Dtos` namespace (i.e. the data-access layer should re-use the DTO rather than having the communication layer re-use the data class).
- When a DTO is going to be repeated many times in an array, where the data-length of the serialized representation of the class would be problematic for transfer speeds, the property names should be shortened via serialization attributes.  However, for most DTOs, where the difference in transfer speed is negligable, the property names should not be shortened.
- If serialization attributes are not required, they should be omitted.  If, however, attributes are required, the following should be used (in order of preference):
	- Newtonsoft JSON.NET attributes only (DTO is only used for Web API)
	- WCF attributes only (DTO is used for both Web API and WCF-style SOAP)
	- Both Newtonsoft JSON.NET and WCF attributes (DTO is used for both Web API and WCF-style SOAP, with differing serialization requirements for each)
	- `XmlSerializer` attibutes only (DTO is used for both Web API and either Message Switch or ASMX-style SOAP)
	- Both Newtonsoft JSON.NET and`XmlSerializer` attibutes (DTO is used for both Web API and either Message Switch or ASMX-style SOAP, with differing serialization requirements for each)
- Simple concrete arrays should be used for all list properties, where possible.  Where arrays are not practical, `List<T>` should be used.
- DTO classes should not have any generic type parameters
- If a list of DTOs needs to be transferred (e.g. as an argument or as a return value), there should always be an enclosing DTO which contains that list as a property, and that's what the Web API methods should use.
- Nothing about the .NET implementation of the web service should be exposed to consumers.  For instance, .NET type-names should not be expected in property values nor be required in serialization-added metadata because the service would need it to dynamically deserialize for the right type via reflection.