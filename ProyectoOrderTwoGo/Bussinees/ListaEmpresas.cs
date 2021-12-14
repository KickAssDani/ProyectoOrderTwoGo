using ProyectoOrderTwoGo.Datos;
using ProyectoOrderTwoGo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoOrderTwoGo.Bussinees
{
    public class ListaEmpresas
    {
        string _conexion = "";
        SqlConnection _con;
        public ListaEmpresas()
        {
            _conexion = "Data Source=SQL5104.site4now.net;Initial Catalog=db_a7da1c_order2go;User Id=db_a7da1c_order2go_admin;Password=jechaves2312!";
            _con = new SqlConnection(_conexion);
        }
        public List<Empresa> Obtener()
        {
            ObtenerListaEmpresas _op = new ObtenerListaEmpresas();
            DataTable _dT = _op.Obtener();
            List<Empresa> _ListaEmpresas = new List<Empresa>();

            foreach (DataRow r in _dT.Rows) {
                Empresa _emp = new Empresa(r);
                _ListaEmpresas.Add(_emp);
            }

            return _ListaEmpresas;
        }

        public List<Roles> ObtenerRoles()
        {
            ObtenerListaEmpresas _op = new ObtenerListaEmpresas();
            DataTable _dT = _op.ObtenerRoles();
            List<Roles> _ListaEmpresas = new List<Roles>();

            foreach (DataRow r in _dT.Rows)
            {
                Roles _emp = new Roles(r);
                _ListaEmpresas.Add(_emp);
            }

            return _ListaEmpresas;
        }
        public List<ProductosRepository> ObtenerInfoProductos() {

            ObtenerListaEmpresas _op = new ObtenerListaEmpresas();
            DataTable _dT = _op.ObtenerInfoProductos();
            List<ProductosRepository> _listaProductos = new List<ProductosRepository>();

            foreach (DataRow r in _dT.Rows)
            {
                ProductosRepository _repository = new ProductosRepository(r);
                _listaProductos.Add(_repository);
            }
            return _listaProductos;
        }
        public List<CarritoRepository> Carrito(int id)
        {

            ObtenerListaEmpresas _op = new ObtenerListaEmpresas();
            DataTable _dT = _op.Carrito(id);
            List<CarritoRepository> _listaProductos = new List<CarritoRepository>();

            foreach (DataRow r in _dT.Rows)
            {
                CarritoRepository _repository = new CarritoRepository(r);
                _listaProductos.Add(_repository);
            }
            return _listaProductos;
        }

        public void Registrar(int id) {

            ObtenerListaEmpresas _op = new ObtenerListaEmpresas();
            _op.Registro(id);

        }

        public DataTable RegistrarFactura(DataTable _dt) {


            return _dt;
        }
    }
}