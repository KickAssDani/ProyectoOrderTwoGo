using ProyectoOrderTwoGo.Bussinees;
using ProyectoOrderTwoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoOrderTwoGo.Controllers
{
    public class ProductosController : Controller
    {
        private readonly Orden2GoEntities _context;
        public ProductosController()
        {
            _context = new Orden2GoEntities();
        }

        public ActionResult Index()
        {
            try
            {            
                int id = 0;
                if (Session["Empresa"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    id = int.Parse(Session["Empresa"].ToString());
                }  
                
                IEnumerable<Productos> ListaProductos = _context.Productos.Where(x => x.idEmpresa == id).ToList();

                return View(ListaProductos);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public ActionResult AgregarProductos()
        {
            ListaEmpresas _Empresas = new ListaEmpresas();
            List<Empresa> _Lista = _Empresas.Obtener();
            ViewBag.Empresa = new SelectList(_Lista, "idEmpresa", "nameEmpresa");
            return View();
        }

        [HttpPost]
        public ActionResult AgregarProductosAdmi(Productos _produtos)
        {
            try
            {
                _context.Productos.Add(_produtos);
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