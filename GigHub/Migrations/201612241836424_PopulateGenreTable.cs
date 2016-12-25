namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into  Genres(GenreId, GenreName) values(1, 'Jazz')");
            Sql("insert into  Genres(GenreId, GenreName) values(2, 'blues')");
            Sql("insert into  Genres(GenreId, GenreName) values(3, 'rock')");
            Sql("insert into  Genres(GenreId, GenreName) values(4, 'country')");
        }
        
        public override void Down()
        {
            Sql("delete from Genres where GenreId in (1, 2, 3, 4)");
        }
    }
}
