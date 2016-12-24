namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class overrideconventionforGigsandGenres1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gigs", "Genre_GenreId", "dbo.Genres");
            DropIndex("dbo.Gigs", new[] { "Genre_GenreId" });
            AlterColumn("dbo.Gigs", "Genre_GenreId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Gigs", "Genre_GenreId");
            AddForeignKey("dbo.Gigs", "Genre_GenreId", "dbo.Genres", "GenreId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "Genre_GenreId", "dbo.Genres");
            DropIndex("dbo.Gigs", new[] { "Genre_GenreId" });
            AlterColumn("dbo.Gigs", "Genre_GenreId", c => c.Byte());
            CreateIndex("dbo.Gigs", "Genre_GenreId");
            AddForeignKey("dbo.Gigs", "Genre_GenreId", "dbo.Genres", "GenreId");
        }
    }
}
