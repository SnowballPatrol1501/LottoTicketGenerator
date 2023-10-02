using LottoTicketGenerator.Dom;
using Microsoft.EntityFrameworkCore;


namespace LottoTicketGenerator.Dal
{
    public class EntityFrameWorkContext : DbContext
    {
        public DbSet<LottoTicket> LottoTickets { get; set; }
        public DbSet<LottoTicketBox> LottoTicketBoxes { get; set; }

        public string DbPath { get; }

        public EntityFrameWorkContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "LottoTicketGenerator.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder Options)
            => Options.UseSqlite($"Data Source={DbPath}");
    }
}