using EventIDV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EventIDV2.Controllers
{
    public class StudentController : ApiController
    {
        DB_EventIDEntities EventRepository = new DB_EventIDEntities();
        public IEnumerable<Student> GetAllStudents()
        {
            return EventRepository.Students.ToList();
        }

        public HttpResponseMessage GetStudent(int id)
        {
            Student student = EventRepository.Students.FirstOrDefault(_=>_.Id == id);
            if (student == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student Not found for the Given ID");
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
        }

        

        public HttpResponseMessage PostStudent(Student student)
        {
            var studentToModify = EventRepository.Students.FirstOrDefault(_ => _.Id == student.Id);
            studentToModify.FirstName = student.FirstName;
            studentToModify.LastName = student.LastName;
            studentToModify.Password = student.Password;
            studentToModify.Bio = student.Bio;
            EventRepository.SaveChanges();
            var response = Request.CreateResponse<Student>(HttpStatusCode.Created, student);
            string uri = Url.Link("http://eventid.rodrigueh.com/api/Student/", new { id = student.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage PutStudent(Student student)
        {
            if (EventRepository.Students.FirstOrDefault(_=>_.Email == student.Email) != null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Email already exists");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    EventRepository.Students.Add(student);
                    EventRepository.SaveChanges();
                    var studentToReturn = EventRepository.Students.FirstOrDefault(_ => _.Email == student.Email);
                    var response = Request.CreateResponse<Student>(HttpStatusCode.Created, studentToReturn);
                    string uri = Url.Link("http://eventid.rodrigueh.com/api/Student/", new { id = student.Id });
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        public HttpResponseMessage DeleteProduct(int id)
        {
            EventRepository.Students.Remove(EventRepository.Students.FirstOrDefault(_ => _.Id == id));
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}