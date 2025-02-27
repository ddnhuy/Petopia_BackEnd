using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeReactionUsingEntityClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reactions",
                schema: "public",
                table: "Reactions");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "public",
                table: "Reactions",
                type: "uuid",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "public",
                table: "Reactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reactions",
                schema: "public",
                table: "Reactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserId",
                schema: "public",
                table: "Reactions",
                column: "UserId");
        }

        private static readonly string[] columns = new[] { "UserId", "TargetId" };

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reactions",
                schema: "public",
                table: "Reactions");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_UserId",
                schema: "public",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "public",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "public",
                table: "Reactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reactions",
                schema: "public",
                table: "Reactions",
                columns: columns);
        }
    }
}
