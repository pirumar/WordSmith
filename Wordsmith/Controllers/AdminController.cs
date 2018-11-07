using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Wordsmith.Models;

namespace Wordsmith.Controllers
{
    public class AdminController : Controller
    {
        metodlar klas = new metodlar();
        // GET: Admin
        public ActionResult Index()
        {
            return View();

        }
        public List<tCategorys> mainList = new List<tCategorys>();
        public ActionResult Newcategory()
        {
            mainList = klas.ConvertToList<tCategorys>(klas.GetDataTable("select IDCategory,CategoryName,IDParentCategory from [dbo].[tCategorys]"));


            foreach (tCategorys cat in mainList)
            {
                tCategorys.Add(cat);

                Altkategori(cat.IDCategory);

            }
            var selectList = new SelectList(tCategorys.Distinct(), "IDCategory", "CategoryName");
             
            ViewBag.data = selectList;
            return View(selectList);
        }
        [HttpPost]
        public JsonResult addCategory(tCategorys tcategorys)
        {
            if (tcategorys.CategoryName != null)
            {
                if (tcategorys.IDParentCategory > 0)
                { 
                    var sonuc = klas.cmd($"insert into tCategorys(CategoryName,IDParentCategory) values('{tcategorys.CategoryName}','{tcategorys.IDParentCategory}')");
                    var data = klas.GetDataRow($"select top 1 * from tCategorys where CategoryName='{tcategorys.CategoryName}' and IDParentCategory='{tcategorys.IDParentCategory}' order by IDCategory desc");
                    tCategorys newCat = new tCategorys();
                    newCat.CategoryName = data["CategoryName"].ToString();
                    newCat.IDCategory = (int)data["IDCategory"];
                    if (sonuc > 0)
                    {
                        return Json(newCat, JsonRequestBehavior.DenyGet);

                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.DenyGet);

                    }
                }
                else
                {

                    var sonuc = klas.cmd($"insert into tCategorys(CategoryName) values('{tcategorys.CategoryName}')");
                    var data = klas.GetDataRow($"select top 1 * from tCategorys where CategoryName='{tcategorys.CategoryName}' and IDParentCategory='{tcategorys.IDParentCategory}' order by IDCategory desc");
                    tCategorys newCat = new tCategorys();
                    newCat.CategoryName = data["CategoryName"].ToString();
                    newCat.IDCategory = (int)data["IDCategory"];
                    if (sonuc > 0)
                    {

                        return Json(newCat, JsonRequestBehavior.DenyGet);

                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.DenyGet);

                    }
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.DenyGet);


            }
        }

        List<tCategorys> tCategorys = new List<tCategorys>();

        private void Altkategori(int id)
        {
            var say = (from i in mainList
                       where i.IDParentCategory == id
                       select i).Count();
            if (say > 0)
            {
                var altKat = from i in mainList
                             where i.IDParentCategory == id
                             select i;
                foreach (tCategorys altkategori in altKat)
                {
                    altkategori.CategoryName = Server.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + altkategori.CategoryName);
                    tCategorys.Add(altkategori);
                    Altkategori(altkategori.IDCategory);
                }
            }


        }
    }
}