using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RouletteModel
    {
        public int iRouletteId { get; set; }
        public string strRouletteCode { get; set; }
        public string strRouletteStatus { get; set; }
        public DateTime dttOpeningDate { get; set; }
        public DateTime dttClosingDate { get; set; }
    }
}
