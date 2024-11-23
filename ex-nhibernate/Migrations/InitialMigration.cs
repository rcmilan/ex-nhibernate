using ex_nhibernate.Domain;
using FluentMigrator;

namespace ex_nhibernate.Migrations
{
    [Migration(1)]
    public class InitialMigration : Migration
    {
        public override void Down()
        {
            Delete.Table(nameof(User));
        }

        public override void Up()
        {
            Create.Table(nameof(User))
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable();
        }
    }
}
