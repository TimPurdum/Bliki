<!-- TITLE: New Puffin Resources -->
<!-- SUBTITLE: Training and References for New Hires (Nuffins) -->

# Orientation
[Orientation Agenda](developer-orientation-agenda)
[Workstation Setup](workstation-setup)
# Reading Material
## Getting Started
[List of Git Repositories](list-of-git-repositories)
[Common Shared Folders](common-shared-folders)
## Policies
[Coding Conventions](coding-conventions)
[Feature Implementation Workflow](feature-implementation-workflow)
[Web API Standards](webapi-standards)
## Source Code
[Master Solution](master-solution)
[Project and Namespace Guidelines for the Master Solution](project-and-namespace-guidelines)
[Architecture](architecture)
## Technical Guides
`I:\Steve\Project Documentation\Central Configuration\Central Configuration Technical Guide.docx`
`I:\Steve\Project Documentation\Reporting\PageDoc Developer Guide.docx`
`I:\Steve\Project Documentation\Database Upgrader\Database Upgrader Technical Guide.docx`
## Product Documentation
`I:\Steve\Project Documentation\Security\Security User Guide.docx`
`I:\Steve\Project Documentation\Security\Security Installation Instructions.docx`
`I:\Steve\Project Documentation\Workstations\Workstations User Guide.docx`
`I:\Steve\Project Documentation\Workstations\Workstation Installation Instructions.docx`
# Communication
Besides phone and e-mail, there are a couple of other common methods for intra-company electronic comminication.

## Trillian
Trillian is an old-school instant messaging product.  It was a popular freeware alternative back when AIM and MSN Messenger were in their heyday.  Even though the other IM services, with which it used to interface, have since died, it has retained some of it's popularity.  All IDN employees are required to install Trillian, and they are expected to be available on Trillian during normal business hours.  Therefore, despite being cumbersome for group chats, it serves as a very convenient way to directly contact anyone in the company without having to pick up the phone.  It also has a handy built-in screen shot capture/send feature.

Be aware that with Trillian, it is possible to pull a message to one device, and then not receive it on another, so know where you are logged in.

## Slack
While all of us are on Trillian, most of us find Slack to be a more convenient communication tool, particularly for group chats.  Therefore, Slack is our primary communication tool on the Puffin team, both for group chats and for direct messages.  Slack has built-in support for video chats and screen sharing, which is convenient and easy to use, however, those features are unfortunately not as stable or functional as other options, so they tend to go unused.

### Channels
- **puffins**: This is the channel for the entire Puffin team.  Since everyone sees these messages, in order to cut down on annoying distractions, please only use this channel for business-related discussions which affect or interest the whole Puffin team.  If the topic only concerns a few of the team-members (because it is related to a project on which only they are working), use one of the branch-specific channels, instead, if possible.
- **development**: This is the channel for the entire development team.  Since everyone sees these messages, in order to cut down on annoying distractions, please only use this channel for business-related discussions which affect or interest the whole development team.  For the most part, the only developers who use slack are on the Puffin team anyway, so most development talk usually ends up on the `#puffins` channel, leaving this channel relatively inactive.  However, occasionally people will post language/IDE/framework-related news items and discuss them.
- **general**: This is the channel for the entire company.  Since everyone sees these messages, in order to cut down on annoying distractions, please only use this channel for business-related discussions which affect or interest the whole company.  Much like the `#development` channel, since Puffin teammembers are the most active on Slack, and they have their own channel, this one is relatively inactive.  It is occasionally used, though, for company-wide announcements.
- **random**: This channel is for discussing non-business-related topics with anyone in the company.
- **git**: This channel should be used for all questions, announcements, and discussions related to Git, its various clients, the `IDN-GIT` server, our repositories, our branching methogology, etc.
- **branch channels**: Each Axosoft ticket (development feature or bug) can have a related slack channel, with the ticket number, such as `f1673_rw_mastersearch`

## GoToMeeting
Some developers have a GoToMeeting account.  GoToMeeting, despite it's failings, tends to be the best option when both video chat and screen sharing are needed simultaneously.  However, due to its cost, the company has chosen to only pay for accounts for those who need it often.  If you find that you do need it, though, let some one know.  Paying for additional accounts is not out of the question.

## People
- **Allen Sharrer**: Allen handles all our builds and deployments.  He performs most of the installations for new customers as well as the upgrades for existing customers.  He coordinates QA testing and the release cycle.  He started as a developer, and he still spends some time every day in the code fixing bugs, maintaining some of his own code, improving our deployment tools, and creating one-off utilities.  He's been here for a long time and has had his hands in everything, so he knows a little something about almost everything.  As such, he's often the best source for answers to questions about our software or our process.  That being said, he's very busy, so he's not always the easiest person to get a hold of.
- **Steve Doggart**: The orinal `S.T.E.V.E.` (Source of Technical Experience and Verbose Explanations). Also very busy. He likes to actually do his own work, and not fix yours. But, that being said, he has the greatest knowledge of our code base, guidelines, and requirements.
- **Doug Blenman, Jr.**: Technically, his father is in charge, but for all intents and purposes, especially for the public-safety products that we work on, he's the CEO.  Before anything else, though, he's a salesman.  He's more-often-than-not on the road doing demos, visiting existing customers, attending shows and conferences, training new customers, and overseeing go-lives.  He's very busy too, but he's a good person to check in with when implementing new features or needing feedback on direction.

