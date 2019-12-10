<!-- TITLE: Project And Namespace Guidelines -->


# Project and Namespace Guidelines

## Class Library Projects

### Naming Convention
- The order of the names within each namespace should be as follows:
  - **Company** - Required and always `IDN`
  - **Product** - The name of the product or major application in which the code is used, if applicable 
    (e.g. `IDN.Rms`, `IDN.Cad`) 
  - **Module** - The name of the module or major feature in which the code is used, if applicable 
    (e.g. `IDN.Rms.Incident`).  When the module does not belong to a specific product, it will come 
    immediately after the company in the namespace (e.g. `IDN.Pages`).
    - **Sub-Module** - If a module is sufficiently large, it may have further layers within the module 
      (e.g. `IDN.Rms.Incident.Clery`)
  - **State** - The postal abbreviation for the state, country, or province in which the code is used, 
    if applicable (e.g. `IDN.Rms.Incident.Va`).  When state-specific code does not belong to a 
    particular product or module, the state may come sooner in the namespace (e.g. `IDN.Va`, 
    `IDN.Rms.Va`, `IDN.Pages.Va`).
  - **Layer** - The major layer of the software's architecture in which the code is used, if applicable 
    (e.g. `IDN.Rms.Incident.Va.Ui`).  If the layer-specific code does not belong to a specific product, 
    module, or state, the layer may come sooner in the namespace (e.g. `IDN.Ui`, `IDN.Pages.Ui`, 
    `IDN.Rms.Ui`).
    - **Sub-Layer** - If the layer is sufficiently large, it may warrant a further breakdown within the 
      layer (e.g. `IDN.Rms.Incident.Va.Business.Ibr`)
  - **Technology** - The name of the technology, framework, or cross-technology marshalling used or 
    implemented by the code in the namespace, if applicable (e.g. `IDN.Rms.Incident.Va.Ui.WinForm`).  If 
    the code is usable by projects implementing any technology (e.g. targetting .NET Standard), then 
    this portion of the namespace should be excluded.  When technology specific code does not belong to 
    a particular product, module, state, or layer, the technology may come sooner in the namespace 
    (e.g. `IDN.MasterSearch.ComVisible`).  The commonly used names are:
    - `ComVisible` - COM-visible code
    - `Legacy` - Managed wrappers around COM libraries and other legacy modules
    - `WebApi` - Code related to hosting or consuming Web API services
    - `Wcf` - Code related to hosting or consuming WCF services
    - `WinForm` - UI code utilizing the `System.Windows.Forms` framework
    - `Wpf` - UI code utilizing the WPF framework
    - `Xamarin` - UI code utilizing the Xamarin Forms framework
    - `Uwp` - Xamarin UWP projects
    - `iOS` - Xamarin iOS projects
    - `Android` - Xamarin Android projects
    - `NetFramework` - Windows-desktop-specific code which targets the classic .NET Framework
    - `Test` - Unit tests utilizing the MSTest framework
- After the company (`IDN.`) all words in a namespace, including acronyms, should be pascal-case 
  (e.g. `IDN.Rms.Incident.Va`)
- Whereas plural names are discouraged for types (e.g. `class`, `struct`), they are encouraged in 
  namespaces, if it helps to make the contents of the namespace more clear and concise 
  (e.g. `IDN.Exceptions`, `IDN.ImageNet.Reports`)
- Higher-level namespaces are used for code that is common to all of the sub-namespaces that come 
  below it.  As such, there should be no need for namespaces called things like `Common` or `Shared`.  
  For Instance:
  - `IDN` contains code that is used by all other namespaces
  - `IDN.Rms` contains RMS-related code that is used by all RMS modules
  - `IDN.Rms.Incidents` contains incident-related code that is used by all states
  - `IDN.Rms.Incident.Va` contains VA-specific code that is used by all layers in the incident module

### Project organization
- If at all possible, the name of the class library should match the root namespace of the code it 
  contains (e.g. `IDN.Rms.Business.dll` contains code in the `IDN.Rms.Business` and 
  `IDN.Rms.Business.Pagination` namespaces)
- If at all possible, even within a single project, code in a higher-level namespace should not 
  directly reference code in a lower-level namespace.  Code references should always be upstream in 
  the namespace heirarchy (e.g. classes in `IDN.Ui.WinForms` can reference and make use of classes in 
  `IDN.Ui`, but code in `IDN.Ui` should not use code from `IDN.Ui.WinForms`).
- A single project may contain code for multiple namespaces, but spanning code for a single namespace 
  across multiple projects is strongly discouraged.
