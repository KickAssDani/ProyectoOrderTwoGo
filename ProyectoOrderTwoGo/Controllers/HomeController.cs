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

        public ActionResult Carrito(int id)
        {
            try
            {
                Productos productos = _context.Productos.Find(id);

                Carrito _carrito = new Carrito();

                _carrito.idProducto = productos.idProduct;
                _carrito.idEmpresa = productos.idEmpresa;
                _carrito.precio = productos.precio;
                _carrito.cantidad = 1;
                _carrito.idUsuario = int.Parse(Session["id"].ToString());

                _context.Carrito.Add(_carrito);
                _context.SaveChanges();
                TempData["Mensaje"] = "Se hicieron los cambios correctamente";
                return RedirectToAction("OrderTwoGo");
            }
            catch (Exception ex)
            {

                TempData["Mensaje"] = "Hubo un error mientras se registraba el producto " + ex.Message;
                return RedirectToAction("OrderTwoGo");
            }
        }

     
        public ActionResult Productos() {

            ListaEmpresas _productos = new ListaEmpresas();
            List<CarritoRepository> _carrito = _productos.Carrito(int.Parse(Session["id"].ToString()));

            ViewBag.Carrito = _carrito;
            return View();
        }
        [HttpPost]
        public ActionResult RegistrarCompra() {

            ListaEmpresas _productos = new ListaEmpresas();
            List<CarritoRepository> _carrito = _productos.Carrito(int.Parse(Session["id"].ToString()));

            return View();
        }

     
    }
}