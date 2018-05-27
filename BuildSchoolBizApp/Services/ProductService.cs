using BizDataLibrary.Models;
using BizDataLibrary.Repositories;
using BuildSchoolBizApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchoolBizApp.Servies
{
    public class ProductService
    {

        public OperationResult Create(ProductViewModel input)
        {
            var result = new OperationResult();
            try
            {
                BizModel context = new BizModel();
                // context 橋梁 "承上啟下" 資料庫的上下文
                BizRepository<Product> repository 
                    = new BizRepository<Product>(context);
                // viewModel 轉成 Model 再給資料庫
                Product entity = new Product()
                { PartNo = input.PartNo, PartName = input.PartName };
                repository.Create(entity);
                context.SaveChanges();
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.exception = ex;
            }
            return result;
        }

        public ProductListViewModel GetProducts()
        {
            var result = new ProductListViewModel();
            result.Items = new List<ProductViewModel>();
            BizModel context = new BizModel();
            BizRepository<Product> repository = new BizRepository<Product>(context);
            foreach (var item in repository.GetAll().OrderBy((x) => x.PartNo))
            {
                var p = new ProductViewModel()
                {
                    PartNo = item.PartNo,
                    PartName = item.PartName
                };
                result.Items.Add(p);
            }
            return result;
        }
    }
}
