# Projeto TaskList - TaskManager
Este projeto é um sistema completo de gerenciamento de tarefas, implementando uma arquitetura limpa com backend em ASP.NET Core Web API e frontend em WPF com MVVM.

# Arquitetura
- Backend: ASP.NET Core Web API (Clean Architecture)
- Frontend: WPF com padrão MVVM
- Banco de Dados: SQL Server
- ORM: Entity Framework Core

# Pré-requisitos
- .NET 8.0 SDK
- Visual Studio 2022
- SQL Server

## Passos para configurar o projeto

1. **Configurar o SQL Server**

   - Abra o Microsft SQL Server Management Studio e realize o login utilizando as credenciais do Windows ou a sua própria
   - Em "Banco de Dados" crie um novo banco de dados com o nome **taskDB**.
   - Abra uma nova consulta e execute:
     <pre>CREATE LOGIN [taskDbUser] WITH PASSWORD = 'SenhaForte123!';
         GO</pre>
   - Para criar o login e senha do banco, execute:
     <pre>USE [taskDb];
           GO</pre>
   - Em seguida, digite:
     <pre>CREATE USER [taskUser] FOR LOGIN [taskDbUser];
           GO</pre>
   - Conceda as permissões de leitura e gravação:
     <pre>EXEC sp_addrolemember N'db_datareader', N'taskUser';
      EXEC sp_addrolemember N'db_datawriter', N'taskUser';
      GO</pre>
     
2. **Visual Studio 2022**

   -Abra o Visual Studio 2022, na pasta raiz do projeto TaskList-DS há um arquivo "appsettings.json", certifique-se de que o login e senha criados para o acesso ao banco estejam corretos:
   `"ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=taskDB;User Id=taskUser;Password=SenhaForte123!;Trusted_Connection=True;TrustServerCertificate=True;"

},`
