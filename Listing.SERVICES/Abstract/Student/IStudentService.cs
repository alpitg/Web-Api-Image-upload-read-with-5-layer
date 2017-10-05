using Listing1.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listing1.SERVICES.Abstract.Student
{
    public interface IStudentService
    {
        /// <summary>
        /// Gets the Student by identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        StudentViewModel GetStudentById(long id);


        /// <summary>
        /// Get All Students
        /// </summary>
        /// <returns></returns>
        List<StudentViewModel> GetAllStudentsWithoutParam();


        /// <summary>
        ///  Adds the student
        /// </summary>
        /// <param name="addStudentModel"></param>
        /// <returns></returns>
        int AddStudent(StudentViewModel addStudentModel);


        /// <summary>
        /// Update the Student details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateStudentModel"></param>
        /// <returns></returns>
        int UpdateStudent(long id, StudentViewModel updateStudentModel);


        /// <summary>
        /// Delete the Student by identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteStudent(long id);
    }
}
