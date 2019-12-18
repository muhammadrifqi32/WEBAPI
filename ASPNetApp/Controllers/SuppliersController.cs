//using ASPNetApp.ViewModels;
using ASPNetApp.Base;
//using ASPNetApp.Models;
using Data.Model;
using Data.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPNetApp.Controllers
{
    public class SuppliersController : Controller
    {
        Port getPort = new Base.Port();
        readonly HttpClient client = new HttpClient();
        // GET: Suppliers
        public ActionResult Index()
        {
            //var supplier = myContext.tb_m_supplier.ToList();
            //return View(supplier);
            return View(List());
        }

        public async Task<JsonResult> List()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.client)
            };
            HttpResponseMessage response = await client.GetAsync("Suppliers");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<Supplier[]>();
                var json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return Json("Internal Server Error", JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertOrUpdate(SupplierVM supplierVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.client)
            };
            var myContent = JsonConvert.SerializeObject(supplierVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (supplierVM.Id == 0)
            {
                var result = client.PostAsync("Suppliers", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Suppliers/" + supplierVM.Id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult Update(SupplierVM supplierVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.client)
            };
            var myContent = JsonConvert.SerializeObject(supplierVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (string.IsNullOrWhiteSpace(supplierVM.Id.ToString()))
            {
                var result = client.PutAsync("Suppliers/" + supplierVM.Id, byteContent).Result;
                return Json(result);
            }
            else
            {

                var result = client.PostAsync("Suppliers", byteContent).Result;
                return Json(result);
            }
        }

        public async Task<JsonResult> GetById(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.client)
            };
            HttpResponseMessage response = await client.GetAsync("Suppliers");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<Supplier[]>();
                var supplier = data.FirstOrDefault(s => s.Id == id);
                var json = JsonConvert.SerializeObject(supplier, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            return Json("Internal Server Error", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.client)
            };
            var result = client.DeleteAsync("Suppliers/" + id).Result;
            return Json(result);
        }

        public JsonResult LoadSupplier()
        {
            IEnumerable<Supplier> supplier = null;

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.client)
            };
            var responseTask = client.GetAsync("Suppliers");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Supplier>>();
                readTask.Wait();
                supplier = readTask.Result;
            }
            else
            {
                supplier = Enumerable.Empty<Supplier>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(supplier, JsonRequestBehavior.AllowGet);
        }

        //// GET: Supplier/Details/5
        //public ActionResult Details(int id)
        //{
        //    var detailssupp = myContext.tb_m_supplier.Where(d => d.Id == id).FirstOrDefault();
        //    return View(detailssupp);
        //}

        //// GET: Supplier/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}


        //// POST: Supplier/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        tb_m_supplier supplier = new tb_m_supplier();
        //        supplier.Name = collection["Name"];
        //        supplier.Email = collection["Email"];
        //        myContext.tb_m_supplier.Add(supplier);
        //        myContext.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Supplier/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var editsupp = myContext.tb_m_supplier.Where(s => s.Id == id).FirstOrDefault();
        //    return View(editsupp);
        //}

        //// POST: Supplier/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        var editsupp = myContext.tb_m_supplier.Where(s => s.Id == id).FirstOrDefault();
        //        editsupp.Name = collection["Name"];
        //        editsupp.Email = collection["Email"];
        //        myContext.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////GET: Supplier/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var deletesupp = myContext.tb_m_supplier.Where(s => s.Id == id).FirstOrDefault();
        //    return View(deletesupp);
        //}

        //// POST: Supplier/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here
        //        var deletesupp = myContext.tb_m_supplier.Where(s => s.Id == id).FirstOrDefault();
        //        myContext.tb_m_supplier.Remove(deletesupp);
        //        myContext.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}        
    }
}
