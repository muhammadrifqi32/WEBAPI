using Data.Model;
using Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> Get();
        Supplier Get(int Id);
        int Create(SupplierVM supplierVM);
        int Update(int Id, SupplierVM supplierVM);
        int Delete(int Id);
    }
}
