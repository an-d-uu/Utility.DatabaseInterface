# Utility.DatabaseInterface
***
This .dll simplifies the process of connecting to a MS SQL Database and retrieving data from it.

***

**Update 1.1.1 - 05/31/2021**
* Added .NET 5.0 support.
* .NET 5 changes the SQL client from System.Data.SqlClient to Microsoft.Data.SqlClient.
  * This change is supported in .NET Framework 4.6.1 and above, but I am only referencing it in .NET 5 for backwards compatability.
* Corrected an issue with the Release Notes version numbers.

**Update 1.1 - 05/19/2021**
* Rewrote the library to run in C#.
* Updated the methods to include async calls.
* Updated the requests to use using statments.