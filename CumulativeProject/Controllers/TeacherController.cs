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

        //GET: /Teacher/Add
        public ActionResult Add()
        {
            return View();
        }

        //POST: /Teacher/Create
   
        [HttpPost]

        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            Debug.WriteLine("I am trying to create a new Teacher with " + TeacherFname);

            Teacher NewTeacher = new Teacher();

            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController MyController = new TeacherDataController();

            MyController.AddTeacher(NewTeacher);


            //redirect to List of teachers
            return RedirectToAction("List");
        }


        //GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController MyController = new TeacherDataController();
            Teacher SelectedTeacher = MyController.FindTeacher(id);

            return View(SelectedTeacher);
        }

        //POST: /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController MyController = new TeacherDataController();
            MyController.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        //GET: /Teacher/Edit/{id}

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //need to get the information about the teacher 
            TeacherDataController MyController = new TeacherDataController();

            Teacher SelectedTeacher = MyController.FindTeacher(id);


            return View(SelectedTeacher);
        }

        //POST: /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string TeacherFName, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            //receiving information about the teachers name, salary, etc

            Debug.WriteLine(id);
            Debug.WriteLine("Receiving information about teacher");
            Debug.WriteLine(TeacherFName);
            Debug.WriteLine(TeacherLname);

            Teacher UpdatedTeacher = new Teacher();

            UpdatedTeacher.TeacherLname = TeacherLname;
            UpdatedTeacher.TeacherFname = TeacherFName;
            UpdatedTeacher.EmployeeNumber = EmployeeNumber;
            UpdatedTeacher.Salary = Salary;
            UpdatedTeacher.HireDate = HireDate;

            TeacherDataController MyController = new TeacherDataController();
            MyController.UpdateTeacher(id, UpdatedTeacher);


            return RedirectToAction("Show/"+id);
        }



    }
}