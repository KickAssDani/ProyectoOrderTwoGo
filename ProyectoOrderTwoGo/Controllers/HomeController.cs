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
using System.Data;

namespace ProyectoOrderTwoGo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private readonly db_a7da1c_order2goEntities _context;

        public HomeController()
        {
            _context = new db_a7da1c_order2goEntities();
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

                if (Session["id"] != null)
                {
                    List<CarritoRepository> _carrito = _productos.Carrito(int.Parse(Session["id"].ToString()));
                    ViewBag.Carrito = _carrito;
                }
                
              
                productos = ViewBag.Productos;

                return View(productos);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ha ocurrido un error editar el producto, por favor verificar: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        
        public ActionResult ActualizarCarrito(int id) {
            
            if (Session["id"] != null)
            {
                var results = from p in _context.Carrito where p.idProducto == id select p;
                foreach (var p in results)
                {
                    p.cantidad = p.cantidad + 1;
                }
                _context.SaveChanges();
                return RedirectToAction("Productos");
            }
            else {
                TempData["Mensaje"] = "Debe de registrarse para poder ver sus artículos en el carrito.";
                return RedirectToAction("Productos");
            }
        
        }

       
        public ActionResult Carrito(int id)
        {
            try
            {
                if (Session["id"] != null)
                {
                    var results = from p in _context.Carrito where p.idProducto == id select p;
                    Productos productos = _context.Productos.Find(id);
                    Carrito _carrito = new Carrito();


                    if (results.Count() > 0)
                    {

                        foreach (var p in results)
                        {
                            p.cantidad = p.cantidad + 1;
                        }
                        _context.SaveChanges();
                        return View();
                    }
                    else
                    {
                        _carrito.idProducto = productos.idProduct;
                        _carrito.idEmpresa = productos.idEmpresa;
                        _carrito.precio = productos.precio;
                        _carrito.cantidad = 1;
                        _carrito.idUsuario = int.Parse(Session["id"].ToString());

                        _context.Carrito.Add(_carrito);
                        _context.SaveChanges();
                        TempData["Mensaje"] = "Se agregó el producto al carrito correctamente.";
                        return RedirectToAction("OrderTwoGo");
                    }
                }
                else {
                    TempData["Mensaje"] = "Para poder hacer el registro del producto en el carrito debe de loguearse.";
                    return RedirectToAction("OrderTwoGo");
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Hubo un error mientras se registraba el producto " + ex.Message;
                return RedirectToAction("OrderTwoGo");
            }
        }

     
        public ActionResult Productos() {

            if (Session["id"] != null)
            {
                ListaEmpresas _productos = new ListaEmpresas();
                List<CarritoRepository> _carrito = _productos.Carrito(int.Parse(Session["id"].ToString()));
                ViewBag.Carrito = _carrito;
                return View();

            }
            else {
                return RedirectToAction("OrderTwoGo", "Home");
            }

            
        }
        
        public ActionResult RegistrarCompra() {

            ListaEmpresas _productos = new ListaEmpresas();
            _productos.Registrar(int.Parse(Session["id"].ToString()));
            

            return RedirectToAction("OrderTwoGo");
        }

     
    }
}