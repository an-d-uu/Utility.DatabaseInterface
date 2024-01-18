# Utility.DatabaseInterface
***
This .dll simplifies the process of connecting to a MS SQL Database and retrieving data from it.

***

**Update 1.1.3 - 01/18/2024
* Added .NET 6.0, .NET 7.0, and .NET 8.0 support.
* .NET 5.0 is out of support, but left support in for compatibility.
* Updated NuGet Package 'Microsoft.Data.SqlClient.2.1.3' to 'Microsoft.Data.SqlClient.2.1.7' due to CVE-2024-0056.

**Update 1.1.2 - 06/02/2021**
* Added sending in the connection string when instantiating the DB_Interface class.
* Corrected an issue with an if statement that caused Async calls to not work in .NET 5.0.

**Update 1.1.1 - 05/31/2021**
* Added .NET 5.0 support.
* .NET 5.0 changes the SQL client from System.Data.SqlClient to Microsoft.Data.SqlClient.
  * This change is supported in .NET Framework 4.6.1 and above, but I am only referencing it in .NET 5 for backwards compatability.
* Corrected an issue with the Release Notes version numbers.

**Update 1.1 - 05/19/2021**
* Rewrote the library to run in C#.
* Updated the methods to include async calls.
* Updated the requests to use using statements.