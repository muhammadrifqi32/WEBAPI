using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Context;
using Data.Model;
using Data.ViewModel;
using Data.Repository.Interface;

namespace Data.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        MyContext myContext = new MyContext();
        public int Create(SupplierVM supplierVM)
        {
            {
                var supplier = myContext.Suppliers.Where(s => s.Name.ToLower() == supplierVM.Name.ToLower() || s.Email.ToLower() == supplierVM.Email.ToLower());
                var result = 0;
                if (supplier != null)
                {
                var push = new Supplier(supplierVM);
                myContext.Suppliers.Add(push);
                return myContext.SaveChanges();
                }
                return result;
            }
            //throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            var delete = myContext.Suppliers.Find(Id);
            if(delete != null)
            {
                delete.IsDelete = true;
                delete.DeleteDate = DateTimeOffset.Now;
            }
            return myContext.SaveChanges();
        }
            //throw new NotImplementedException();

        public IEnumerable<Supplier> Get()
        {
            //throw new NotImplementedException();
            return myContext.Suppliers.Where(s => s.IsDelete ==  false).ToList();
        }

        public Supplier Get(int Id)
        {
            //throw new NotImplementedException();
            return myContext.Suppliers.Find(Id);
        }

        public int Update(int Id, SupplierVM supplierVM)
        {
            var update = myContext.Suppliers.Find(Id);
            update.Update(supplierVM);
            return myContext.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