- All public dependency-free non-implementation types which are intended for consumption by code in 
  other namespaces (i.e. aren't just public for exposure to unit-tests) should be placed in a 
  `Surface` namespace (e.g. `IDN.Surface`, `IDN.Rms.Incident.Surface`).  
  - The `Surface` namespaces are an exception to the upstream-only rule.  Each `Surface` namespace 
    contains the non-implementation types for the implementation types in the namespace above it 
    (e.g. implementation code in `IDN.Rms.Incident` will use and reference non-implementation code in 
    `IDN.Rms.Incident.Surface`.  However, with the `Surface` namespace, it is strictly downstream only 
    (e.g. `IDN.Rms.Incident.Surface` can have no reference to `IDN.Rms.Incident`).
  - Each `Surface` namespace should contain the following child namespaces, if applicable:
    - `Attributes` - Derivations of `System.Attribute`
    - `Data` - Data types, containing very little logic, used to represent entities, data models, data structures, and complex 
      parameters/return-values. This should not include types derived from `EventArgs` or `Exception`.  This should 
			also not contain any types which need to be serializable, since those should be in `Surface.Dtos`.
    - `Definitions` - Constants, enumerations, literals, and other read-only values
    - `Delegates` - Derivations of `System.Delegate`
    - `Dtos` - Data transfer objects (i.e. classes, containing very little logic, which are used for 
      serializing data, such as for communication and caching).  All types in this namespace must be serializable 
			and all serializable surface types should be in this namespace.  If a data type does not need to be serializable, 
			it probably belongs in `Surface.Data` instead.
    - `EventArgs` - Derivations of `System.EventArgs`
    - `Exceptions` - Derivations of `System.Exception`
    - `Interfaces` - Interfaces for types which are implemented in the parent namespace
  - Projects containing `Surface` namespaces should never contain any non-`Surface` namespaces
  - Projects containing `Surface` namespaces should only reference other `Surface` projects
  - So as to reduce the total number of surface library dependencies, reasonable numbers of `Surface` 
    namespaces should be grouped into single projects.  At the very least, all child-namespaces of a 
    `Surface` (e.g. `Definitions`, `Interfaces`) should be in the same project with their parent 
    `Surface` namespace (e.g. `IDN.Surface.dll` contains `IDN.Surface.Definitions` and 
    `IDN.Surface.Interfaces`).  However, it should not be uncommon that several parent `Surface` 
    namespaces will be grouped into the same project (e.g. `IDN.Pages.Surface.dll` might contain 
    `IDN.Pages.Surface`, `IDN.Pages.Business.Surface`, and `IDN.Pages.Ui.Surface`, along with 
    their respective child-namespaces).  When this is done, the default/root namespace for the project 
    should be changed (e.g. the default namespace for the `IDN.Pages.Surface` project would be 
    `IDN.Pages`, since it would contain namespaces that are not directly under `IDN.Pages.Surface`, 
    such as `IDN.Pages.Business.Surface`).
- When a single project contains multiple namespaces, the following conventions should be followed:
  - C# Projects
    - Each code-containing folder in the project should represent a namespace by the same name (e.g. the 
      `Definitions` folder in root folder of the `IDN.Surface` project contains the code for the 
      `IDN.Surface.Definitions` namespace).  Nested folders represent similarly nested namespaces.
    - Code files declaring types in the root-namespace for the project should be placed in the root folder 
      for the project
  - VB Projects
    - Each namespace should be contained within a separate folder in the project's root folder.  The name 
      of the folder should be the same as the fully qualified namespace (e.g. the 
      `IDN.Surface.Definitions` folder in the root of the `IDN.Surface` project contains the code for the 
      `IDN.Surface.Definitions` namespace).  Folders for child namespaces should also be in the root 
      folder of the project (i.e. the namespace folders should not be nested).
    - Each code file in any of the namespace folders must declare the namespace at the top of the file.
      - In the WinForm `Designer` files for `Form` and `Control` classes, the namespace must be declared 
        using the relative namespace from the project's root namespace.  (e.g. the 
        `IDN.Pages.Ui\PreviewControl.Designer.vb` file in the `IDN.Pages` project declares 
        `Namespace Ui`)
      - In all other code files, the namespace should be declared using the fully qualified name (e.g. the 
        `IDN.Pages.Ui\PreviewControl.vb` file in the `IDN.Pages` project declares 
        `Namespace Global.IDN.Pages.Ui`)
    - All other folders in the project are for organizational purposes only.  Unless the name of the 
      folder is that of a fully-qualified namespace, it is to be assumed that the code inside that folder 
      is in the same namespace as the code in it's parent folder.
- All COM-visible code should be housed in a separate class library rather than being intermingled 
  with non-COM-related code.  Such projects should use `ComVisible` as the name for the technology 
  portion of their namespace (e.g. `IDN.MasterSearch.ComVisible.dll`).
- All code which references legacy COM binaries and provides a managed .NET wrapper around them should 
  be housed in a separate class library.  Such projects should use Legacy as the name for the 
  technology portion of their namespace (e.g. `IDN.Rms.Legacy.dll`).

## Executable Projects
All executable projects (e.g. WinForm, Windows Services, Console) should use a root namespace which 
matches the name of the project.  The name should start with `Idn` and contain no spaces.  All the 
words in the name, including acronyms should be pascal-case (e.g. `IdnRmsVaService`, `IdnDbUpgrader`).

## Folder/File organization
Each project should be stored in a separate folder.  The name of the folder should match the 
project name.  The project folder should not be nested inside another "solution folder".

Project folders should be stored directly under one of the following top-level folders, depending 
on its project type:
- `ConsoleApps` - Console applications
- `Services` - Windows services (registered with the Services control panel)
- `Shared` - All class libraries, regardless of their dependencies/targetted-framework
- `Tests` - All unit test projects (utilizing the MSTest framework)
- `WpfApps` - Windows desktop applications utilizing the WPF framework
- `Web` - All IIS-hosted assemblies (e.g. websites and web services)
- `WinFormApps` - Windows desktop applications utilizing the WinForm framework
- `XamarinApps` - Applications targetting the Xamarin framework (usually mobile apps)