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
                //Método para traer solo productos de la empresa en la que se encuntra el usuario.
                if (Session["Empresa"] == null)
                {
                    return RedirectToAction("Login", "Users");
                }
                int id = int.Parse(Session["Empresa"].ToString());
                IEnumerable < Productos > ListaProductos = _context.Productos.Where(x => x.idEmpresa == id).ToList();
                return View(ListaProductos);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ha ocurrido un error mostrando los productos, por favor verificar: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AgregarProductos()
        {
            ListaEmpresas _Empresas = new ListaEmpresas();
            List<Empresa> _Lista = _Empresas.Obtener();
            ViewBag.Empresa = new SelectList(_Lista, "idEmpresa", "nameEmpresa");
            return View();
        }

        
        public ActionResult EditarProducto(int? id)
        {
            Productos productos = _context.Productos.Find(id);

            ListaEmpresas _Empresas = new ListaEmpresas();
            List<Empresa> _Lista = _Empresas.Obtener();
            ViewBag.Empresa = new SelectList(_Lista, "idEmpresa", "nameEmpresa");

            return View(productos);
        }

        [HttpPost]
        public ActionResult Edit(Productos _productos)
        {
            using (Orden2GoEntities db = new Orden2GoEntities())
            {
                Productos producto = db.Productos.Find(_productos.idProduct);

                producto.ProductNam = _productos.ProductNam;
                producto.idEmpresa = _productos.idEmpresa;
                producto.precio = _productos.precio;              
                _context.SaveChanges();
                TempData["Mensaje"] = "Se hicieron los cambios correctamente";
                return RedirectToAction("Index");
            }
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