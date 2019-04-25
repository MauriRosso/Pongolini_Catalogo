using System;
using System.Collections.Generic;
using System.Text;

namespace Pongolini_Catalogo.Negocio
{
    public class BujesViewModel
    {
        public string codigo { get; set; }
        public double? diametro_exterior { get; set; }
        public double? diametro_interior { get; set; }
        public double? largo { get; set; }
        public string aplicacion { get; set; }
        public string img_diam_ext { get; set; }
        public string img_diam_int { get; set; }
        public string img_largo { get; set; }
    }
}
