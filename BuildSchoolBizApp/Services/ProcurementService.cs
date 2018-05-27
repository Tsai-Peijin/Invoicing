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
    public class ProcurementService
    {
        public OperationResult Create(ProcurementViewModel input)
        {
            var result = new OperationResult();
            try
            {
                BizModel context = new BizModel();
                var repository = new BizRepository<Procurement>(context);
                Procurement entity = new Procurement()
                {
                    PartNo = input.PartNo,
                    PurchasingDay = input.PurchasingDay,
                    Quantity = input.Quantity,
                    InvetoryQuantity = input.InvetoryQuantity,
                    UintPrice = input.UnitPrice
                };
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

       public int GetStock(string partNo)
        {
            BizModel context = new BizModel();
            var repository = new BizRepository<Procurement>(context);
            var items = repository.GetAll().Where((x) => x.PartNo == partNo);
            int result = 0;
            if (items.Count() > 0)
            {
                result = items.Sum((x) => x.InvetoryQuantity);
            }
            return result;
        }

        public ProcurementListQueryViewModel GetStockList()
        {
            var result = new ProcurementListQueryViewModel();
            BizModel context = new BizModel();
            var procurementRepo = new BizRepository<Procurement>(context);
            var productRep = new BizRepository<Product>(context);
            var temp = from p in productRep.GetAll()
                       join q in procurementRepo.GetAll()
                       on p.PartNo equals q.PartNo
                       select new
                       {
                           PartNo = p.PartNo,
                           PartName = p.PartName,
                           Quantity = q.InvetoryQuantity
                       };
            var group = from t in temp
                        group t by new { t.PartNo, t.PartName } into g
                        select new ProcurementQueryViewModel
                        {
                            PartName = g.Key.PartName,
                            PartNo = g.Key.PartNo,
                            TotalInvetoryQuantity = g.Sum((x) => x.Quantity)
                        };
            result.Items = group.ToList();
            return result;
        }
    }
}
