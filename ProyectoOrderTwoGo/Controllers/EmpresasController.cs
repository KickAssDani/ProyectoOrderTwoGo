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
            IEnumerable<Empresa> listaEmpresas = _context.Empresa.ToList();

            return View(listaEmpresas);
        }
        public ActionResult Empresas()
        {
            return View();
        }
        public ActionResult Editar(int? id)
        {
            if(id == null)
            {
                TempData["Mensaje"] = "El id debe de existir";
                RedirectToAction("Index");
            }
            Empresa _empresa = _context.Empresa.Find(id);

            return View(_empresa);
        }
        public ActionResult Actualizar(Empresa _empresa)
        {
            if (!ModelState.IsValid)
            {
                TempData["Mensaje"] = "Debe de destar el campo rellenado."
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