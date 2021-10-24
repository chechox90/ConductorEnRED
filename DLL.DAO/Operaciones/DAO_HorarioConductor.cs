using DLL.DAO.Operaciones.Interfaces;
using DLL.DATA.SeguridadSoluinfo;
using DLL.DTO.CargaHorario;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DAO.Operaciones
{
    public class DAO_HorarioConductor: I_DAO_HorarioConductor
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DAO_HorarioConductor()
        {
            XmlConfigurator.Configure();
        }

        public string SetGuardarHorarioConductor(List<DTO_CargarHorarioConductor> list, string nombreCarga, DateTime fechaCarga, string descripcion)
        {
            try
            {
                string respuesta = "";
                int res = 0;
                using (SolusegEntities context = new SolusegEntities())
                {
                    using (var contextTransaction = context.Database.BeginTransaction())
                    {
                        CARGA_HORARIO newItemCarga = new CARGA_HORARIO()
                        {
                            ID_USUARIO_CARGA = 1,
                            NOMBRE_CARGA = nombreCarga,
                            COMENTARIO_CARGA = descripcion,
                            FECHA_CARGA = fechaCarga,
                            ESTADO = true
                        };

                        context.CARGA_HORARIO.Add(newItemCarga);
                        var res2 = context.SaveChanges();
                                            
                        int maxIdCarga = context.CARGA_HORARIO.Max(x => x.ID_CARGA_HORARIO);

                        foreach (var item in list)
                        {
                            HORARIO_CONDUCTOR newItemHorario = new HORARIO_CONDUCTOR()
                            {
                                ID_CARGA_HORARIO = maxIdCarga,
                                ID_USUARIO = 1,
                                ID_TERMINAL_INICIO = item.TERMINAL_INICIO,
                                FECHA_INICIO = item.FECHA_HORA_INICIO,
                                HORARIO_CUBIERTO = false,
                                ID_BUS_INICIO = item.BUS_INICIO,
                                ESTADO = true
                            };
                            context.HORARIO_CONDUCTOR.Add(newItemHorario);
                        }

                        res = context.SaveChanges();
                        contextTransaction.Commit();
                    }
                    int filasTotales = list.Count;


                    if (res == filasTotales)
                    {
                        respuesta = "Se ha guardado con éxito los horarios";
                        return respuesta;
                    }
                    else
                    {
                        respuesta = "Ha ocurrido un error al guardar";
                        return respuesta;
                    }
                }
                    
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                return "Tenemos una incidencia al guardar los datos, intentelo nuevamente. !El equipo de Conductor En Red ya está al tanto!";
                throw;
            }

            
        }
    }
}
