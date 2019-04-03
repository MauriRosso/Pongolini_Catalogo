using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Asientos
    {
        public int codigo { get; set; }
        public string marca_modelo { get; set; }
        public string motor { get; set; }
        public string numero_original { get; set; }
        public string numero_300indy { get; set; }
        public string admision_escape { get; set; }
        public double? diametro_exterior { get; set; }
        public double? diametro_interior { get; set; }
        public double? largo { get; set; }
        public string angulo { get; set; }
        public string codigo_riosulense { get; set; }
        public string codigo_mahle { get; set; }
    }
}
