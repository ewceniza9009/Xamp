using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tpl_mobile.Controllers
{
    [RoutePrefix("api")]
    public class TrnWithdrawalOrderController : Controller
    {
        private Data.tplDataContext tpl;

        public ActionResult Index()
        {
            return View();
        }

        [Route("withdrawalorders/json/withdrawalorder")]
        public JsonResult GetWithdrawalOrders()
        {
            var wOrders = new List<Models.WithdrawalOrder>();

            using (tpl = new Data.tplDataContext())
            {
                wOrders = (from trnWithdrawalOrderMaterials in tpl.trnWithdrawalOrderMaterials
                           where
                             trnWithdrawalOrderMaterials.trnWithdrawalOrder.IsApproved == 0
                           group new { trnWithdrawalOrderMaterials.trnWithdrawalOrder, trnWithdrawalOrderMaterials.trnWithdrawalOrder.sysUser1, trnWithdrawalOrderMaterials } by new
                           {
                               trnWithdrawalOrderMaterials.trnWithdrawalOrder.Id,
                               trnWithdrawalOrderMaterials.trnWithdrawalOrder.MstWarehouse.WarehouseCode,
                               WithdrawalOrderDate = trnWithdrawalOrderMaterials.trnWithdrawalOrder.WithdrawalOrderDate,
                               trnWithdrawalOrderMaterials.trnWithdrawalOrder.WithdrawalOrderNumber,
                               trnWithdrawalOrderMaterials.trnWithdrawalOrder.IsApproved
                           } into g
                           select new Models.WithdrawalOrder
                           {
                               Id = g.Key.Id,
                               WarehouseCode = g.Key.WarehouseCode,
                               WithdrawalOrderDate = (DateTime)g.Key.WithdrawalOrderDate,
                               WithdrawalOrderNumber = g.Key.WithdrawalOrderNumber,
                               NetWeight = (decimal)g.Sum(p => p.trnWithdrawalOrderMaterials.Weight),
                               IsApproved = g.Key.IsApproved == null ? 0 : g.Key.IsApproved
                           }).ToList();
            }

            return Json(wOrders, JsonRequestBehavior.AllowGet);
        }

        [Route("withdrawalorders/json/withdrawalordera")]
        public JsonResult GetWithdrawalOrdersA()
        {
            var wOrders = new List<Models.WithdrawalOrder>();

            using (tpl = new Data.tplDataContext())
            {
                wOrders = (from trnWithdrawalOrderMaterials in tpl.trnWithdrawalOrderMaterials
                           where
                             trnWithdrawalOrderMaterials.trnWithdrawalOrder.IsApproved == -1
                           group new { trnWithdrawalOrderMaterials.trnWithdrawalOrder, trnWithdrawalOrderMaterials.trnWithdrawalOrder.sysUser1, trnWithdrawalOrderMaterials } by new
                           {
                               trnWithdrawalOrderMaterials.trnWithdrawalOrder.Id,
                               trnWithdrawalOrderMaterials.trnWithdrawalOrder.MstWarehouse.WarehouseCode,
                               WithdrawalOrderDate = trnWithdrawalOrderMaterials.trnWithdrawalOrder.WithdrawalOrderDate,
                               trnWithdrawalOrderMaterials.trnWithdrawalOrder.WithdrawalOrderNumber,
                               trnWithdrawalOrderMaterials.trnWithdrawalOrder.IsApproved
                           } into g
                           select new Models.WithdrawalOrder
                           {
                               Id = g.Key.Id,
                               WarehouseCode = g.Key.WarehouseCode,
                               WithdrawalOrderDate = (DateTime)g.Key.WithdrawalOrderDate,
                               WithdrawalOrderNumber = g.Key.WithdrawalOrderNumber,
                               NetWeight = (decimal)g.Sum(p => p.trnWithdrawalOrderMaterials.Weight),
                               IsApproved = g.Key.IsApproved == null ? 0 : g.Key.IsApproved
                           }).ToList();
            }

            return Json(wOrders, JsonRequestBehavior.AllowGet);
        }

        [Route("withdrawalorders/json/withdrawaldetail/{Id}")]
        public JsonResult GetWithdrawalOrderMaterials(int Id)
        {
            var wOrderMaterialsq2 = new Models.WithdrawalOrderDetail();

            using (tpl = new Data.tplDataContext()) {
                var wOrderMaterialsq1 = from trnWithdrawalOrderMaterials in tpl.trnWithdrawalOrderMaterials
                                        where
                                          trnWithdrawalOrderMaterials.trnWithdrawalOrder.Id == Id
                                        select new
                                        {
                                            trnWithdrawalOrderMaterials.trnWithdrawalOrder.Id,
                                            trnWithdrawalOrderMaterials.trnWithdrawalOrder.WithdrawalOrderNumber,
                                            WithdrawalOrderDate = trnWithdrawalOrderMaterials.trnWithdrawalOrder.WithdrawalOrderDate,
                                            trnWithdrawalOrderMaterials.trnWithdrawalOrder.IsApproved,
                                            trnWithdrawalOrderMaterials.mstMaterial.MaterialName,
                                            trnWithdrawalOrderMaterials.mstUnit.UnitName,
                                            trnWithdrawalOrderMaterials.Quantity,
                                            trnWithdrawalOrderMaterials.Weight,
                                            trnWithdrawalOrderMaterials.Barcode
                                        };

                wOrderMaterialsq2 = new Models.WithdrawalOrderDetail()
                {
                    Id = wOrderMaterialsq1.FirstOrDefault().Id,
                    WithdrawalOrderNumber = wOrderMaterialsq1.FirstOrDefault().WithdrawalOrderNumber,
                    WithdrawalOrderDate = (DateTime)wOrderMaterialsq1.FirstOrDefault().WithdrawalOrderDate,
                    IsApproved = wOrderMaterialsq1.FirstOrDefault().IsApproved
                };

                var wom = new List<Models.WithdrawalOrderMaterial>();

                foreach (var x in wOrderMaterialsq1) {
                    wom.Add(new Models.WithdrawalOrderMaterial()
                    {
                        MaterialName = x.MaterialName,
                        UnitName = x.UnitName,
                        Quantity = (decimal)x.Quantity,
                        Weight = (decimal)x.Weight,
                        Batch = x.Barcode.Substring(x.Barcode.Length - 7, 4)
                    });
                }

                wOrderMaterialsq2.WithdrawalOrderMaterial = (from i in wom
                                                            group new { i } by new {i.MaterialName, i.UnitName, i.Batch } into g
                                                            select new Models.WithdrawalOrderMaterial()
                                                            {
                                                                MaterialName = g.Key.MaterialName,
                                                                UnitName = g.Key.UnitName,
                                                                Quantity = g.Sum(x => x.i.Quantity),
                                                                Weight = g.Sum(x => x.i.Weight),
                                                                Batch = g.Key.Batch
                                                            }).ToList();
            }

            return Json(wOrderMaterialsq2, JsonRequestBehavior.AllowGet);
        }
       
        [Route("withdrawalorders/approvewithdrawalorder/{Id}")]
        public JsonResult ApproveWithdrawalOrder(int Id)
        {
            using (tpl = new Data.tplDataContext())
            {
                var worders = tpl.trnWithdrawalOrders.Where(x => x.Id == Id).SingleOrDefault();

                bool isTrue = worders.IsApproved == 0 ? false : true;

                if (isTrue)
                {
                    worders.IsApproved = 0;
                }
                else
                {
                    worders.IsApproved = -1;
                }

                tpl.SubmitChanges();
                
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}