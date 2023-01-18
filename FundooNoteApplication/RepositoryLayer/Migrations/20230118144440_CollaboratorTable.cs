using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class CollaboratorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollabTable_NoteTable_NoteId",
                table: "CollabTable");

            migrationBuilder.DropForeignKey(
                name: "FK_CollabTable_UserTable_UserId",
                table: "CollabTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollabTable",
                table: "CollabTable");

            migrationBuilder.RenameTable(
                name: "CollabTable",
                newName: "CollaboratorTable");

            migrationBuilder.RenameIndex(
                name: "IX_CollabTable_UserId",
                table: "CollaboratorTable",
                newName: "IX_CollaboratorTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CollabTable_NoteId",
                table: "CollaboratorTable",
                newName: "IX_CollaboratorTable_NoteId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "CollaboratorTable",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollaboratorTable",
                table: "CollaboratorTable",
                column: "CollabId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorTable_NoteTable_NoteId",
                table: "CollaboratorTable",
                column: "NoteId",
                principalTable: "NoteTable",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorTable_UserTable_UserId",
                table: "CollaboratorTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorTable_NoteTable_NoteId",
                table: "CollaboratorTable");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorTable_UserTable_UserId",
                table: "CollaboratorTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollaboratorTable",
                table: "CollaboratorTable");

            migrationBuilder.RenameTable(
                name: "CollaboratorTable",
                newName: "CollabTable");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorTable_UserId",
                table: "CollabTable",
                newName: "IX_CollabTable_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorTable_NoteId",
                table: "CollabTable",
                newName: "IX_CollabTable_NoteId");

            migrationBuilder.AlterColumn<long>(
                name: "Email",
                table: "CollabTable",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollabTable",
                table: "CollabTable",
                column: "CollabId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollabTable_NoteTable_NoteId",
                table: "CollabTable",
                column: "NoteId",
                principalTable: "NoteTable",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollabTable_UserTable_UserId",
                table: "CollabTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
