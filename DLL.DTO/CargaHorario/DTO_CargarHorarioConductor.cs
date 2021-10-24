using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DTO.CargaHorario
{
  public  class DTO_CargarHorarioConductor
    {
        public int TERMINAL_INICIO { get; set; }
        public int BUS_INICIO { get; set; }
        public string NOMBRE_CARGA { get; set; }
        public string DESCRIPCION_CARGA { get; set; }
        public DateTime FECHA_HORA_INICIO { get; set; }
    }
}
