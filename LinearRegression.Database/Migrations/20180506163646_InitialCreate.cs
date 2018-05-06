using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LinearRegression.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalysisCalculationsSet",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdjustedY = table.Column<string>(nullable: true),
                    AnalysisDataID = table.Column<long>(nullable: false),
                    AverageErrorB0 = table.Column<double>(nullable: false),
                    AverageErrorB1 = table.Column<double>(nullable: false),
                    B0 = table.Column<double>(nullable: false),
                    B1 = table.Column<double>(nullable: false),
                    ExplainedDispersion = table.Column<double>(nullable: false),
                    FEmpirical = table.Column<double>(nullable: false),
                    FTheoretical = table.Column<double>(nullable: false),
                    MaximalErrorB0 = table.Column<double>(nullable: false),
                    MaximalErrorB1 = table.Column<double>(nullable: false),
                    ResidualDispersion = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisCalculationsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalysisDataSet",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnalysisCalculationsId = table.Column<long>(nullable: false),
                    AnalysisInformationId = table.Column<long>(nullable: false),
                    XData = table.Column<string>(nullable: true),
                    XMeaning = table.Column<string>(nullable: true),
                    YData = table.Column<string>(nullable: true),
                    YMeaning = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisDataSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalysisInformationSet",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnalysisDataId = table.Column<long>(nullable: false),
                    CommentIDs = table.Column<string>(nullable: true),
                    CreationDate = table.Column<string>(nullable: true),
                    Descrioption = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisInformationSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentSet",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnalysisInformationID = table.Column<long>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentSet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisCalculationsSet");

            migrationBuilder.DropTable(
                name: "AnalysisDataSet");

            migrationBuilder.DropTable(
                name: "AnalysisInformationSet");

            migrationBuilder.DropTable(
                name: "CommentSet");
        }
    }
}
