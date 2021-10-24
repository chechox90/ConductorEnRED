using DLL.DAO.Operaciones;
using DLL.DAO.Operaciones.Interfaces;
using DLL.DAO.Seguridad;
using DLL.DAO.Seguridad.Interfaces;
using DLL.NEGOCIO.Operaciones;
using DLL.NEGOCIO.Operaciones.Interfaces;
using DLL.NEGOCIO.Seguridad;
using DLL.NEGOCIO.Seguridad.Interfaces;
using Microsoft.Practices.Unity;

namespace WebApplication1
{
    public class ContainerBootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            // Registrar todas las Interfaces y su correspondiente Clase
            container.RegisterType<I_N_Usuario, N_Usuario>();
            container.RegisterType<I_DAO_Usuario, DAO_Usuario>();

            container.RegisterType<I_N_HorarioConductor, N_HorarioConductor>();
            container.RegisterType<I_DAO_HorarioConductor, DAO_HorarioConductor>();

            container.RegisterType<I_N_Terminal, N_Terminal>();
            container.RegisterType<I_DAO_Terminal, DAO_Terminal>();
            
            // ================================================= Planillas no revisadas
        }
    }
}