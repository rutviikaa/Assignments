using StudentApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentApplication.Controllers
{
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        StudentDbEntities student = new StudentDbEntities();

        [HttpGet]
        public IQueryable<Student> GetStudentDetails()
        {
            try
            {
                return student.Students;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("{studentId}")]
        public IHttpActionResult GetStudentDetailById(int studentId)
        {
            Student stud = new Student();
            
            try
            {
                stud = student.Students.Find(studentId);
                if (stud == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(stud);
        }

        [HttpPost]
        
        public IHttpActionResult PostStudentDetail(Student data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (var database = new StudentDbEntities())
                {
                    var insert = database.spInsert(data.Name, data.MobileNumber, data.EmailId, data.CourseEnrolled, data.FeesStatus);
                    database.SaveChanges();
                }
                    
            }
            catch (Exception)
            {
                throw;
            }



            return Ok(data);
        }

        [HttpPut]
        public IHttpActionResult UpdateStudentDetail(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Student stud = new Student();
                
                using (var database = new StudentDbEntities())
                {
                    stud = database.Students.Find(student.StudentId);
                    if (stud != null)
                    {
                        stud.Name = student.Name;
                        stud.MobileNumber = student.MobileNumber;
                        stud.EmailId = student.EmailId;
                        stud.CourseEnrolled = student.CourseEnrolled;
                        stud.FeesStatus = student.FeesStatus;

                        var update = database.spUpdate(student.StudentId, student.Name, student.MobileNumber, student.EmailId, student.CourseEnrolled, student.FeesStatus);
                        database.SaveChanges();
                    }
                    
                }
              }
            catch (Exception)
            {
                throw;
            }
            return Ok(student);
        }

        [HttpDelete]
        public IHttpActionResult DeleteStudentDetail(int studentId)
        {
            using (var database = new StudentDbEntities())
            {
                Student stud = new Student();
                
                stud = student.Students.Find(studentId);
                if (stud == null)
                {
                    return NotFound();
                }
                var delete = database.spDelete(studentId);
                database.SaveChanges();
             }
             return Ok(student);

        }
    }
}
