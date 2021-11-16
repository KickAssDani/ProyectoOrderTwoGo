using ProyectoOrderTwoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoOrderTwoGo.Controllers
{
    public class EmpresasController : Controller
    {
        // GET: Empresas

        private readonly Orden2GoEntities _context;

        public EmpresasController()
        {
            _context = new Orden2GoEntities();
        }

        public ActionResult Index()
        {
            try
            {
                IEnumerable<Empresa> listaEmpresas = _context.Empresa.ToList();
                return View(listaEmpresas);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ocurrió un error en Empresas: " +ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Empresas()
        {
            return View();
        }
        public ActionResult Editar(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["Mensaje"] = "El id debe de existir";
                    return RedirectToAction("Index");
                }
                else
                {
                    Empresa _empresa = _context.Empresa.Find(id);
                    return View(_empresa);
                }
                
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ocurrió un error al editar la Empresa: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult Actualizar(Empresa _empresa)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Mensaje"] = "Debe de destar el campo rellenado.";
                    return RedirectToAction("Index");
                }
                else
                {
                    var result = _context.Empresa.FirstOrDefault();
                    if (result.nameEmpresa == _empresa.nameEmpresa)
                    {
                        TempData["Mensaje"] = "Ya hay una empresa con el mismo nombre, verifique.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Empresa emp = _context.Empresa.Find(_empresa.idEmpresa);
                        emp.nameEmpresa = _empresa.nameEmpresa;
                        _context.SaveChanges();
                        TempData["Mensaje"] = "Se hicieron los cambios correctamente";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Hubo un error:" + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult AgregarEmpresa(Empresa _empresa)
        {
            try
            {
                _context.Empresa.Add(_empresa);
                _context.SaveChanges();
                TempData["Mensaje"] = "Se agregó correctamente el dato.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Hubo un error:" + ex.Message;
                return View("Index");
            }
        }
    }
}