using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wordsmith.Models;

namespace Wordsmith.Controllers
{
    public class AdminController : Controller
    {
        metodlar klas = new metodlar();
        public ActionResult Index()
        {
            return View();

        }
        public static List<tCategorys> mainList = new List<tCategorys>();
        public ActionResult Newcategory()
        {
            mainList = klas.ConvertToList<tCategorys>(klas.GetDataTable("select IDCategory,CategoryName,IDParentCategory from [dbo].[tCategorys]"));
            GetCategorys();
            mainList = null;
            mainList = new List<tCategorys>();
            foreach (var item in tCategorys)
            {
                item.CategoryName = Server.HtmlDecode(item.CategoryName);
                
                mainList.Add(item); 
            }
            var selectList = new SelectList(tCategorys.Distinct(), "IDCategory", "CategoryName");

            ViewBag.data = selectList;
            return View(selectList);
        }
        public void GetCategorys()
        {
            tCategorys = null;
            tCategorys = new List<tCategorys>();
            mainList = klas.ConvertToList<tCategorys>(klas.GetDataTable("select IDCategory,CategoryName,IDParentCategory from [dbo].[tCategorys]"));
            foreach (tCategorys cat in mainList)
            {
                tCategorys.Add(cat);

                Altkategori(cat.IDCategory);

            }
        }
        [HttpPost]
        public JsonResult addCategory(tCategorys tcategorys)
        {
            if (tcategorys.CategoryName != null)
            {
                if (tcategorys.IDCategory > 0)
                {
                    var sonuc = klas.cmd($"insert into tCategorys(CategoryName,IDParentCategory) values('{tcategorys.CategoryName}','{tcategorys.IDCategory}')");
                    var data = klas.GetDataRow($"select top 1 * from tCategorys where CategoryName='{tcategorys.CategoryName}' and IDParentCategory='{tcategorys.IDCategory}' order by IDCategory desc");
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
                    
                    altkategori.CategoryName = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + altkategori.CategoryName;
                    tCategorys.Add(altkategori);
                    Altkategori(altkategori.IDCategory);
                }
            }


        }
        public JsonResult GetCategory()
        {
            GetCategorys();
            return Json(tCategorys.Distinct().ToList(), JsonRequestBehavior.AllowGet);

        }
        public ActionResult _PartialCategory()
        {
            mainList = klas.ConvertToList<tCategorys>(klas.GetDataTable("select IDCategory,CategoryName,IDParentCategory from [dbo].[tCategorys]"));
            GetCategorys();
            mainList = null;
            mainList = new List<tCategorys>();
            foreach (var item in tCategorys)
            {
                item.CategoryName = Server.HtmlDecode(item.CategoryName);

                mainList.Add(item);
            }
            var selectList = new SelectList(tCategorys.Distinct(), "IDCategory", "CategoryName");
 
            return PartialView(selectList);
        }
        public ActionResult newPost()
        {
            mainList = klas.ConvertToList<tCategorys>(klas.GetDataTable("select IDCategory,CategoryName,IDParentCategory from [dbo].[tCategorys]"));
            GetCategorys();
            mainList = null;
            mainList = new List<tCategorys>();
            foreach (var item in tCategorys)
            {
                item.CategoryName = Server.HtmlDecode(item.CategoryName);

                mainList.Add(item);
            }
            var selectList = new SelectList(tCategorys.Distinct(), "IDCategory", "CategoryName");

            ViewBag.data = selectList;
            return View(selectList);
        }
        [HttpPost]
        public JsonResult addPost(tPosts tposts)
        {

            if (tposts.PostText != "" && tposts.PostText != "")
            {
                if (tposts.IDCategory > 0)
                {
                    int sonuc;
                    //[dbo].[tPosts] IDPost, PostTitle, PostText, PostImage, IDUser, IDCategory
                    if (tposts.Image != null)
                    {
                        Upload upload = new Upload();
                        upload = UploadImage(tposts.Image);

                        if (upload.Status)
                        {
                            tposts.PostImage = "/images/" + tposts.Image.FileName;
                            sonuc = klas.cmd($"insert  into tPosts(PostTitle,PostText,PostImage,IDUser,IDCategory) " +
                                            $"values('{tposts.PostTitle}','{tposts.PostText}','{"/images/" + upload.Name}','2','{tposts.IDCategory}')");


                        }
                        else
                        {
                            sonuc = 0;
                        }

                    }
                    else
                    {
                        sonuc = klas.cmd($"insert into tPosts(PostTitle,PostText,IDUser,IDCategory) " +
                                                                  $"values('{tposts.PostTitle}','{tposts.PostText}','2','{tposts.IDCategory}')");

                    }
                    var data = klas.GetDataRow($"select top 1 * from tPosts where PostTitle='{tposts.PostTitle}' and PostText='{tposts.PostText}' order by IDPost desc");
                    if (data != null)
                    {

                        tPosts newCat = new tPosts();
                        newCat.PostTitle = data["PostTitle"].ToString();
                        newCat.IDPost = (int)data["IDPost"];
                        if (tposts.Tags != "")
                        {

                            addTag(newCat.IDPost, tposts.Tags.Split(','));
                        }

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
                        return Json(false, JsonRequestBehavior.DenyGet);

                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);

                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.DenyGet);


            }
        }
        public Upload UploadImage(HttpPostedFileBase file)
        {
            Upload upload = new Upload();
            try
            {
                string extension = Path.GetExtension(file.FileName);
                upload.Name = DateTime.Now.ToString("yyyyMMddHHmmss").ToLower().Trim().Replace(" ", "") + extension;
                string path = Path.Combine(Server.MapPath("~/images/"), upload.Name);

                upload.Status = true;
                file.SaveAs(path);
            }
            catch (Exception ex)
            {
                var sonucs = ex.Message;
                upload.Status = false;
            }
            return upload;

        }
        public void addTag(int IDPost, string[] tags)
        {
            foreach (var item in tags)
            {
                klas.cmd($"insert  into tTags(Tag,IDPost)  values('{item}','{IDPost}')");
            }
        }
    }
}