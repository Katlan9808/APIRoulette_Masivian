using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ResponseBetModel
    {
        public int iIdRoulette { get; set; }
        public long lgTotalBet { get; set; }
        public int iWinningNumber { get; set; }
        public string strWinningColor { get; set; }
        public int iWinningClient { get; set; }
    }
}