# Workflow
## Axosoft
http://www.idnetworks.com/Axosoft/
Axosoft is the ticket-tracking system used at IDN. 
- There are two areas of tracking used: `Features` and `Bugs`
- The most important fields to set on any ticket are `Workflow Step`, `Assigned To`, `Primary Assembly`, and `Planned Build Number`
- See the [Feature Implementation Workflow](feature-implementation-workflow) for details on how to create and use tickets.
- Tickets are also (correctly) referred to as `On-Times`
- Once a ticket is created, that ticket number, plus an `f` or `b` should be used for git branches and slack channels

## Git
https://git-scm.com/ - The source of commands
- You can use git via command line (git-bash is good) or the VS GUI, However, some workflow steps, such as `merge --squash` are only supported in the command line.
- The [Feature Implementation Workflow](feature-implementation-workflow) describes the basic workflow for using git
- All commits should be commented, and whenever possible should include the Axosoft ticket # (e.g., `f2141`)
- It's a good idea to ask for help if git is new to you
- **repo**: The origin repositories are at `http://idn-git/git/`. Most work is done in the `master` repo (`http://idn-git/git/master`). Not to be confused with the `master` branch :)
- Follow the [Recommended Git Configuration](recommended-git-configuration) to configure git

## Visual Source Safe
For use with [VB6](#Languages) code.
- `Check Out`: Downloads and makes writable a copy of the files to open in the editor. While checked out, only that user can edit/check in
- `Check In`: Similar to `commit`/`push` in git, saves to the repo.

# Development
## Languages
### C#
- The preferred modern language for IDN code. See the [Coding Conventions](coding-conventions) document for details on proper formatting and usage.
### VB.Net
- Before the transfer to C#, a lot of our master repository was written in VB.Net. While completely compatible with C#, the syntax is quite different. Since languages cannot be mixed within a project, it is important to be able to read/write VB.Net in order to maintain existing systems.
### VB6
- The original language used at IDN. Many entire systems are still written in this language. 
- VB6 requires it's own `VisualBasic` editor, and the code is saved to `Visual Source Safe` (VSS), instead of git. 
- Usually, we set up a VM environment for working with VB6, which includes the editor and VSS.
- Syntax-wise, VB6 is similar to VB.NET, but there are some differences that can throw you. Also, get used to debugging with `MsgBox` and no IntelliSense.
- Ask Tim Covert, Mike Sansone, Tim Purdum, or Steve Doggart for help in VB6
### T-SQL
- Used for our Sql-Server databases. SQL commands are written either in the `DataAccess` layer of the `master` solution, or as `StoredProcedures`
- Database scripts are in the `master` solution under `Deployment\DbUpgrade`. *Existing scripts should never be edited*. Instead, create a new script that increments the Db version number. 
- Run all upgrade scripts locally, and have Allen proof-read them before committing to the `master` branch.

## Environments
### Visual Studio
- The main `.NET` IDE from MS, use the [Master Solution Readme](readme-master-solution) `Pre-Requisites to Build` section to get started.
### Rider
- An optional, not-IDN-provided IDE, which several Puffins like to use for development, as it often is more responsive to the large size of the `master` solution.
- Provides additional styling, formatting, and error suggestions.
### VS Code
- Ideal for opening individual code files or non-solution-based code
### VisualBasic
- The editor for VB6

## Projects
See the [Project Documentation](project-documentation) for details on how to run and develop existing apps.

# Support
All of the information regarding what software is installed at each customer site, where it's installed, how it's configured, and how to access it remotely is kept in a OneNote document called the [CCI](onenote://idn-fileserver/Service/IDN_CCI_365/IDN_CCI).  Since the document contains very sensitive information, it must never be shared with non-IDN-employees.  When opening any of the tabs in the document, it will ask for a password as a safety precaution, in case someone on location at a customer site leaves it open on their machine.  The password is `idnservice`.

Most of our customers have at least one workstation installed on their local network which is dedicated for our use, is always left running, and is setup for remote access.  We call these workstations the "24x7 machines".  Most of the 24x7 machines are accessible via [Bomgar](https://remotesupport.idnetworks.com/).

Our technical support staff logs all support calls in their [Help Desk](http://www.idnetworks.com/support-login/) software.  As developers, we only have limited access to their Help Desk software, if any at all.  They also use the help desk to store contact information for the primary contacts at each customer agency.  If you need information about a help desk ticket, or you need access to their contact information, contact Allen Sharer, or one of the support technicians for assistance.

