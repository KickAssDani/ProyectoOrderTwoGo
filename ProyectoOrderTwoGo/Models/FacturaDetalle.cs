//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoOrderTwoGo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FacturaDetalle
    {
        public int id { get; set; }
        public Nullable<int> idFactura { get; set; }
        public Nullable<int> idProducto { get; set; }
        public Nullable<int> idEmpresa { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<int> precio { get; set; }
    
        public virtual Factura Factura { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Productos Productos { get; set; }
    }
}
