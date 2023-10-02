using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using webapi.Dom.Generic;

namespace LottoTicketGenerator.Dom
{
    [Table("LottoTicketBoxes")]
    public class LottoTicketBox
    {
        #region Properties
        public static int NumbersPerLottoTicketBox = 6;
        [Key]
        public int Id { get; set; }
        public DistinctCollection<int> Numbers { get; set; }
        public int LottoTicketId { get; set; }

        [ForeignKey(nameof(LottoTicketId)), InverseProperty("LottoTicketBoxes")]
        public LottoTicket? LottoTicket { get; set; }
        #endregion Properties

        public static LottoTicketBox Create(LottoTicket lottoTicket)
        {
            var rnd = new Random();
            var lottoTicketBox = new LottoTicketBox();
            lottoTicketBox.LottoTicket = lottoTicket;
            lottoTicketBox.LottoTicketId = lottoTicket.Id;
            lottoTicketBox.Numbers = new DistinctCollection<int>();
            InitNumbers(lottoTicketBox, rnd);
            return lottoTicketBox;
        }

        private static void InitNumbers(LottoTicketBox lottoTicketBox, Random rnd)
        {
            var possibleNumbers = new List<int>();
            for (var i = 0; i <= 49; i++) possibleNumbers.Add(i);
            for (var i = 1; i <= LottoTicketBox.NumbersPerLottoTicketBox; i++)
            {
                var rndNumber = possibleNumbers[rnd.Next(possibleNumbers.Count())];
                lottoTicketBox.Numbers.Add(rndNumber);
                possibleNumbers.Remove(rndNumber);
            }
        }
    }
}