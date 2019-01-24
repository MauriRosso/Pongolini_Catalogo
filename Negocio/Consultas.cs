using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class Consultas
    {
        Pongolini_DBEntities db = new Pongolini_DBEntities();

        public List<Guias_Asientos> ObtenerGuiasAsientos()
        {
            var GuiasAsientos = db.Guias_Asientos;
            List<Guias_Asientos> resultado = new List<Guias_Asientos>();
            foreach (var item in GuiasAsientos)
            {
                resultado.Add(new Guias_Asientos()
                {
                    Id = item.Id,
                    Marca_Modelo = item.Marca_Modelo,
                    Aplicacion_Motor = item.Aplicacion_Motor,
                    NumOriginal = item.NumOriginal,
                    Num300Indy = item.Num300Indy,
                    Adm_Esc = item.Adm_Esc,
                    Diam_Ext = item.Diam_Ext,
                    Diam_Int = item.Diam_Int,
                    Largo = item.Largo,
                    Material = item.Material,
                    Forma = item.Forma,
                    Inter_METELLI = item.Inter_METELLI,
                    Inter_RIOSULENSE = item.Inter_RIOSULENSE,
                    Inter_TRW = item.Inter_TRW,
                    Inter_SM = item.Inter_SM,
                    Producto = item.Producto,                 
                });
            }
            return resultado;
        }
    }
}
