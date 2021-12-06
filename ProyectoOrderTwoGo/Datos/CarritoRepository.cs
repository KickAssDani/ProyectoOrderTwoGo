using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProyectoOrderTwoGo.Datos
{
    public class CarritoRepository
    {
        public string nameEmpresa { get; set; }
        public int idEmpresa { get; set; }
        public string NombreProducto { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public int precio { get; set; }
        public virtual int total {get; set;}
        public virtual int stock { get; set; }
        public CarritoRepository(DataRow r)
        {
            idProducto = Convert.ToInt32(r["idProducto"]);
            NombreProducto = Convert.ToString(r["ProductNam"]);
            precio = Convert.ToInt32(r["precio"]);
            idEmpresa = Convert.ToInt32(r["idEmpresa"]);
            nameEmpresa = Convert.ToString(r["nameEmpresa"]);
            cantidad = Convert.ToInt32(r["cantidad"]);
            total = Convert.ToInt32(int.Parse(r["precio"].ToString()) * int.Parse(r["cantidad"].ToString()));
            stock = Convert.ToInt32(r["stock"]);
            
        }
    }
}