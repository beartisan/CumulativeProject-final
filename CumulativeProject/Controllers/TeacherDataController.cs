using CumulativeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
///using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CumulativeProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        //public int TeacherId { get; private set; }

        /// <summary>
        /// Returns a list of information about teachers
        /// </summary>
        /// <example> GET api/TeacherData/ListTeachers </example>
        /// <returns>
        /// List of Teachers
        /// {teacherFname, TeacherLname, EmployeeNumber, TeacherId, Salary, HireDate}
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the Connection
            Conn.Open();

            //Create a new command for the database
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql query
            cmd.CommandText = "Select * from Teachers where teacherfname like @key or teacherlname like @key or lower(concat(teacherfname, ' ', teacherlname)) like @key or salary like @key or hiredate like @key or employeenumber like @key";

            //clean and sanitize
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();


            MySqlDataReader ResultSet = cmd.ExecuteReader();

           

            List<Teacher> Teachers = new List<Teacher> ();

            while (ResultSet.Read())
            {
                //string teacher= ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
                int TeacherId = Convert.ToInt32(ResultSet["TeacherId"]);
                string TeacherFname = ResultSet["TeacherFname"].ToString();
                string TeacherLname = ResultSet["TeacherLname"].ToString();
                string EmployeeNumber = ResultSet["EmployeeNumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["HireDate"]);
                decimal Salary = Convert.ToDecimal(ResultSet["Salary"].ToString());


                Teacher NewTeacher = new Teacher();

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                Teachers.Add(NewTeacher);
            }

            //Close the connection
            Conn.Close();


            return Teachers;

        }

        ///<summary>
        ///Returns an individual author from the database by specifying the primary key teacherid
        /// </summary>
        /// <param name="TeacherId"> gives the teacher's ID in the database
        /// </param>
        /// <return>Returns the Teacher object
        /// </return>

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]

        public Teacher FindTeacher(int TeacherId)
        {
            //Create a connection
            MySqlConnection Conn = School.AccessDatabase();


            //Open the connection
            Conn.Open();

            //SQL Query
            string query = "Select * from teachers where TeacherId=@key " + TeacherId; //.ToString();

            //Establish a new command query for our db
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = query;

            //sanitize the teacherId input
            cmd.Parameters.AddWithValue("@id", TeacherId);
            cmd.Prepare();

            //Gather ResultSet of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            Teacher SelectedTeacher = new Teacher();

            //while loop
            while (ResultSet.Read())
            {
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["TeacherId"]);
                SelectedTeacher.TeacherFname = ResultSet["TeacherFname"].ToString();
                SelectedTeacher.TeacherLname = ResultSet["TeacherLname"].ToString();
                SelectedTeacher.EmployeeNumber = ResultSet["EmployeeNumber"].ToString();
                SelectedTeacher.HireDate = Convert.ToDateTime(ResultSet["HireDate"]);
                SelectedTeacher.Salary = Convert.ToDecimal(ResultSet["Salary"].ToString());

            }
            
            Conn.Close();


            return SelectedTeacher;
        }
    }
}
