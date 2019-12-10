<!-- TITLE: Sql Server Environment Setup -->
<!-- SUBTITLE: For starting a clean local Db Environment -->

# SQL SERVER INSTANCE
Download and install [either the Developer or Express version](https://www.microsoft.com/en-us/sql-server/sql-server-downloads). Use the default install options.

# SSMS
- Download and install [Sequel Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017)
- Open SSMS, and click `Connect`. Use Windows credentials the first time, and leave the address as `.`
- Right-click on the server instance that appears in the object explorer and select `Properties`. Go to the `Security` tab, and change the Server Authentication to allow both `SQL Server and Windows Authentication mode`.

# IdnEnvironmentInitializer
- Find the `IdnEnvironmentInitializer` project in the 5.0 master repository. Run the program, being sure to select all variables carefully.
- The Initializer takes quite a while. Don't close it! It will tell you when it's done.
- You should now have a new local config file in your central config.