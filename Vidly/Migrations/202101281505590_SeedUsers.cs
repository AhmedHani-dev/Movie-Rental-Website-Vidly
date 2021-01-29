namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'28bdcf85-6a5a-4bc0-9e95-e01c2a28d0be', N'admin@vidly.com', 0, N'ANf2rOlwLkeZLSiSPXUcydKCjg+Xhqp+M+h1F72P31KmcAn5gek0payyV+V4wP2xFg==', N'2d52084e-e49c-4abf-8383-73eb4ad50780', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8a471554-6372-47a2-8c5c-04ca7019a10e', N'guest@vidly.com', 0, N'ACKEpbcUfFIjXeh7dFzNPpOgTPFFTdohlbWhNTrlfkJwVpwTD+fesDZZb6pQfgKRgg==', N'57c57f25-aff6-4fe3-904f-bd7a5cfb25a0', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7380bc23-8c34-4688-ac22-ce09a31233ed', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'28bdcf85-6a5a-4bc0-9e95-e01c2a28d0be', N'7380bc23-8c34-4688-ac22-ce09a31233ed')
");
        }
        
        public override void Down()
        {
        }
    }
}
