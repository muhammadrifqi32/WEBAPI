using API.Services.Interface;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ItemsController : ApiController
    {
        private IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/Item

        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            var data = _itemService.Get();
            if (!data.Count().Equals(0))
            {
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
            //return new string[] { "value1", "value2" };
        }

        // GET: api/Item/5
        public HttpResponseMessage Get(int id)
        {
            var get = _itemService.Get(id);
            if (get != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, get);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
            //return "value";
        }

        // POST: api/Item
        public HttpResponseMessage Post(ItemVM itemVM)
        {
            var post = _itemService.Create(itemVM);
            if (post >= 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, post);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        // PUT: api/Item/5
        public HttpResponseMessage Put(int id, ItemVM itemVM)
        {
            var put = _itemService.Update(id, itemVM);
            if (put <= 0)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        // DELETE: api/Item/5
        public HttpResponseMessage Delete(int id)
        {
            var delete = _itemService.Delete(id);
            if (delete >= 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, delete);
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
