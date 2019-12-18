using ASPNetApp.Base;
//using ASPNetApp.ViewModels;
using Data.Context;
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
    public class ItemsController : Controller
    {
        Port getPort = new Base.Port();
        readonly HttpClient client = new HttpClient();
        // GET: Items
        public ActionResult Index()
        {
            //var item = myContext.tb_m_item.ToList();
            //ViewBag.Supplier = myContext.tb_m_supplier.ToList();
            return View(List());
        }

        public async Task<JsonResult> List()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.client)
            };
            HttpResponseMessage response = await client.GetAsync("Items");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<IList<Item>>();
                //var json = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings()
                //{
                //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                //});
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json("Internal Server Error", JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertOrUpdate(ItemVM itemVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.client)
            };
            var myContent = JsonConvert.SerializeObject(itemVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (itemVM.Id == 0)
            {
                var result = client.PostAsync("Items", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Items/" + itemVM.Id, byteContent).Result;
                return Json(result);
            }
        }

        public async Task<JsonResult> GetById(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.client)
            };
            HttpResponseMessage response = await client.GetAsync("Items");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<Item[]>();
                var item = data.FirstOrDefault(i => i.Id == id);
                var json = JsonConvert.SerializeObject(item, Formatting.None, new JsonSerializerSettings()
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
            var result = client.DeleteAsync("Items/" + id).Result;
            return Json(result);
        }

        //    // GET: Items/Details/5
        //    public ActionResult Details(int id)
        //    {
        //        var detailsitem = myContext.tb_m_item.Find(id);
        //        return View(detailsitem);
        //    }

        //    // GET: Items/Create
        //    public ActionResult Create()
        //    {
        //        IEnumerable<SelectListItem> items = myContext.tb_m_supplier.Select(c => new SelectListItem
        //        {
        //            Value = c.Id.ToString(),
        //            Text = c.Name,
        //            //Selected = c.Id == edititem.tb_m_supplier.Id
        //        }).ToArray();
        //        ViewBag.Supplier_Id = items;
        //        return View();
        //    }

        //    // POST: Items/Create
        //    [HttpPost]
        //    public ActionResult Create(ItemVM ItemVM)
        //    {
        //        try
        //        {
        //            // TODO: Add insert logic here
        //            tb_m_item item = new tb_m_item();
        //            item.Name = ItemVM.Name;
        //            item.Stock = ItemVM.Stock;
        //            item.Price = ItemVM.Price;
        //            item.Supplier_Id = ItemVM.Supplier_Id;
        //            myContext.tb_m_item.Add(item);
        //            myContext.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: Items/Edit/5
        //    public ActionResult Edit(int id)
        //    {
        //        var edititem = myContext.tb_m_item.Find(id);
        //        ItemVM itemVM = new ItemVM();
        //        itemVM.Name = edititem.Name;
        //        itemVM.Price = edititem.Price;
        //        itemVM.Stock = edititem.Stock;
        //        IEnumerable<SelectListItem> items = myContext.tb_m_supplier.Select(c => new SelectListItem
        //        {
        //            Value = c.Id.ToString(),
        //            Text = c.Name,
        //            //Selected = c.Id == edititem.tb_m_supplier.Id
        //        }).ToArray();
        //        foreach (var item in items)
        //        {
        //            if (edititem.tb_m_supplier.Name == item.Text)
        //            {
        //                item.Selected = true;
        //            }
        //            else
        //            {
        //                item.Selected = false;
        //            }
        //        }
        //        ViewBag.Supplier_Id = items;
        //        return View(itemVM);
        //    }

        //    // POST: Items/Edit/5
        //    [HttpPost]
        //    public ActionResult Edit(int id, ItemVM ItemVM)
        //    {
        //        try
        //        {
        //            // TODO: Add update logic here
        //            var edititems = myContext.tb_m_item.Find(id);
        //            edititems.Name = ItemVM.Name;
        //            edititems.Price = ItemVM.Price;
        //            edititems.Stock = ItemVM.Stock;
        //            edititems.Supplier_Id = ItemVM.Supplier_Id;
        //            myContext.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: Supplier/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        var deleteitem = myContext.tb_m_item.Find(id);
        //        return View(deleteitem);
        //    }

        //    // POST: Supplier/Delete/5
        //    [HttpPost]
        //    public ActionResult Delete(int id, FormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add delete logic here
        //            var deleteitem = myContext.tb_m_item.Find(id);
        //            myContext.tb_m_item.Remove(deleteitem);
        //            myContext.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //}
    }
}