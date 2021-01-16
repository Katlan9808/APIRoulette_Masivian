using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BetModel
    {
        public int iBetId { get; set; }
        public string strBetCode { get; set; }
        public string strBetKey { get; set; }
        public double lgBetValue { get; set; }
        public DateTime dttBetDate { get; set; }
        public int iFkRouletteId { get; set; }
        public int iFkClientId { get; set; }
        public string strBetColor { get; set; }
        public int iBetNumber { get; set; }
        public ResponseBetModel objResponseBetModel { get; set; } = new ResponseBetModel();
    }
}
