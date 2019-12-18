using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetApp.ViewModels
{
    public class ItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public int Supplier_Id { get; set; }
    }
}