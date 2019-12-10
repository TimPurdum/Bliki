<!-- TITLE: Project Documentation -->
<!-- SUBTITLE: Installation, UI, Usage, Support, Etc. -->
# Documents
[Project User Guide Template](/uploads/project-documentation/project-user-guide-template.docx "Project User Guide Template")
[Project Installation Instructions Template](/uploads/project-documentation/project-installation-instructions-template.docx "Project Installation Instructions Template")
[Project Design Template](/uploads/project-documentation/project-design-template.docx "Project Design Template")

# Environment Setup
- `c:\Windows\idn.ini`: Includes tons of legacy settings needed for RMS and other IDN programs to run.
- `central.config`: The more current config file. Includes necessary connections to databases for running the `IdnCoreService` and `IdnRmsService`, as well as urls for the services.
- `local.config`: Maintains a list of `central.config` files, and allows for setting the `default="true"` for one file. Configs can be edited with the `IdnCentralConfigEditor` (in Master repo).
    - [Central Configuration Technical Guide](/uploads/project-documentation/central-configuration-technical-guide.docx "Central Configuration Technical Guide")
- **Services**: see Rms Service and Core Service below. These are Web API projects. They can be run locally, or connect to a QA service on the VPN.
    - In addition to the Service project (`IdnRmsService` and `IdnCoreService`), services are dependent on many `Business` and `DataAccess` classes, so any changes to these layers probably require building and possibly restarting the service.
    - To run a service locally, you need to install it after building. You can do this with `installutil idncoreservice.exe`, or for creating a more complex setup (allowing for multiple versions), try something like `sc.exe create "Idn-Dev-IN-CoreService" binPath= "<fullPathToExe>" displayName= "Idn-Dev-IN-CoreService" start= manual`.
    - After installing, Services will show up in the Windows `Services` application, where they can be started, stopped, or restarted.
    - Services should be used for all calls to SQL databases, rather than allowing client applications to access the database directly.

# Security
All environments, by default, have a backdoor login which provides full permissions to do everything in the software.  While the password can be changed, it is almost always left as the default:
- User: `service`
- Password: `1k39-2bs`
# Core Service
The Core Service is the Web API for non-RMS-specific Db calls, such as Security, Folio Reports, Master Search, or Attachments

# RMS
## Incidents
- [Fbi Nibrs Techspec V 2019 1 With Va State Specifics](/uploads/incidents/fbi-nibrs-techspec-v-2019-1-with-va-state-specifics.pdf "Fbi Nibrs Techspec V 2019 1 With Va State Specifics")
- [Fbi And Va Tech Specs 2019 Layout](/uploads/incidents/fbi-and-va-tech-specs-2019-layout.pdf "Fbi And Va Tech Specs 2019 Layout")
- [IBRV3 Readme](ibr-v-3-readme)
- Pre-2018 docs
    - [Va Suppport Table Sources](/uploads/incidents/va-suppport-table-sources.xlsx "Va Suppport Table Sources")
    - [Umw Training And Va Ibr Requirements](/uploads/incidents/umw-training-and-va-ibr-requirements.docx "Umw Training And Va Ibr Requirements")
    - [Delete Incident](/uploads/incidents/delete-incident.sql "Delete Incident")
    - [Clery User Guide](/uploads/incidents/clery-user-guide.docx "Clery User Guide")

## Citations

## Reviewals

## Rms Service
The Rms Service is for RMS-specific Db calls used by any of the RMS applications

# MasterSearch
- [Master Search Design](/uploads/master-search/master-search-design.docx "Master Search Design")
- [Meeting Notes 2017 01 03](/uploads/master-search/meeting-notes-2017-01-03.docx "Meeting Notes 2017 01 03")
- [Meeting Notes 2017 01 10](/uploads/master-search/meeting-notes-2017-01-10.docx "Meeting Notes 2017 01 10")
- [Meeting Notes 2017 01 31](/uploads/master-search/meeting-notes-2017-01-31.docx "Meeting Notes 2017 01 31")
- [Mike Sansones Requirement Fact Finding Notes](/uploads/master-search/mike-sansones-requirement-fact-finding-notes.docx "Mike Sansones Requirement Fact Finding Notes")
# CAD