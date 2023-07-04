using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mesaj_portali.ViewModel
{
    public class MesajModel
    {

        public int mesajId { get; set; }
        public int gonderenId { get; set; }
        public int aliciId { get; set; }
        public string mesajText { get; set; }
        public System.DateTime mesajTarih { get; set; }
        public int mesajSohbetId { get; set; }
    }
}