using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VanesaMoraga_EVA2.Models;

namespace VanesaMoraga_EVA2.Views.Estadistica
{
    public class EstadisticaController : Controller
    {
        private FabricaDB db = new FabricaDB();

        // GET: Estadistica
        public ActionResult Index()
        {
            //lis_stock deberia guardar las cantidades de productos por categoría en una lista
            //lis_categoría guarda los nombres de las categorías en una lista
            //lis_prom es la lista que guarda el precio promedio por categoría
            List<int> lis_stock = new List<int>(){};
            List<double> lis_prom = new List<double>() { };
            List<String> lis_categoria = new List<String>() { };
            //recorro las categorías
            foreach (var cat in db.Categorias)
            {
                int stock = 0;
                int tipos = 0; //la variable "tipos" es el divisor para el promedio de precios
                int precio = 0; //la variable "precio" guarda el valor de un tipo de artículo
                double prom = 0; // la variable "prom" guarda el promedio por categoría en una lista
                //recorro los productos
                foreach (var prod in db.Productoes)
                {
                    //si la id de la categoría de este ciclo es igual a la categoría del ciclo de productos que está anidado,
                    //suma en la variable "stock" (que inicia de nuevo cada vez que se cambia de categoría)
                    //la cantidad de productos declarados en la columna "stock" de la tabla productos
                    if(prod.id_categoria == cat.id_categoria) {
                        stock = stock + prod.stock.Value;
                        //esto es para el calculo de promedio de precio por categoría
                        tipos = tipos + 1;
                        precio = precio + ((byte)prod.precio);
                    }
                }
                //Opero el promedio y lo guardo en prom solo si tipos es distinto de cero, para evitar la error de agregar
                //una categoría que no contenga productos, por lo que se dividiría por cero.
                //(si "tipos == 0" no se opera y conserva ese valor para el promedio mostrado en la vista)
                if (tipos != 0) {
                    prom = (precio / tipos);
                }
                //agrego a la lista de categorias, promedio, y stock los resultados
                lis_prom.Add(prom);
                lis_categoria.Add(cat.nombre.ToString());
                lis_stock.Add(stock);
            }
            //paso los resultados en viewbags
            ViewBag.lis_prom = lis_prom;
            ViewBag.lis_stock = lis_stock;
            ViewBag.lis_categoria= lis_categoria;
            
            return View();

            }
    };
}