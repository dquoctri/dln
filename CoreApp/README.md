Generating the Private Key -- Windows
openssl genrsa -out private.key 2048
openssl rsa -in private.key -out public.pem -pubout -outform PEM

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Relational
dotnet add package Microsoft.EntityFrameworkCore.InMemory

Install-Package Microsoft.EntityFrameworkCore.Relational

dotnet ef migrations add InitialCreate --project Authentication\Authentication.Context
dotnet ef migrations remove InitialCreate --project Authentication\Authentication.Context
dotnet ef database update --project Authentication\Authentication.Context
dotnet ef database drop InitialCreate --project Authentication\Authentication.Context

dotnet ef database update InitialCreate
dotnet ef database update 20180904195021_InitialCreate --connection your_connection_string

Scaffold-DbContext "Server=localhost,51433;Database=dln_auth;User Id=sa;Password=StrongP@ssword;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models




dotnet ef migrations add CreateTimeSeriesDataTable --project Authentication\Authentication.Context