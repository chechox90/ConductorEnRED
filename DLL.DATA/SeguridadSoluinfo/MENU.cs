//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DLL.DATA.SeguridadSoluinfo
{
    using System;
    using System.Collections.Generic;
    
    public partial class MENU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MENU()
        {
            this.ACCIONES = new HashSet<ACCIONES>();
        }
    
        public int ID_MENU { get; set; }
        public Nullable<int> ID_PADRE { get; set; }
        public int ORDEN { get; set; }
        public string NOMBRE { get; set; }
        public bool ESTADO { get; set; }
        public string CONTROLADOR { get; set; }
        public string ICONO_URL { get; set; }
        public string URL { get; set; }
        public Nullable<short> ID_PROYECTO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCIONES> ACCIONES { get; set; }
        public virtual PROYECTOS PROYECTOS { get; set; }
    }
}
