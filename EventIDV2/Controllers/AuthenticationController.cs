using EventIDV2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EventIDV2.Controllers
{
    public class AuthenticationController : ApiController
    {
        public class AuthModel
        {
            [JsonProperty("username")]
            public string Username { get; set; }
            [JsonProperty("password")]
            public string Password { get; set; }
        }
        DB_EventIDEntities EventRepository = new DB_EventIDEntities();
     
        [HttpPost]
        public HttpResponseMessage Post(AuthModel auth)
        {
            if (auth != null)
            {
                var student = EventRepository.Students.FirstOrDefault(_ => _.Email == auth.Username && _.Password == auth.Password);
                if (student != null)
                    return Request.CreateResponse<Student>(HttpStatusCode.Created, student);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Username and password do not match");;
        }
    }
}