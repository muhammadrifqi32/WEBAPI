using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using Data.ViewModel;

namespace Data.Repository.Interface
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> Get();
        Supplier Get(int Id);
        int Create(SupplierVM supplierVM);
        int Update(int Id, SupplierVM supplierVM);
        int Delete(int Id);

        //hard delete = menghapus data dari db
        //soft delete = boolean.
    }
}
