using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LottoTicketGenerator.Dom
{
    [Table("LottoTickets")]
    public class LottoTicket
    {
        public LottoTicket()
        {
            LottoTicketBoxes = new HashSet<LottoTicketBox>();
        }

        #region Properties
        [Key]
        public int Id { get; set; }
        public int? SuperNumber { get; set; }
        public bool ShowSuperNumber { get; set; }

        [InverseProperty("LottoTicket")]
        public ICollection<LottoTicketBox> LottoTicketBoxes { get; set; }
        #endregion Properties

        public class CreateCommand
        {
            public int NumOfBoxes { get; set; }
            public bool GenerateSuperNumber { get; set; }
        }

        public static LottoTicket Create(CreateCommand cmd)
        {
            var lottoTicket = new LottoTicket();
            lottoTicket.LottoTicketBoxes = new HashSet<LottoTicketBox>();
            for (int i = 1; i <= cmd.NumOfBoxes; i++)
                lottoTicket.LottoTicketBoxes.Add(LottoTicketBox.Create(lottoTicket));
            if (cmd.GenerateSuperNumber)
            {
                var rnd = new Random();
                lottoTicket.SuperNumber = rnd.Next(0, 9);
                lottoTicket.ShowSuperNumber = true;
            }
            return lottoTicket;
        }
    }
}
