namespace Howest.NMCT.Shredder.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        PlaceId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        Name = c.String(),
                        Description = c.String(),
                        Rating = c.Int(),
                    })
                .PrimaryKey(t => t.PlaceId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        PictureId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                        User_UserId = c.Int(),
                        Place_PlaceId = c.Int(),
                        Rating = c.Int(),
                    })
                .PrimaryKey(t => t.PictureId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Places", t => t.Place_PlaceId)
                .Index(t => t.User_UserId)
                .Index(t => t.Place_PlaceId);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                        User_UserId = c.Int(),
                        Place_PlaceId = c.Int(),
                        Rating = c.Int()
                    })
                .PrimaryKey(t => t.VideoId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Places", t => t.Place_PlaceId)
                .Index(t => t.User_UserId)
                .Index(t => t.Place_PlaceId);
            
            CreateTable(
                "dbo.PlaceComments",
                c => new
                    {
                        PlaceCommentId = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        User_UserId = c.Int(),
                        Place_PlaceId = c.Int(),
                    })
                .PrimaryKey(t => t.PlaceCommentId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Places", t => t.Place_PlaceId)
                .Index(t => t.User_UserId)
                .Index(t => t.Place_PlaceId);
            
            CreateTable(
                "dbo.VideoComments",
                c => new
                    {
                        VideoCommentId = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        User_UserId = c.Int(),
                        Place_PlaceId = c.Int(),
                        Video_VideoId = c.Int(),
                    })
                .PrimaryKey(t => t.VideoCommentId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Places", t => t.Place_PlaceId)
                .ForeignKey("dbo.Videos", t => t.Video_VideoId)
                .Index(t => t.User_UserId)
                .Index(t => t.Place_PlaceId)
                .Index(t => t.Video_VideoId);
            
            CreateTable(
                "dbo.PictureComments",
                c => new
                    {
                        PictureCommentId = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        User_UserId = c.Int(),
                        Place_PlaceId = c.Int(),
                        Picture_PictureId = c.Int(),
                    })
                .PrimaryKey(t => t.PictureCommentId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Places", t => t.Place_PlaceId)
                .ForeignKey("dbo.Pictures", t => t.Picture_PictureId)
                .Index(t => t.User_UserId)
                .Index(t => t.Place_PlaceId)
                .Index(t => t.Picture_PictureId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PictureComments", new[] { "Picture_PictureId" });
            DropIndex("dbo.PictureComments", new[] { "Place_PlaceId" });
            DropIndex("dbo.PictureComments", new[] { "User_UserId" });
            DropIndex("dbo.VideoComments", new[] { "Video_VideoId" });
            DropIndex("dbo.VideoComments", new[] { "Place_PlaceId" });
            DropIndex("dbo.VideoComments", new[] { "User_UserId" });
            DropIndex("dbo.PlaceComments", new[] { "Place_PlaceId" });
            DropIndex("dbo.PlaceComments", new[] { "User_UserId" });
            DropIndex("dbo.Videos", new[] { "Place_PlaceId" });
            DropIndex("dbo.Videos", new[] { "User_UserId" });
            DropIndex("dbo.Pictures", new[] { "Place_PlaceId" });
            DropIndex("dbo.Pictures", new[] { "User_UserId" });
            DropIndex("dbo.Places", new[] { "UserId" });
            DropForeignKey("dbo.PictureComments", "Picture_PictureId", "dbo.Pictures");
            DropForeignKey("dbo.PictureComments", "Place_PlaceId", "dbo.Places");
            DropForeignKey("dbo.PictureComments", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.VideoComments", "Video_VideoId", "dbo.Videos");
            DropForeignKey("dbo.VideoComments", "Place_PlaceId", "dbo.Places");
            DropForeignKey("dbo.VideoComments", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.PlaceComments", "Place_PlaceId", "dbo.Places");
            DropForeignKey("dbo.PlaceComments", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Videos", "Place_PlaceId", "dbo.Places");
            DropForeignKey("dbo.Videos", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Pictures", "Place_PlaceId", "dbo.Places");
            DropForeignKey("dbo.Pictures", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Places", "UserId", "dbo.Users");
            DropTable("dbo.PictureComments");
            DropTable("dbo.VideoComments");
            DropTable("dbo.PlaceComments");
            DropTable("dbo.Videos");
            DropTable("dbo.Pictures");
            DropTable("dbo.Places");
            DropTable("dbo.Users");
        }
    }
}
