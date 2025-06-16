using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ddbind.Models;
namespace MVC_ddbind.Controllers
{
    public class DBInsertController : Controller
    {
        MVC_dropdownEntities dbobj = new MVC_dropdownEntities();
        StateDistrictDB DBclsobj = new StateDistrictDB();

        // GET: DBInsert
        public ActionResult Insert_Pageload()
        {
            //State Dropdownlist
            List<stclass> stList = DBclsobj.Selectstates();
            ViewBag.Selstates = new SelectList(stList, "sId", "sName");
            return View();
        }
        public JsonResult GetDistricts(int stateId)
        {
            var districts = GetDistrictsByStateId(stateId);
            return Json(districts, JsonRequestBehavior.AllowGet);
        }
        private List<SelectListItem>GetDistrictsByStateId(int stateId)
        {
            var getdistricts = DBclsobj.Selectdistricts(stateId);
            var districtsbystate = getdistricts.Select(a => new SelectListItem() { Value = a.dId.ToString(), Text = a.dName }).ToList();
            return districtsbystate;


        }
        public ActionResult Insert_click(UserInsert clsobj,HttpPostedFileBase file,FormCollection form)
        {
            if (ModelState.IsValid)
            {


                if (file.ContentLength > 0)
                {
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/PHS");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);
                    var fullpath = Path.Combine("~\\PHS", fname);
                    clsobj.Photo = fullpath;
                }
                List<stclass> stList = DBclsobj.Selectstates();
                int selectedId = Convert.ToInt32(form["sId"]);
                stclass selectedItem = stList.FirstOrDefault(c => c.sId == selectedId);
                clsobj.sId = selectedItem.sId;
                clsobj.sName = selectedItem.sName;

                int disId = Convert.ToInt32(form["DistrictId"]);
                clsobj.dId = disId;
                ViewBag.Selstates = new SelectList(stList, "sId", "sName");

                dbobj.sp_insert(clsobj.sId, clsobj.dId, clsobj.Name, clsobj.Age, clsobj.Photo);
                clsobj.msg = "Successfully inserted";
                return View("Insert_Pageload", clsobj);
            }
            else
            {
                List<stclass> stList = DBclsobj.Selectstates();
                ViewBag.Selstates = new SelectList(stList, "sId", "sName");
            }
            return View("Insert_Pageload", clsobj);
        }
    }
}