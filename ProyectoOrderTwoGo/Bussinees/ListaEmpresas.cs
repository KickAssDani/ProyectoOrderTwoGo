﻿using ProyectoOrderTwoGo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProyectoOrderTwoGo.Bussinees
{
    public class ListaEmpresas
    {
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
    }
}