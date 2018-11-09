using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wordsmith.Models;

namespace Wordsmith.Controllers
{
    public class HomeController : Controller
    {
        public static metodlar klas = new metodlar();
        public static Postlist postlist = new Postlist();
        public static List<tCategorys> tCategorysList = new List<tCategorys>();
        public static AdminController AdminControllers = new AdminController();
        public List<tPosts> allPosts()
        {
            List<tPosts> ass = new List<tPosts>();
            foreach (tPosts item in postlist.tPosts())
            {
                List<tCategorys> newCat = new List<tCategorys>();
                foreach (tCategorys cat in tCategorysList.Where(x => x.IDCategory == item.IDCategory))
                {
                    newCat.Add(cat);
                }
                foreach (tUsers user in klas.ConvertToList<tUsers>(klas.GetDataTable("select * from [dbo].[tUsers] Where IDUser=" + item.IDUser)))
                {
                    item.tUsers = user;
                }
                item.TCategorys = newCat;
                ass.Add(item);
            }
            return ass;

        }
        //public ActionResult Index()
        //{
        //    AdminController.mainList = klas.ConvertToList<tCategorys>(klas.GetDataTable("select IDCategory,CategoryName,IDParentCategory from [dbo].[tCategorys]"));
        //    AdminControllers.GetCategorys();
        //    ViewBag.Category = AdminController.mainList;

        //    return View(klas.getList());
        //}

        public ActionResult Index(int? SayfaNo)
        {
            AdminController.mainList = klas.ConvertToList<tCategorys>(klas.GetDataTable("select IDCategory,CategoryName,IDParentCategory from [dbo].[tCategorys]"));
            AdminControllers.GetCategorys();
            tCategorysList = AdminController.mainList;
            int _sayfaNo = SayfaNo ?? 1;

            var MusteriListesi = allPosts().OrderByDescending(m => m.IDPost).ToPagedList<tPosts>(_sayfaNo, 10);
            return View(MusteriListesi);

        }
        public ActionResult PartialIndex(int? SayfaNo)
        {
            AdminController.mainList = klas.ConvertToList<tCategorys>(klas.GetDataTable("select IDCategory,CategoryName,IDParentCategory from [dbo].[tCategorys]"));
            AdminControllers.GetCategorys();
            tCategorysList = AdminController.mainList;
            int _sayfaNo = SayfaNo ?? 1;

            var MusteriListesi = allPosts().OrderByDescending(m => m.IDPost).ToPagedList<tPosts>(_sayfaNo, 10);
            return PartialView("_PostListCategory", MusteriListesi);


        }
        public ActionResult Detail()
        {
            return View();

        }
        public ActionResult PartialCategory()
        {
            return PartialView();
        }

        public ActionResult PartialPopularPost()
        {
            return PartialView();
        }
    }
}