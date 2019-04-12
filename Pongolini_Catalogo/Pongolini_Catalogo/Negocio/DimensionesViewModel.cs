using System;
using System.Collections.Generic;
using System.Text;

namespace Pongolini_Catalogo.Negocio
{
    public class DimensionesViewModel
    {
        public string producto { get; set; }
        public string marca_modelo { get; set; }
        public string motor { get; set; }
        public string numero_300indy { get; set; }
        public double? diametro_exterior { get; set; }
        public double? diametro_interior { get; set; }
        public double? largo_alto { get; set; }
        public string angulo_material { get; set; }
    }
}
