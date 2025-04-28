using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class IntegerEngineSizeForCarSpecs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auction_Cars_CarId",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_Auction_Users_UserId",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Auction_AuctionId",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Users_UserId",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_CarImage_Cars_CarId",
                table: "CarImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarSpec_SpecId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Auction_AuctionId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInteraction_Cars_CarId",
                table: "UserInteraction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInteraction_Users_UserId",
                table: "UserInteraction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInteraction",
                table: "UserInteraction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarSpec",
                table: "CarSpec");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarImage",
                table: "CarImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bid",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auction",
                table: "Auction");

            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Auction");

            migrationBuilder.RenameTable(
                name: "UserInteraction",
                newName: "UserInteractions");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "CarSpec",
                newName: "CarSpecs");

            migrationBuilder.RenameTable(
                name: "CarImage",
                newName: "CarImages");

            migrationBuilder.RenameTable(
                name: "Bid",
                newName: "Bids");

            migrationBuilder.RenameTable(
                name: "Auction",
                newName: "Auctions");

            migrationBuilder.RenameIndex(
                name: "IX_UserInteraction_UserId",
                table: "UserInteractions",
                newName: "IX_UserInteractions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInteraction_CarId",
                table: "UserInteractions",
                newName: "IX_UserInteractions_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuctionId",
                table: "Comments",
                newName: "IX_Comments_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_CarImage_CarId",
                table: "CarImages",
                newName: "IX_CarImages_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_UserId",
                table: "Bids",
                newName: "IX_Bids_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_AuctionId",
                table: "Bids",
                newName: "IX_Bids_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_UserId",
                table: "Auctions",
                newName: "IX_Auctions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Auction_CarId",
                table: "Auctions",
                newName: "IX_Auctions_CarId");

            migrationBuilder.AlterColumn<int>(
                name: "EngineSize",
                table: "CarSpecs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Emissions",
            //    table: "CarSpecs",
            //    type: "integer",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "text");
            migrationBuilder.Sql(
    @"ALTER TABLE ""CarSpecs""
      ALTER COLUMN ""Emissions"" TYPE integer
      USING ""Emissions""::integer;");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Bids",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "StartingPrice",
                table: "Auctions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<int>(
                name: "Reserve",
                table: "Auctions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInteractions",
                table: "UserInteractions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarSpecs",
                table: "CarSpecs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarImages",
                table: "CarImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auctions",
                table: "Auctions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Cars_CarId",
                table: "Auctions",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_UserId",
                table: "Auctions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Auctions_AuctionId",
                table: "Bids",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_UserId",
                table: "Bids",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarSpecs_SpecId",
                table: "Cars",
                column: "SpecId",
                principalTable: "CarSpecs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Auctions_AuctionId",
                table: "Comments",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInteractions_Cars_CarId",
                table: "UserInteractions",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInteractions_Users_UserId",
                table: "UserInteractions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Cars_CarId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_UserId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Auctions_AuctionId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Users_UserId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_CarImages_Cars_CarId",
                table: "CarImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarSpecs_SpecId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Auctions_AuctionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInteractions_Cars_CarId",
                table: "UserInteractions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInteractions_Users_UserId",
                table: "UserInteractions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInteractions",
                table: "UserInteractions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarSpecs",
                table: "CarSpecs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarImages",
                table: "CarImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auctions",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "Reserve",
                table: "Auctions");

            migrationBuilder.RenameTable(
                name: "UserInteractions",
                newName: "UserInteraction");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "CarSpecs",
                newName: "CarSpec");

            migrationBuilder.RenameTable(
                name: "CarImages",
                newName: "CarImage");

            migrationBuilder.RenameTable(
                name: "Bids",
                newName: "Bid");

            migrationBuilder.RenameTable(
                name: "Auctions",
                newName: "Auction");

            migrationBuilder.RenameIndex(
                name: "IX_UserInteractions_UserId",
                table: "UserInteraction",
                newName: "IX_UserInteraction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInteractions_CarId",
                table: "UserInteraction",
                newName: "IX_UserInteraction_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuctionId",
                table: "Comment",
                newName: "IX_Comment_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_CarImages_CarId",
                table: "CarImage",
                newName: "IX_CarImage_CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_UserId",
                table: "Bid",
                newName: "IX_Bid_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_AuctionId",
                table: "Bid",
                newName: "IX_Bid_AuctionId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_UserId",
                table: "Auction",
                newName: "IX_Auction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Auctions_CarId",
                table: "Auction",
                newName: "IX_Auction_CarId");

            migrationBuilder.AlterColumn<decimal>(
                name: "EngineSize",
                table: "CarSpec",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Emissions",
                table: "CarSpec",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Bid",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "StartingPrice",
                table: "Auction",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "Auction",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInteraction",
                table: "UserInteraction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarSpec",
                table: "CarSpec",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarImage",
                table: "CarImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bid",
                table: "Bid",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auction",
                table: "Auction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_Cars_CarId",
                table: "Auction",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_Users_UserId",
                table: "Auction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Auction_AuctionId",
                table: "Bid",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Users_UserId",
                table: "Bid",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarImage_Cars_CarId",
                table: "CarImage",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarSpec_SpecId",
                table: "Cars",
                column: "SpecId",
                principalTable: "CarSpec",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Auction_AuctionId",
                table: "Comment",
                column: "AuctionId",
                principalTable: "Auction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInteraction_Cars_CarId",
                table: "UserInteraction",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInteraction_Users_UserId",
                table: "UserInteraction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
