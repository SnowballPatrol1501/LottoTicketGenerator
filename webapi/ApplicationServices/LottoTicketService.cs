using LottoTicketGenerator.Dom;
using System.ComponentModel.Composition;
using webapi.Controllers;

namespace webapi.ApplicationServices
{
    [Export(typeof(LottoTicketService)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class LottoTicketService : ServiceBase
    {
        public LottoTicketService(ILogger logger) : base(logger)
        {
        }

        public class CreateLottoTicketCommand : LottoTicket.CreateCommand
        {
        }

        public int CreateLottoTicket(CreateLottoTicketCommand cmd)
        {
            return ExecuteCommand<int>(context =>
            {
                var Ticket = LottoTicket.Create(cmd);
                context.LottoTickets.Add(Ticket);
                context.SaveChanges();
                return Ticket.Id;
            });
        }
    }
}
