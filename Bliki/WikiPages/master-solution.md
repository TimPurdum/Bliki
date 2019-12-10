<!-- TITLE: Master Solution -->

# Description
The Master solution is a standard Visual Studio solution, called `Master.sln`, which, in principal, contains all of the projects, for all IDN products, all under one roof.  The master solution is what is compiled by the Jenkins autotomated build scripts, so if a project needs to be included in the automated build process, it should be added to the master solution.  In reality, though, it's not that inclusive.  Most Biometrics, Jail, and Civil-Processing projects are not included in the master solution.  In addition to that, since it's a .NET solution, projects written in other languages, like VB6, are also excluded.  Other build mechanisms are used for each of those kinds of projects, some more automated than others.

The master solution, along with all of its dependencies, are included in a single [Git repository](list-of-git-repositories), naturally called the "Master Repository".  The main (i.e. trunk) development branch in the repository is the `master` branch.  While the word "master" is often used interchangeably, without confusion, to refer to the solution or the repository, the fact that it's also the name of the primary branch can cause confusion.  Sometimes developers will say "master/master", "master/master/master", or "3M", to refer to the master branch of the master solution in the master repository.  This unfortunate confusion was caused by the fact that the master solution was created years before we switched from Visual Source Safe to Git for our source control.  In VSS, there was no such confusion.

# Purpose
The purpose of the master solution is to provide a single solution containing all IDN .NET projects.  Not only is the master solution used for automated builds, but most developers use it regularly when working on their individual projects.  Among the many advantages of having all of the projects in one solution are:
* More open sharing of code and, consequently, less duplication
* More natural cross-product exposure and training for developers
* Ability to easily search the entire code-base
* Easier to view/run all unit tests
* Ability to use project references for all in-house dependencies

That last item is of particular importance.  By having all projects in one solution, all dependency-assembly references can be project references rather than file references.  Project references provide many advantages over file references, most notably:
* Full IDE support for things like intellisense, find-all-references, and refactoring tools (e.g. identifier renaming)
* Easier debugging
* Easier scripting of automated compiles
* More convenient to implement consistent versioning across projects

The goal is that any developer can, from a new machine, clone the master repository, open the master solution, immediately have access to all of our code, and easily build and run any of our software.  As you might expect, despite our continued best efforts, it's not quite that easy.  There are some pre-requisite steps which must be taken before the solution will successfully build and run.  All of those steps are listed in the `README.md` file which can be found in the root of the master repository.

# Guidelines
While members of the Puffins team have some voluntary guidelines, such as their [Coding Conventions](coding-conventions), that they follow in all of their projects, there are a fewer number of guidelines that we intend to enforce solution-wide.  The following are the guidelines that do apply for the whole solution:
* [Project and Namespace Guidelines](project-and-namespace-guidelines)
* All library references must be one of the following
	* A project reference to another project in the master solution
	* A NuGet package reference
	* A file reference to a DLL included in a folder in the master repository
	* A file reference to a DLL installed by an SDK on every development workstation (these should be kept to a minimum and instructions to install the SDK must be included in the `README.md` file in the root of the repository)
* There are a number of branch-specific guidelines which are listed in the `README.md` file in the root of the repository.  Since the `README.md` file is in the repository, its contents may vary on each branch.  
 
Only brach-specific guidelines should be put in the `README.md` file.  All other guidelines for the master repository should be listed here.

# Recommendations
One obvious downside to having a giant solution is IDE performance.  Visual Studio has been known to struggle with it.  It can take a long time to load, build, and switch branches.  Since VS is still a 32-bit process, it can even run low on memory and start shutting down important features like intelisense.  Since 2015, Microsoft has been putting a lot of effort into improving the performance of Visual Studio when working with large solutions, but even now it can still cause some difficulties.  Here are some ideas for ways to improve performance:

## Subsets
Create a feature-specific solution which only has the one or two projects you really need to run, along with all of their dependencies.  Alternatively, in VS 2019 and later, you can use solution filters (`.slnf` files) for the same purpose.  There are already a number of solutions and solution-filters in the repository.  If one of them suits your needs, you can use it.  If not, you can create your own and add it to the repository to share it with others.  The only thing to remember is, if you are using your own solution, and you need to add a new project, that project will also need to be added to the master solution, since that is the solution used by Jenkins to build everything.

## Disabling VS Features
Turning off some of the resource-intensive features in Visual Studio, when you don't need them, can help.  These features can relatively easily be toggled on/off as necessary:
* CodeLens
* Diagnostic Tools while Debugging
* Full Solution Analysis

## Other IDEs
Some have found it convenient to use other IDEs like Rider or VS Code.

