﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetApp.ViewModels
{
    public class SupplierVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTimeOffset CreateDate { get; set; }
    }
}