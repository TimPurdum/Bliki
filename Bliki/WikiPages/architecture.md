<!-- TITLE: Architecture -->
<!-- SUBTITLE: A high-level description of the various architectures used in the master solution  -->

(This document is still very much a work in progress)
# Straw-Ban
For all new modules and products being written today, Straw-Ban is the architecture which should be used.  This is the vision for the future to which we are striving.  All new projects should follow this pattern as closely as possible, and, when practical to do so, older code should be refactored to be more inline with this new architecture.  Building on the lessons learned from the Fidget Spinner architecture, Straw-Ban was formulated over several years, coallescing into a clear architecture during 2019.  Nothing fully uses Straw-Ban yet, in a clean/from-scratch way, but the closest examples would be the Master Names, Reviewals, and Support Data modules.

## Major Projects
While each of the following libraries may be split into multiple sub-libraries, depending on the breadth of the product, this demonstrates the basic breakdown of the layers of the application into its respective projects.

### Client-Side Only

#### `Idn[Product].exe`
This executable project should have very little code in it.  It should have some global error handling, self-updating, configuration-loading, and system-launching logic, but other than that, all of the code for the product should be in class libraries.  Each product should only have one client-side executable.  This executable launches the client-side process which displays the UI.  This project should only reference UI, business, and surface layer libraries.  Nothing in this executable, or any of its dependencies, should directly access any data on the server.

#### `IDN.[Product].Ui.dll`
This library contains the forms, controls, and helper classes for the UI layer of the application (i.e. the presentation layer or the GUI).  This project should only reference communication, business, surface, and other UI layer libraries.  Nothing in this library, or any of its dependencies should directly access any data on the server.

#### `IDN.[Product].Communication.dll`
This library contains the client-side logic to connect to the server, build and send request-messages, receive and read response-messages, cache the responses (when applicable), serialize and deserialize data (as necessary), and throw UI-friendly exceptions for any communication-related errors.  This will likely contain a separate class for each feature of the software, each containing a list of asynchronous methods which provide the API for all the server-side functionality that is needed for that feature by the UI.  This project should only reference business, surface, and other communication layer libraries.  Nothing in this library, or any of its dependencies should directly access any data on the server.

### Server-Side Only

#### `Idn[Product]Service.exe`
This is a standard Windows service (launched from within the Services Control Panel) which hosts the back-end web-service for the product.  It should run as an unattended service with no locally displayed UI.  The service provides all of the data-access and as much of the business logic for the product that it can, so that the client-side projects can be as thin as possible.  The web-service's API should be as full featured and as cross-platform-friendly as possible so that multiple clients written using different technologies can share the same functionality with minimal effort.  This project should contain only communication-layer logic, such as request-message reading, response-message building, data serialization, in-memory caching, error handling, and logging.  All the code for the actual logic being invoked via the web-service should be in libraries.  This project should only reference surface, business, and data-access layer libraries.  

#### `IDN.[Product].DataAccess.dll`
This library contains all the server-side data repositories as well as their back-end data-access dependencies.  The data-access layer is usually split into two sub-layers:

1. High-level repository classes which expose the data in an implementation-hiding object-graph style, as they will be represented to clients of the web service
2. Low-level data-access classes which expose the data in their raw, implementation-specific, relational-style, mimicing the underlying DB schema

The repositories are public, so they can be used by the web service.  The data-access classes are internal and are only used by the repositories.  This project should only reference surface, business, and other data-access layer libraries.

Which technology is used for the implementation of the data-access classes (e.g. ADO.NET, Dappper, EF) is generally considered to be unimportant, though, for the sake of consistency, most are implemented with ADO.NET.  Regardless of which technology is used, it should ideally be completely encapsulated in the data-access classes so that the consuming repositories don't need to be rewritten if the data-access class is refactored to use a different technology.  If some of those implementation details do need to leak out, it absolutely must not be exposed by the repositories.  Nothing about the public interface of the repositories should be dependent on the technology used to implement the data-access classes.

For the most part, the data-access classes are equivalent to those used in the Fidget Spinner architecture.  The main Straw-Ban innovation in this layer is the addition of the repositories, and the limited access level (i.e. internal) of the data-access classes.  However, even in the data-access classes, there is one important difference in their style of implementation.  In Fidget Spinner, all data-access methods use ADO.NET to call a stored procedure in the database.  In Straw-Ban, even when ADO.NET is being used (as is most often the case), the SQL commands are constructed, ad hoc, in the code, rather than using a stored procedure.  Stored procedures may still be used in some cases, where doing so provides a necessary, significant boost to performance, but they aren't used otherwise.

### Both Sides

#### `IDN.[Product].Business.dll`
This library contains all the business logic for the product.  Business logic will be any code which is not specific to one of the other layers (UI, communication, data-access, and surface), and may be used by any of them (except surface).  This includes things like domain-rules, data validation, report generation, integrations, data processing, and common helper classes.  Nothing in this library, or any of its dependencies should directly access any data on the server.  This project should only reference surface, and other business layer libraries.  

#### `IDN.[Product].Surface.dll`
This library contains all public, dependency-free, non-implementation types which are intended for consumption by code in other namespaces (i.e. aren't just public for exposure to unit-tests).  In other words, it contains non-logic classes and structures which are exposed publicly by any of the other projects because they are part of the public interface of those projects so that they can be used by its consumers.  For instance, the surface project will contain things like interfaces, DTOs, constants, enumerations, exception classes, attributes, and delegates.  See the [Project and Namespace Guidelines](project-and-namespace-guidelines#project-organization) for a detailed description of how the surface project should be structured.  The purpose of the surface project is to provide a way to reference the public interface of a system without any of its implementation (and all the dependencies that entails).  This not only greatly reduces the number of unintented dependencies, but it also helps to avoid circular project references.  This project should only reference other surface projects.

## Technologies
* All projects should be written in C#
* All surface, business, and communication projects should target .NET Standard
* All UI, web-service, and data-access projects should target .NET Core
* All web-services should use the [Web API protocol](webapi-standards) and should be self-hosted in a windows service
* All UI should be written in WPF or Xamarin Forms
* All dependencies should be purely managed (no COM dependencies or other native binaries)

## Example
Nope.  No example yet.  Try again later.
# Fidget Spinner 
This is the architecture originally used by the RMS Incidents module, which was released, first for Virginia, around 2015.  It closely models the Gangnam Style architecture, but it has an extra layer specifically for communication.  The lack of a communication layer was seen as the most obvious flaw in the Gangnam Style architecture.  In all other other respects, Fidget Spinner was a conservative, incremental improvement.
# Gangnam Style 
Security, Workstations (2012)
# Message Switch
Mobile, CAD, and Master Search (2005)

* MSMQ or sockets
* Clients (e.g. Mobile, CAD, master search)
* Router (Message Switch)
* Services (Message Switch, interfaces)
* DB-on-DB
* Messages can be addressed to workstation, application, or user
* Encryption and logging
* Originally designed as a general purpose messaging framework to be used by all IDN products for all communication
* CAD Interface
* Many message types
* XML serialization
# CAD
CAD (2005)
* MSMQ
* CAD client
* CAD server
* Few message types
* XML serialization
* Encryption
# Legacy
VB6, Access, Upscale to SQL, MasterData, SOAP Mode