using System;
using Microsoft.EntityFrameworkCore.Migrations;
using TaskList_DS.Domain.Entities;

#nullable disable

namespace TaskList_DS.Migrations
{
    /// <inheritdoc />
    public partial class StarterMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "taskEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoneAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskEntities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "taskEntities",
                columns: new[] { "TaskTitle", "TaskDescription", "CreatedAt", "DoneAt", "Status" },
                values: new object[,]
                {
                    { "Configurar banco", "Criar base e usuário SQL Server", DateTime.Now, null, Domain.Entities.TaskStatus.Pending.ToString() },
                    { "Implementar CRUD", "Criar telas e repositórios no projeto", DateTime.Now, null, Domain.Entities.TaskStatus.Pending.ToString() }
                });
        }



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "taskEntities");
        }
    }
}
