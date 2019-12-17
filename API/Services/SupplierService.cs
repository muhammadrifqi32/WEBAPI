using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data.Model;
using Data.ViewModel;
using Data.Repository.Interface;
using Data.Repository;

namespace API.Services
{
    public class SupplierService : ISupplierService
    {
        //ISupplierRepository _supplierRepository = new SupplierRepository();
        private ISupplierRepository _supplierRepository;

        //public SupplierService() { }
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public int Create(SupplierVM supplierVM)
        {
            if (string.IsNullOrWhiteSpace(supplierVM.Name) || string.IsNullOrWhiteSpace(supplierVM.Email))
            {
                return 0;
            }
            else
            {
                return _supplierRepository.Create(supplierVM);
            }
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            if (string.IsNullOrWhiteSpace(Id.ToString()))
            {
                return 0;
            }
            else
            {
            return _supplierRepository.Delete(Id);
            }
            //throw new NotImplementedException();
        }

        public IEnumerable<Supplier> Get()
        {
            return _supplierRepository.Get();
            //throw new NotImplementedException();
        }

        public Supplier Get(int Id)
        {
            return _supplierRepository.Get(Id);
            //throw new NotImplementedException();
        }

        public int Update(int Id, SupplierVM supplierVM)
        {
            if (string.IsNullOrWhiteSpace(supplierVM.Name) || string.IsNullOrWhiteSpace(supplierVM.Email))
            {
                return 0;
            }
            else
            {
                return _supplierRepository.Update(Id, supplierVM);
            }
            //throw new NotImplementedException();
        }
    }
}