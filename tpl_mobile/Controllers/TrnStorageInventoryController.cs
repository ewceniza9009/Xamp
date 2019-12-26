using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace tpl_mobile.Controllers
{
    [RoutePrefix("api")]
    public class TrnStorageInventoryController : Controller
    {
        private Data.tplDataContext tpl;

        [Route("storageinventory/json/inventorysummary/{searchItem}")]
        public JsonResult GetInventoryByItemId(string searchItem)
        {
            tpl = new Data.tplDataContext();
            var inventory = new List<Models.InventorySummaryModel>();

            if (searchItem == "=")
            {
                inventory = tpl.TrnStorageInventories
                .Join(tpl.mstMaterials, i => i.MaterialId,
                    material => material.Id,
                    (i, material) =>
                         new
                         {
                             i = i,
                             material = material
                         }
               )
               .GroupBy(
                  temp0 =>
                     new
                     {
                         MaterialId = temp0.i.MaterialId,
                         MaterialName = temp0.material.MaterialName
                     },
                  temp0 =>
                     new
                     {
                         i = temp0.i,
                         material = temp0.material
                     }
               )
               .Select(
                  g =>
                     new Models.InventorySummaryModel()
                     {
                         MaterialId = g.Key.MaterialId,
                         Material = g.Key.MaterialName,
                         Quantity = g.Sum(x => (x.i.InQuantity - x.i.OutQuantity)),
                         Weight = g.Sum(x => x.i.Weight)
                     }
               ).ToList();
            }
            else
            {
                inventory = tpl.TrnStorageInventories
                .Join(tpl.mstMaterials, i => i.MaterialId,
                    material => material.Id,
                    (i, material) =>
                         new
                         {
                             i = i,
                             material = material
                         }
               )
               .GroupBy(
                  temp0 =>
                     new
                     {
                         MaterialId = temp0.i.MaterialId,
                         MaterialName = temp0.material.MaterialName
                     },
                  temp0 =>
                     new
                     {
                         i = temp0.i,
                         material = temp0.material
                     }
               )
               .Select(
                  g =>
                     new Models.InventorySummaryModel()
                     {
                         MaterialId = g.Key.MaterialId,
                         Material = g.Key.MaterialName,
                         Quantity = g.Sum(x => (x.i.InQuantity - x.i.OutQuantity)),
                         Weight = g.Sum(x => x.i.Weight)
                     }
               )
               .Where(a => a.Material.Contains(searchItem))
               .OrderBy(x => x.Material)
               .ToList();
            }

            return Json(inventory, JsonRequestBehavior.AllowGet);
        }

        // GET: TrnStorageInventory
        public ActionResult Index()
        {
            return View();
        }

        // GET: TrnStorageInventory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrnStorageInventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrnStorageInventory/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TrnStorageInventory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrnStorageInventory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TrnStorageInventory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrnStorageInventory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}