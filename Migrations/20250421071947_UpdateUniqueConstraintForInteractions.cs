using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace greenswamp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUniqueConstraintForInteractions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_interactions_user_id_post_id_interaction_type",
                table: "interactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_interactions_user_id_post_id_interaction_type",
                table: "interactions",
                columns: new[] { "user_id", "post_id", "interaction_type" },
                unique: true);
        }
    }
}
