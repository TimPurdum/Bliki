<!-- TITLE: NDC Conference -->
<!-- SUBTITLE: Ideas Gleaned from the 2019 Conference -->

# From Design Patterns to Universal Abstractions

## Composite Pattern
- Allows composition of similar methods and classes
- Example Application : `ISupportRepository`
  - Could create a `CompositeSupportRepository` that takes a param list of other `ISupportRepository` implementations and loops through them on `GetRecords<T>()`
## Command Query Separation
- Commands – classes/methods that modify state
- Queries – classes/methods that return data
- Keeping separate allows for more knowledge of state at any given moment.

# Security
- Don't allow public apis without authentication that accept an integer as key, easy to guess/iterate through for a hacker.
# Kanban
- List, prioritize, and share all tasks/tickets
- Prioritize by the cost of not finishing
- Allow devs to pull tasks, rather than assigning to an already full dev
- Track everything, to understand how time is really used

# Clean Architecture with Asp.Net Core
[bit.ly/northwind-traders]
Design
- **Core**
	- **Domain Layer**
	- **Application Layer** -> dependency on Domain Layer
- **Persistence Layer** -> dependency on Application Layer
	- Repository (or not, if using EF Core, not necessarily useful)
	- Entity Configuration classes can pass Fluent API descriptions to `DbContext`
- **Infrastructure Layer** -> dependency on Application Layer
	- Implements the interfaces from the Core layers
	- No layers depend *on* Infrastructure, only on interfaces
	- `MachineDateTime` implementation for testing/changing date
- **Presentation Layer** -> dependency on Application Layer
	- `ViewModel`s should contain all necessary data for display, avoid roundtrip calls
	- Controllers should be logic-free, call into business layer

# Cosmos DB
- Service available on Azure
- Stores JSON documents
- Multiple APIs
	- Core (SQL)
		- Collection = SQL Table
		- SQL queries return JSON
		- Compatible with EF Core
	- MongoDB (Document)
	- Cassandra
	- Azure Table
	- Gremlin
- Lots of scaling advantages

# REST
- Works with Web API, but more strict that what we're doing currently
- API should just follow the HTTP methods
	- GET (all)
	- GET (one)
	- POST (insert)
	- PUT (update)
	- DELETE

# Swagger
- Allows easy web-based interaction with API endpoints
- Possible replacement/extension for the core service web interface

# Web API Security in Asp.Net Core 2.2 +
- Kestrel - certified for direct web as of 2.2, but still recommended to use reverse proxy through IIS, Apache, Nginx...
- Data Protection
	- Store kesy outside app directory
	- Keys are auto-generated
	- Use `services.AddDataProtection()` in Startup file
		- `.SetApplicationName("name")`
		- `.ProtectKeysWithCertificate(x509cert)`
		- `.PersistKeysToFileSystem(path)`
	- Used for:
		- cookies
		- anti-forgery
		- OpenID/OAuth
		- TempData
		- can be called directly via `IDataProtectionProvider`
- Authentication
	- local, social, OpenID, JWT
	- Session management handled via cookies
	- `IAuthenticationService`
- Authorization
	- Moves away from role-based security to policy-based authorization.
	- Uses a permission/policy name set like `options.AddPolicy("name")`
	- `AuthorizationRequirementHandler` handles the policy
	- `AspNetCore.Identity` is a separate thing, used with EF for managing DB records
	- `IdentityServer` is built into Asp.Net Core 3.0, allows separation of login logic from specific apps

# Continuous Deployment
- Use Azure DevOps
- Configure via YAML file
- Cool

# Event Driven Architecture
- Managing complexity is a tradeoff between:
	- Local Complexity: human readable, understandable projects
	- Global Complexity: the design of boundaries and dependencies
- 3 Types of Events
	- Event Notification: no response, minimal info
	- Event-Carried State Transfer: contains new state information
	- Event Sourcing/Domain Event: contains list of events that lead to current state
		- stateless, only contains list of changes
- Anti-Corruption Layer - decouples logic changes from dependency
	- useful to manage changes that only happen to one side of the communication

# High Performance C#
- Two book suggestions:
	- Pro .NET Benchmarking
	- Pro .NET Memory Management
- Pareto Principal (80/20 rule)
	- 80% of resources are consumed by 20% of the code
	- Squared: 64% of resources consumed by 4% of code
	- Cubed: 51% of resources consumed by 0.8% of code
- Micro-optimization is a *last resort*, it makes code less readable, more brittle
- (Actual implementation examples were completely over my head)

# Polly - Transient Error Handling
- Built into Asp.Net Core 2.1 `HttpClientFactory`, available as .Net Standard 2.0 package
- Strategies/Policies (used in client logic to handle failures)
	- Retry
	- Circuit Breaker - stops flooding retries
	- Timeout
	- Bulkhead Isolation
	- Cache
	- Fallback
- Step 1: Define Policy (`var retryPolicy = Policy.Handle<Exception>...`
- Step 2: Execute (`await retryPolicy.ExecuteAsync()...`
- Can combine multiple policies with `Policy.Wrap(pol1, pol2, pol3)`
- Demos available on GitHub

# Azure Functions 2.0
- Event-driven (serverless) functions
- Can be run on Azure, locally, or containerized and deployed to on-site
- Premium tier 
	- prevents cold-start delays
	- can be run over VPN
- AppInsights
- Durable Functions - maintain state, can be used as orchestrators to call multiple functions