using ProyectoOrderTwoGo.Bussinees;
using ProyectoOrderTwoGo.Datos;
using ProyectoOrderTwoGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Web.UI;

namespace ProyectoOrderTwoGo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private readonly Orden2GoEntities _context;

        public HomeController()
        {
            _context = new Orden2GoEntities();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderTwoGo(int? page)
        {
             try
            {
                int pageSize = 24;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                IPagedList<ProductosRepository> productos = null;

                ListaEmpresas _productos = new ListaEmpresas();
                List<ProductosRepository> _repository = _productos.ObtenerInfoProductos();
                ViewBag.Productos = _repository.ToList().ToPagedList(pageIndex, pageSize);

                productos = ViewBag.Productos;

                return View(productos);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ha ocurrido un error editar el producto, por favor verificar: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}