using Data.Repository;
using Data.Model;
using Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data.ViewModel;
using API.Services;

namespace API.Controllers
{
    public class SuppliersController : ApiController
    {
        //ISupplierRepository supplier = new SupplierRepository();
        //ISupplierService supplierservice = new SupplierService();

        private ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET: api/Suppliers

        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            var data = _supplierService.Get();
            if (!data.Count().Equals(0))
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        // GET: api/Suppliers/5
        public HttpResponseMessage Get(int id)
        {
            var get = _supplierService.Get(id);
            if (get != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, get);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        // POST: api/Suppliers
        public HttpResponseMessage Post(SupplierVM supplierVM)
        {
           var post = _supplierService.Create(supplierVM);
            if (post >= 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, post);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // PUT: api/Suppliers/5
        public HttpResponseMessage Put(int id, SupplierVM supplierVM)
        {
            var put = _supplierService.Update(id, supplierVM);
            if (put <= 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        // DELETE: api/Suppliers/5
        public HttpResponseMessage Delete(int id)
        {
            var delete =_supplierService.Delete(id);
            if (delete >= 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, delete);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
