using ProyectoOrderTwoGo.Bussinees;
using ProyectoOrderTwoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoOrderTwoGo.Controllers
{
    public class UsersController : Controller
    {
        private readonly Orden2GoEntities _context;

        public UsersController()
        {
            _context = new Orden2GoEntities();
        }

        public ActionResult Agregar()
        {
            ListaEmpresas _Empresas = new ListaEmpresas();
            List<Empresa> _Lista = _Empresas.Obtener();
            ViewBag.Empresa = new SelectList(_Lista, "idEmpresa", "nameEmpresa");

            ListaEmpresas _Roles = new ListaEmpresas();
            List<Roles> _Rols = _Roles.ObtenerRoles();
            ViewBag.Roles = new SelectList(_Rols, "idRol", "nameRol");

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarPost(Usuarios _usuarios) {
            try
            {
                using (Orden2GoEntities db = new Orden2GoEntities())
                {
                    
                    var UserDatails = db.Usuarios.Where(x => x.usuario == _usuarios.usuario).FirstOrDefault();
                    if(UserDatails.usuario == _usuarios.usuario)
                    {
                        TempData["Mensaje"] = "El usuario ingresado ya forma parte de un registro en la base.";
                        return RedirectToAction("Agregar");
                    }
                    else
                    {
                        _context.Usuarios.Add(_usuarios);
                        _context.SaveChanges();
                        TempData["Mensaje"] = "Se agregó correctamente el dato.";
                        return RedirectToAction("Agregar");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Hubo un error:" + ex.Message;
                return RedirectToAction("Agregar");
            }
        }

        [HttpPost]
        public ActionResult Authorice(ProyectoOrderTwoGo.Models.Usuarios users)
        {
            using (Orden2GoEntities db = new Orden2GoEntities())
            {
                var UserDatails = db.Usuarios.Where(x => x.usuario == users.usuario && x.clave == users.clave).FirstOrDefault();
                if (UserDatails == null)
                {

                    ViewBag.Error = "Hubo un error a la hora de intentar validar el usuario.";
                    return View("Index", users);
                }
                else
                {
                    Session["user"] = UserDatails.NombreUsuario;
                    Session["Rol"] = UserDatails.idRol;
                    Session["Empresa"] = UserDatails.idEmpresa;
                    TempData["mensaje"] = "Bienvenido " + Session["user"];
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Users");
        }

    }
}