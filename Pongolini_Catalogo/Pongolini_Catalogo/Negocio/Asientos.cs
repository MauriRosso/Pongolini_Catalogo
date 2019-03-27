using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Asientos
    {
        public string codigo { get; set; }
        public string marca_modelo { get; set; }
        public string motor { get; set; }
        public string tipo_aplicacion { get; set; }
        public string tipo_alimentacion { get; set; }
        public string cilindros { get; set; }
        public string numero_original { get; set; }
        public string numero_300indy { get; set; }
        public string admision_escape { get; set; }
        public decimal? diametro_exterior { get; set; }
        public decimal? diametro_interior { get; set; } //En guias aparece la forma de la guia (A,B..) y en asientos el angulo (45°, 50°..)
        public decimal? largo { get; set; }
        public string material { get; set; }
        public string angulo { get; set; }
        public string codigo_trw { get; set; }
        public string codigo_metelli { get; set; } //G, A, NG, NA
        public string codigo_riosulense { get; set; }
        public string codigo_sm { get; set; }
        public string sobremedida { get; set; }
        public string imagen { get; set; }
    }
}
