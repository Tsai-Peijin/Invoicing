using BizDataLibrary.Models;
using BizDataLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizDataTest001
{
    public class ProductService
    {
        public void Create(Product entity)
        {
            BizModel context = new BizModel();
            BizRepository<Product> respository = new BizRepository<Product>(context);
            respository.Create(entity);
            context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            BizModel context = new BizModel();
            BizRepository<Product> repository = new BizRepository<Product>(context);
            var result = repository.GetAll().OrderBy((x) => x.PartNo).ToList();
            return result;
        }

        public Product GetByPartNo(string partNo)
        {
            BizModel context = new BizModel();
            BizRepository<Product> repository = new BizRepository<Product>(context);
            var result = repository.GetAll().FirstOrDefault((x) => x.PartNo == partNo);
            return result;
        }
    }

}
