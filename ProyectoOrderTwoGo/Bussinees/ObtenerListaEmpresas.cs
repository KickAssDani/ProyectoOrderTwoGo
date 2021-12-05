using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProyectoOrderTwoGo.Bussinees
{
    public class ObtenerListaEmpresas
    {

        string _conexion = "";
        SqlConnection _con;

        public ObtenerListaEmpresas()
        {
            _conexion = "server=EDUARDOCHAVES\\SQLEXPRESS;initial catalog=Orden2Go;integrated security=True;";
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
                "Productos.precio, Empresa.idEmpresa, Empresa.nameEmpresa as Empresa " +
                "from Productos inner join Empresa on Productos.idEmpresa = Empresa.idEmpresa";
            SqlCommand _command = new SqlCommand(_sentencia, _con);
            _command.CommandType = CommandType.Text;
            SqlDataAdapter _ap = new SqlDataAdapter(_command);
            DataTable _dT = new DataTable();
            _ap.Fill(_dT);
            return _dT;
        }
        public DataTable obtenerInfo(int id)
        {
            string _sentencia = "select ";
            SqlCommand _command = new SqlCommand(_sentencia, _con);
            _command.CommandType = CommandType.Text;
            SqlDataAdapter _ap = new SqlDataAdapter(_command);
            DataTable _dT = new DataTable();
            _ap.Fill(_dT);
            return _dT;
        }

    }
}