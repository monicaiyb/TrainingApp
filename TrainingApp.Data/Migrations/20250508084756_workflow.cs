using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class workflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkflowConfigurations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Process = table.Column<int>(type: "int", nullable: false),
                    iscreatedby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ismodifiedby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modifiedon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isdeleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deletedon = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowConfigurations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowConfigurationSteps",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    iscreatedby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ismodifiedby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modifiedon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isdeleted = table.Column<bool>(type: "bit", nullable: false),
                    deletedby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deletedon = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowConfigurationSteps", x => x.id);
                    table.ForeignKey(
                        name: "FK_WorkflowConfigurationSteps_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkflowConfigurationSteps_WorkflowConfigurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "WorkflowConfigurations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowConfigurationSteps_ConfigurationId",
                table: "WorkflowConfigurationSteps",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowConfigurationSteps_RoleId",
                table: "WorkflowConfigurationSteps",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkflowConfigurationSteps");

            migrationBuilder.DropTable(
                name: "WorkflowConfigurations");
        }
    }
}
