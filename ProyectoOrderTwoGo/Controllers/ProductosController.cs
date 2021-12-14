using ProyectoOrderTwoGo.Bussinees;
using ProyectoOrderTwoGo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoOrderTwoGo.Controllers
{
    public class ProductosController : Controller
    {
        private readonly db_a7da1c_order2goEntities _context;
        public ProductosController()
        {
            _context = new db_a7da1c_order2goEntities();
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
            try
            {
                ListaEmpresas _Empresas = new ListaEmpresas();
                List<Empresa> _Lista = _Empresas.Obtener();
                ViewBag.Empresa = new SelectList(_Lista, "idEmpresa", "nameEmpresa");
                return View();
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ha ocurrido un error mostrando los productos, por favor verificar: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        
        public ActionResult EditarProducto(int? id)
        {
            try
            {
                Productos productos = _context.Productos.Find(id);

                ListaEmpresas _Empresas = new ListaEmpresas();
                List<Empresa> _Lista = _Empresas.Obtener();
                ViewBag.Empresa = new SelectList(_Lista, "idEmpresa", "nameEmpresa");

                return View(productos);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ha ocurrido un error editar el producto, por favor verificar: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Productos _productos)
        {

            try
            {
                Productos producto = _context.Productos.Find(_productos.idProduct);
                producto.ProductNam = _productos.ProductNam;
                producto.idEmpresa = _productos.idEmpresa;
                producto.precio = _productos.precio;
                _context.SaveChanges();
                TempData["Mensaje"] = "Se hicieron los cambios correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Ha ocurrido un error editar el producto, por favor verificar: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult AgregarProductosAdmi(Productos _produtos)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(_produtos.Imagen.FileName);
                string extensio = Path.GetExtension(_produtos.Imagen.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extensio;
                _produtos.ImagenProducto = "~/images/"+filename;
                filename = Path.Combine(Server.MapPath("~/images/"), filename);
                _produtos.Imagen.SaveAs(filename);
                
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