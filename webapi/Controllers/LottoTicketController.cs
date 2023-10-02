using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Composition;
using webapi.ApplicationServices;

namespace webapi.Controllers
{
    [Export(typeof(LottoTicketController)), PartCreationPolicy(CreationPolicy.NonShared)]
    [Route("LottoTicket/{action=Index}")]
    public class LottoTicketController : ControllerBase
    {
        public LottoTicketController(ILogger<LottoTicketController> logger, LottoTicketService lottoTicketService) : base(logger)
        {
            this.LottoTicketService = lottoTicketService;
        }

        LottoTicketService LottoTicketService;

        public class LottoTicketListResult
        {
            public int id { get; set; }
            public bool hasSuperNumber { get; set; }
            public int countOfBoxes { get; set; }
        }

        [HttpGet]
        public IEnumerable<LottoTicketListResult> GetLottoTickets()
        {
            LogMethod();
            return ReadDtos(context => context.LottoTickets.Select(lt => new LottoTicketListResult()
            {
                id = lt.Id,
                hasSuperNumber = lt.SuperNumber.HasValue,
                countOfBoxes = lt.LottoTicketBoxes.Count()
            }).ToArray());
        }

        [HttpPost]
        public int CreateLottoTicket([FromBody] LottoTicketService.CreateLottoTicketCommand cmd)
        {
            LogMethod();
            return LottoTicketService.CreateLottoTicket(cmd);
        }

        public class LottoTicketDetailResult
        {
            public int id { get; set; }
            public int? superNumber { get; set; }
            public bool showSuperNumber { get; set; }
            public IEnumerable<LottoTicketDetailBoxResult> ticketBoxes { get; set; }
        }

        public class LottoTicketDetailBoxResult
        {
            public int id { get; set; }
            public IEnumerable<int> numbers { get; set; }
        }

        [HttpGet]
        public LottoTicketDetailResult GetLottoTicketDetail(int id)
        {
            LogMethod();
            return ReadDto(context => context.LottoTickets.Include("LottoTicketBoxes").Where(lt => lt.Id == id).ToArray()
            .Select(lt => new LottoTicketDetailResult()
            {
                id = lt.Id,
                superNumber = lt.SuperNumber,
                showSuperNumber = lt.ShowSuperNumber,
                ticketBoxes = lt.LottoTicketBoxes.Select(ltb => new LottoTicketDetailBoxResult()
                {
                    id = ltb.Id,
                    numbers = ltb.Numbers.Data.OrderBy(d => d)
                })
            }).Single());
        }
    }
}