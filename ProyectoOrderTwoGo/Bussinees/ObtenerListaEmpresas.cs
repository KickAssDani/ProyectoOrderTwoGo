using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ProyectoOrderTwoGo.Datos;

namespace ProyectoOrderTwoGo.Bussinees
{
    public class ObtenerListaEmpresas
    {

        string _conexion = "";
        SqlConnection _con;

        public ObtenerListaEmpresas()
        {
            _conexion = "Data Source=SQL5104.site4now.net;Initial Catalog=db_a7da1c_order2go;User Id=db_a7da1c_order2go_admin;Password=jechaves2312!";
            _con = new SqlConnection(_conexion);
        }

        public DataTable Obtener()
        {
            string _sentencia = "Select * from Empresa";
            SqlCommand _command = new SqlCommand(_sentencia, _con);
            _command.CommandType = CommandType.Text;
            SqlDataAdapter _ap = new SqlDataAdapter(_command);
            DataTable _dT = new DataTable();
            _ap.Fill(_dT);
            return _dT;
        }
        public DataTable ObtenerRoles()
        {
            string _sentencia = "Select * from Roles";
            SqlCommand _command = new SqlCommand(_sentencia, _con);
            _command.CommandType = CommandType.Text;
            SqlDataAdapter _ap = new SqlDataAdapter(_command);
            DataTable _dT = new DataTable();
            _ap.Fill(_dT);
            return _dT;
        }
        public DataTable ObtenerInfoProductos()
        {
            string _sentencia = "select Productos.idProduct as codigo, Productos.ProductNam as NombreProducto, " +
                "Productos.precio, Empresa.idEmpresa, Empresa.nameEmpresa as Empresa, Productos.stock " +
                "from Productos inner join Empresa on Productos.idEmpresa = Empresa.idEmpresa";
            SqlCommand _command = new SqlCommand(_sentencia, _con);
            _command.CommandType = CommandType.Text;
            SqlDataAdapter _ap = new SqlDataAdapter(_command);
            DataTable _dT = new DataTable();
            _ap.Fill(_dT);
            return _dT;
        }
        public DataTable Carrito(int id)
        {
            string _sentencia = "select Carrito.idProducto,Productos.ProductNam,Carrito.idEmpresa, " +
                "Empresa.nameEmpresa, Carrito.cantidad, Carrito.precio, Productos.stock from Carrito " +
                "inner join Productos on Carrito.idProducto = Productos.idProduct " +
                "inner join Empresa on Carrito.idEmpresa = Empresa.idEmpresa where Carrito.idUsuario = @idUsuario";
            SqlCommand _command = new SqlCommand(_sentencia, _con);
            _command.CommandType = CommandType.Text;
            _command.Parameters.AddWithValue("@idUsuario", id);
            SqlDataAdapter _ap = new SqlDataAdapter(_command);
            DataTable _dT = new DataTable();
            _ap.Fill(_dT);
            return _dT;
        }

        public DataTable Registro(int id) {

            try
            {
                int total = 0;
                string _sentencia = "select Carrito.idProducto,Productos.ProductNam,Carrito.idEmpresa, Empresa.nameEmpresa, " +
                    "Carrito.cantidad, Carrito.precio, Productos.stock from Carrito inner join Productos on Carrito.idProducto = Productos.idProduct " +
                    "inner join Empresa on Carrito.idEmpresa = Empresa.idEmpresa where Carrito.idUsuario = @idUsuario";
                SqlCommand _command = new SqlCommand(_sentencia, _con);
                _command.CommandType = CommandType.Text;
                _command.Parameters.AddWithValue("@idUsuario", id);
                SqlDataAdapter _ap = new SqlDataAdapter(_command);
                DataTable _dT = new DataTable();
                _ap.Fill(_dT);
                if (int.Parse(_dT.Rows[0]["cantidad"].ToString()) <= int.Parse(_dT.Rows[0]["stock"].ToString()))
                {

                    for (int i = 0; i < _dT.Rows.Count; i++)
                    {
                        total = total + (int.Parse(_dT.Rows[i]["precio"].ToString()) * int.Parse(_dT.Rows[i]["cantidad"].ToString()));
                    }

                    RegistrarFactura(_dT, total, id);
                    return _dT;
                }
                else
                {
                    return _dT;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }       
            
        }

        public void RegistrarFactura(DataTable _dt, int total, int id) {

            try
            {
                SqlCommand _command = new SqlCommand("RegistroFactura", _con);
                _command.CommandType = CommandType.StoredProcedure;
                _command.Parameters.AddWithValue("@opcion", 1);
                _command.Parameters.AddWithValue("@total", total);
                _command.Parameters.AddWithValue("@idUsuario", id);
                _con.Open();
                _command.ExecuteNonQuery();
                _con.Close();

                RegistrarProductos(_dt, id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        public void RegistrarProductos(DataTable _dt, int id) {

            try
            {
                int update = int.Parse(_dt.Rows[0]["stock"].ToString()) - int.Parse(_dt.Rows[0]["cantidad"].ToString());
                SqlCommand _command = new SqlCommand();
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    _command = new SqlCommand("RegistroFactura", _con);
                    _command.CommandType = CommandType.StoredProcedure;
                    _con.Open();
                    _command.Parameters.AddWithValue("@opcion", 2);
                    _command.Parameters.AddWithValue("@idProducto", _dt.Rows[i]["idProducto"]);
                    _command.Parameters.AddWithValue("@idEmpresa", _dt.Rows[i]["idEmpresa"]);
                    _command.Parameters.AddWithValue("@cantidad", _dt.Rows[i]["Cantidad"]);
                    _command.Parameters.AddWithValue("@precio", _dt.Rows[i]["Precio"]);
                    _command.Parameters.AddWithValue("@update", update);
                    _command.ExecuteNonQuery();
                    _con.Close();
                }

                _command = new SqlCommand("RegistroFactura", _con);
                _command.CommandType = CommandType.StoredProcedure;
                _con.Open();
                _command.Parameters.AddWithValue("@opcion", 3);
                _command.Parameters.AddWithValue("@idUsuario", id);
                _command.ExecuteNonQuery();
                _con.Close();


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }
      

    }
}