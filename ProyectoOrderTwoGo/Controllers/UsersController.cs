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
        private readonly db_a7da1c_order2goEntities _context;



        public UsersController()
        {
            _context = new db_a7da1c_order2goEntities();
        }

        public ActionResult Agregar()
        {
            try
            {
                ListaEmpresas _Empresas = new ListaEmpresas();
                List<Empresa> _Lista = _Empresas.Obtener();
                ViewBag.Empresa = new SelectList(_Lista, "idEmpresa", "nameEmpresa");

                ListaEmpresas _Roles = new ListaEmpresas();
                List<Roles> _Rols = _Roles.ObtenerRoles();
                ViewBag.Roles = new SelectList(_Rols, "idRol", "nameRol");

                return View();
            }
            catch (Exception ex)
            {

                TempData["Mensaje"] = "En el modulo de usuarios ocurrió algún problema: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Index()
        {
            try
            {
               
                IEnumerable<Usuarios> _listsEmpleados = _context.Usuarios.ToList();

                return View(_listsEmpleados);
            }
            catch (Exception ex)
            {

                TempData["Mensaje"] = "En el modulo de usuarios ocurrió algún problema: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Editar(int? id)
        {
            try
            {
                Usuarios user = _context.Usuarios.Find(id);

                ListaEmpresas _Empresas = new ListaEmpresas();
                List<Empresa> _Lista = _Empresas.Obtener();
                ViewBag.Empresa = new SelectList(_Lista, "idEmpresa", "nameEmpresa", user.idEmpresa);

                ListaEmpresas _Roles = new ListaEmpresas();
                List<Roles> _Rols = _Roles.ObtenerRoles();
                ViewBag.Roles = new SelectList(_Rols, "idRol", "nameRol", user.idRol);

                return View(user);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Hubieron errores en el ingreso de los datos: " + ex.Message;
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public ActionResult Actualizar(Usuarios _usuarios)
        {
            try
            {
                Usuarios _users = _context.Usuarios.Find(_usuarios.idUsuario);

                _users.NombreUsuario = _usuarios.NombreUsuario;
                _users.usuario = _usuarios.usuario;
                _users.idEmpresa = _usuarios.idEmpresa;
                _users.idRol = _usuarios.idRol;
                _users.clave = _usuarios.clave;
                _context.SaveChanges();
                TempData["Mensaje"] = "Se hicieron los cambios correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Hubieron errores en la actualización: " + ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult AgregarPost(Usuarios _usuarios)
        {
            try
            {
                using (db_a7da1c_order2goEntities db = new db_a7da1c_order2goEntities())
                {

                    var UserDatails = db.Usuarios.Where(x => x.usuario == _usuarios.usuario).FirstOrDefault();
                    if (UserDatails == null)
                    {
                        _context.Usuarios.Add(_usuarios);
                        _context.SaveChanges();
                        TempData["Mensaje"] = "Se agregó correctamente el dato.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Mensaje"] = "Ya hay un usuario agregado, intente con otro distinto.";
                        return RedirectToAction("Agregar");
                    }

                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Hubo un error:" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Registrarse()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuarios _usuarios)
        {
            try
            {
                using (db_a7da1c_order2goEntities db = new db_a7da1c_order2goEntities())
                {

                    var UserDatails = db.Usuarios.Where(x => x.usuario == _usuarios.usuario).FirstOrDefault();
                    if (UserDatails == null)
                    {
                        Usuarios _users = new Usuarios();

                        _users.idRol = 3;
                        _users.idEmpresa = 3;
                        _users.NombreUsuario = _usuarios.NombreUsuario;
                        _users.usuario = _usuarios.usuario;
                        _users.clave = _usuarios.clave;

                        _context.Usuarios.Add(_users);
                        _context.SaveChanges();
                        TempData["Mensaje"] = "Se agregó correctamente el dato.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Mensaje"] = "Ya hay un usuario agregado, intente con otro distinto.";
                        return RedirectToAction("Agregar");
                    }

                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Hubo un error:" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Authorice(ProyectoOrderTwoGo.Models.Usuarios users)
        {
            try
            {
                using (db_a7da1c_order2goEntities db = new db_a7da1c_order2goEntities())
                {
                    var UserDatails = db.Usuarios.Where(x => x.usuario == users.usuario && x.clave == users.clave).FirstOrDefault();
                    if (UserDatails == null)
                    {
                        ViewBag.Error = "Hubo un error a la hora de intentar validar el usuario.";
                        return RedirectToAction("Login", "Users");
                    }
                    else
                    {
                        Session["id"] = UserDatails.idUsuario;
                        Session["user"] = UserDatails.NombreUsuario;
                        Session["Rol"] = UserDatails.idRol;
                        Session["Empresa"] = UserDatails.idEmpresa;
                        TempData["mensaje"] = "Bienvenido " + Session["user"];
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["mensaje"] = "Hubieron algunos errores a la hora de realizar la autorización, por favor corregir estos errores: " +ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Users");
        }

    }
}