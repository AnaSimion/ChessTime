using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChessProject.Models.MyDatabaseInitializer
{

    public class DbCtx : DbContext
    {
        public DbCtx() : base("DbConnectionString")
        {
            Database.SetInitializer<DbCtx>(new Initp());
        }


        public DbSet<Board> Boards { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ContactInfo> ContactsInfo { get; set; }
        public DbSet<BoardType> BoardTypes { get; set; }
    }
    
 
    public class Initp : DropCreateDatabaseAlways<DbCtx> //DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(DbCtx ctx)
        {
            BoardType type1 = new BoardType { BoardTypeId = 67, Name = "Magnetic" };
            BoardType type2 = new BoardType { BoardTypeId = 68, Name = "Normal" };


            ctx.BoardTypes.Add(type1);
            ctx.BoardTypes.Add(type2);



            ContactInfo contact1 = new ContactInfo
            {
                PhoneNumber = "0712345678",
                Email = "a@gmail.com",
            };

            ContactInfo contact2 = new ContactInfo
            {
                PhoneNumber = "0713345878",
                Email = "b@gmail.com",
            };

            ContactInfo contact3 = new ContactInfo
            {
                PhoneNumber = "0711345678",
                Email = "c@gmail.com",
            };

            ContactInfo contact4 = new ContactInfo
            {
                PhoneNumber = "0712665679",
                Email = "c@gmail.com",
            };

            ctx.ContactsInfo.Add(contact1);
            ctx.ContactsInfo.Add(contact2);
            ctx.ContactsInfo.Add(contact3);
            ctx.ContactsInfo.Add(contact4);

            ctx.Boards.Add(new Board
            {
                Name = "Old Peak",
                Owner = "Michael Brown",
                Price = 7007,
                Year = 1511,
                Description = "Chess is a board game for two players. It is played in a square board, made of 64 smaller squares, with eight squares on each side. Each player starts with sixteen pieces: eight pawns, two knights, two bishops, two rooks, one queen and one king.",
                Producer = new Producer { Name = "CollinsToys", ContactInfo = contact1 },
                BoardType = type1,
                Materials = new List<Material> {
                    new Material { Name = "Alerce"}
                }
            });
            ctx.Boards.Add(new Board
            {
                Name = "In Defense of Ragnarok",
                Owner = "John Steinbeck",
                Price = 4444,
                Year = 1711,
                Description = "Large folding chessboard for easy game play but also light and convenient to carry around.",
                Producer = new Producer { Name = "Tales Of Old", ContactInfo = contact2 },
                BoardType = type1,
                Materials = new List<Material> {
                    new Material { Name = "European yew"}
                }
            });
            ctx.Boards.Add(new Board
            {
                Name = "Magnetic Board",
                Owner = "Mike Jack",
                Price = 2002,
                Year = 1520,
                Description = "Handcrafted and perfectly-sized chess pieces so that they are easy to identify and comfortable to handle.",
                Producer = new Producer { Name = "ABC", ContactInfo = contact3 },
                BoardType = type1,
                Materials = new List<Material> {
                    new Material { Name = "Kauri"},
                    new Material { Name = "Huon pine"},
                    new Material { Name = "Bald cypress"},
                    new Material { Name = "Japanese larch"}
                }
            });
            ctx.Boards.Add(new Board
            {
                Name = "Ice Crash",
                Owner = "Bob Werner",
                Price = 7227,
                Year = 1520,
                Description = "This Wooden chess set is made from carefully selected high quality pine wood and upgraded magnet, the smooth surface of the entire board ensures optimal touch comfort. A flocking process is used at the bottom of each piece to reduce the sound while moving.",
                Producer = new Producer { Name = "ChessSchooler", ContactInfo = contact4 },
                BoardType = type2,
                Materials = new List<Material> {
                    new Material { Name = "Pine"}
                }
            });

            ctx.SaveChanges();
            base.Seed(ctx);
        }
    }
}