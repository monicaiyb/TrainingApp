using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class workflowEnginAndhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkflowEngines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentPosition = table.Column<int>(type: "int", nullable: false),
                    ConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Record = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Process = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_WorkflowEngines", x => x.id);
                    table.ForeignKey(
                        name: "FK_WorkflowEngines_WorkflowConfigurations_ConfigId",
                        column: x => x.ConfigId,
                        principalTable: "WorkflowConfigurations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowStateHistory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EngineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutorUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: true),
                    StepId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NextStep = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_WorkflowStateHistory", x => x.id);
                    table.ForeignKey(
                        name: "FK_WorkflowStateHistory_WorkflowEngines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "WorkflowEngines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowEngines_ConfigId",
                table: "WorkflowEngines",
                column: "ConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStateHistory_EngineId",
                table: "WorkflowStateHistory",
                column: "EngineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkflowStateHistory");

            migrationBuilder.DropTable(
                name: "WorkflowEngines");
        }
    }
}
