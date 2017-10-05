using Listing1.SERVICES.Abstract.Student;
using Listing1.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Listing1.WEB.Controllers.Student
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]

    /// <summary>
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        private readonly IStudentService _iStudentService;

        /// <summary>
        ///   Initializes a new instance of the <see cref="StudentsController" /> class.
        /// </summary>
        /// <param name="iAddStudentService"></param>

        public StudentController(IStudentService iAddStudentService)
        {
            _iStudentService = iAddStudentService;
        }




        /// <summary>
        /// Get all the student data from databse
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetAllStudent")]
        public HttpResponseMessage GetAllStudent()
        {
            var resultData = _iStudentService.GetAllStudentsWithoutParam();

            return Request.CreateResponse(HttpStatusCode.OK, resultData, "application/json");
        }




        /// <summary>
        /// Get the particular student data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetStudentById/{id}")]
        public HttpResponseMessage GetStudentById(long id)
        {
            var resultData = _iStudentService.GetStudentById(id);
            return Request.CreateResponse(HttpStatusCode.OK, resultData, "application/json");
        }



        /// <summary>
        /// Save the student data into database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        //[Authorize]
        // POST: api/Home
        [Route("AddStudent")]
        [HttpPost]
        public int AddStudent([FromBody] StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return 0;
            }
            else
            {
                return _iStudentService.AddStudent(model);
            }

        }



        /// <summary>
        /// update the student data and save it into database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>

        [Route("UpdateStudent/{id}")]
        [HttpPut]
        public HttpResponseMessage UpdateStudent(long id, [FromBody] StudentViewModel model)
        {
            try
            {
                var response = new ResponseViewModel();

                if (!ModelState.IsValid)
                {

                    response.IsSuccess = false;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, response, "application/json");
                }
                else
                {
                    response.IsSuccess = true;
                    var returnData = _iStudentService.UpdateStudent(id, model);

                    return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");
                }

            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// Delete the Student by Identifier
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route("DeleteStudentById/{id}")]
        public HttpResponseMessage DeleteStudentById(long Id)
        {
            var response = new ResponseViewModel();

            var returnData = _iStudentService.DeleteStudent(Id);

            switch (returnData)
            {
                case 0:
                    response.IsSuccess = false;
                    break;
                case 1:
                    response.IsSuccess = true;
                    break;
                default:
                    response.IsSuccess = false;
                    response.Message = "Please try again..";
                    break;
            }

            return Request.CreateResponse(HttpStatusCode.OK, response, "application/json");

        }
    }
}