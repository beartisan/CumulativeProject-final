using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProject.Models;

namespace CumulativeProject.Controllers
{
    public class TeacherController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List?SearchKey={value}

        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController MyController = new TeacherDataController();
            IEnumerable<Teacher> MyTeacher = MyController.ListTeachers(SearchKey);

            //console.log stuff
            Debug.WriteLine("SearchKey is " + SearchKey);
            Debug.WriteLine("I have accessed " + MyTeacher.Count());

            return View(MyTeacher);
        }

        // GET: Teacher/Show/{TeacherId}
        public ActionResult Show(int id)
        {
            TeacherDataController MyController = new TeacherDataController();
            Teacher SelectedTeacher = MyController.FindTeacher(id);

            return View(SelectedTeacher);
        }

    }
}