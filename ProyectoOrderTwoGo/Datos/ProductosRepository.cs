using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProyectoOrderTwoGo.Datos
{
    public class ProductosRepository
    {
        public int idProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Precio { get; set; }
        public int idEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string ImagenProducto { get; set; }

        public ProductosRepository(DataRow r)
        {
            idProducto = Convert.ToInt32(r["codigo"]);
            NombreProducto = Convert.ToString(r["NombreProducto"]);
            Precio = Convert.ToInt32(r["precio"]);
            idEmpresa = Convert.ToInt32(r["idEmpresa"]);
            NombreEmpresa = Convert.ToString(r["Empresa"]);
        }
    }
}