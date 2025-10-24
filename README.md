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
   - Abra uma nova consulta.
   - Para criar o novo login e senha em nível de servidor, digite: `CREATE LOGIN [TaskDbUser] WITH PASSWORD = 'SenhaForte123!' GO` e aperte `F5` no teclado para executar.
   - Para criar o login e senha do banco, digite: `USE [taskDb]; GO` e tecle `F5` novamente, em seguida, digite: `CREATE USER [taskUser] FOR LOGIN [TaskDbUser]; GO` e tecle `F5`.
   - Conceda as permissões de leitura e gravação digitando:
     `EXEC sp_addrolemember N'db_datareader', N'taskUser';
      EXEC sp_addrolemember N'db_datawriter', N'taskUser';
      GO` e tecle `F5` para executar.
2. ****
