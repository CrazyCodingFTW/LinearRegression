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
                name: "AnalysisDataSet",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    CreationDate = table.Column<string>(nullable: true),
                    Descrioption = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisInformationSet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysisDataSet");

            migrationBuilder.DropTable(
                name: "AnalysisInformationSet");
        }
    }
}
