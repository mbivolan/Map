using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    class CheckPoint
    {
        public string NumeProprietar;
        public string Tarla;
        public string Parcela;
        public string Suprafata;
        public string StatusDosar;
        public string LinkDocument;
        public List<string> AdditionalDocument;

        public int coord_x;
        public int coord_y;

        public CheckPoint()
        {
            this.AdditionalDocument = new List<string>();
        }
    }
}
