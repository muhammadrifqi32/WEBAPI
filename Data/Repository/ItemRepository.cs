using Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using Data.ViewModel;
using Data.Context;

namespace Data.Repository
{
    public class ItemRepository : IItemRepository
    {
        MyContext myContext = new MyContext();
        public int Create(ItemVM itemVM)
        {
            var item = myContext.Items.Where(i => i.Name.ToLower() == itemVM.Name.ToLower() || i.Price == itemVM.Price || i.Stock == itemVM.Stock);
            var result = 0;
            if (item != null)
            {
                var push = new Item(itemVM);
                push.Supplier = myContext.Suppliers.Where(s => s.Id == itemVM.Supplier).FirstOrDefault();
                myContext.Items.Add(push);
                return myContext.SaveChanges();
            }
            return result;
            //throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            var delete = myContext.Items.Find(Id);
            if (delete != null)
            {
                delete.IsDelete = true;
                delete.DeleteDate = DateTimeOffset.Now;
            }
            return myContext.SaveChanges();
            //throw new NotImplementedException();
        }

        public IEnumerable<Item> Get()
        {
            //throw new NotImplementedException();
            var sup = myContext.Items.Include("Supplier").Where(i => i.IsDelete == false);
            return sup;
        }

        public Item Get(int Id)
        {
            //throw new NotImplementedException();
            return myContext.Items.Find(Id);
        }

        public int Update(int Id, ItemVM itemVM)
        {
            var update = myContext.Items.Find(Id);
            update.Update(itemVM);
            return myContext.SaveChanges();
            //throw new NotImplementedException();
        }
    }
}
