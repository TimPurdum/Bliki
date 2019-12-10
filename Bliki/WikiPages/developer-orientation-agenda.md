<!-- TITLE: Developer Orientation Agenda -->
<!-- SUBTITLE: Steve's notes for what to cover during orientation for new developer hires -->

# General
*	Tour/Meet-and-greet
*	HR paperwork
*	Workstation setup
*	Order hardware
	*	Dock
	*	Monitors (usually two 28" 4K)
	*	Keyboard
	*	Mouse
	*	Mouse pad
	*	Webcam 
	*	Headset (usually Logitech wireless)
	*	Video cables
	*	Chair mat
	*	UPS
*	Product demos
*	Remote employee introductions
*	Industry specific terms
	*	UCR, IBR, Clery, CJIS, NCIC, etc.
*	Setup development machine
*	Fingerprints at Ashtabula CSO for Ohio background check
*	Fingerprints at IDN for other states
*	CJIS training

# Code-base 
*	Source control
	*	Git server
	*	Git client tools
	*	Git repositories
	*	Git workflow/branching/release cycle
	*	VSS
*	Folder structure of master repository
*	Master solution
	*	Organization
	*	Project references
	*	File references
	*	NuGet references
*	Projects
	*	VB vs. C# 
	*	Unit tests
* Project settings
	*	Option Explicit/Strict/Infer
	*	Target framework
	*	Target CPU
	*	Configuration (Debug/Release)
*	Solution Integrity Checker

# Incident Module
*	DtoSet (Model)
*	PageDoc (View)
*	PageMeta (ViewModel)
*	Paginator & FormGenerator (Business)
*	CodeBehind (Controller)
*	Communication
*	Business
*	DataAccess
*	Support tables
*	IBR validation and export

# Tools
*	Trillian
*	Slack
*	Bomgar
*	Axosoft
*	Jenkins

# Configuration
*	Idn.ini
*	MasterData
*	Central.config
*	Security
*	Personnel

# Deployment
*	Auto Updater
*	Auto-Rev
*	Web Updater
*	File Transfer Service
*	DB Upgrader
*	Environment initializer
*	Services
*	Order of deployment and backwards compatibility (DB, then services, then clients)

# Other architecture
*	CAD/CAD Server
*	Mobile/Message Switch
*	Legacy MasterData and SOAP
*	Master Search and triggers
*	Query Builder and Normalization Service

# PageDoc tutorial project
*	Create a project which displays a read-only PageDoc of some simple official form
	*	Redactable elements
	*	Quick-redact groups
	*	Load data from file
	*	Custom element
*	Create an `IPageDocSupplier` class which supplies the form to the viewer
*	Change the supplier class to be an `IEditCodeBehind`
	*	Add edit points to the form where appropriate, using only the common edit point classes
	*	Save to the same file from which it loads the data
	*	Add some validation rules
	*	Add a drag data source and a drop point
	*	Create a custom edit point

# First project
* Discuss and prepare for first project, if known.