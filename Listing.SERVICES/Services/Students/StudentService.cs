using Listing1.SERVICES.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Listing1.VIEWMODEL;
using AutoMapper;
using Listing1.ENTITIES.Model;
using Listing1.DATA.Repositories;
using Listing1.DATA.Infrastructure;
using System.Web.Http.Cors;
using Listing1.SERVICES.Abstract.Student;

namespace Listing1.SERVICES.Services.Students
{

    /// <summary>
    /// AddStudent Service
    /// </summary>
    /// <seealso cref="IAddStudentService" />

    
    public class StudentService : IStudentService
    {
        private readonly IEntityBaseRepository<Student> _studentRepository;
        private readonly IUnitOfWork _unitOfWork;


        /// <summary>
        /// Initializes a new instance of the <see cref="StudentService"/> class.
        /// </summary>
        /// <param name="StudentRepository"></param>
        /// <param name="unitOfWork"></param>

        public StudentService(
          IEntityBaseRepository<Student> studentRepository,
          IUnitOfWork unitOfWork
          )
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }



        /// <summary>
        ///  Adds the class
        /// </summary>
        /// <param name="StudentModel"></param>
        /// <returns></returns>

        public int AddStudent(StudentViewModel addStudentModel)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Student, StudentViewModel>();
                cfg.CreateMap<StudentViewModel, Student>();
            });
            var studentData = Mapper.Map<StudentViewModel, Student>(addStudentModel);

            _studentRepository.Add(studentData);
            _unitOfWork.Commit();

            return 1;
        }



        /// <summary>
        /// Get all Students
        /// </summary>
        /// <returns></returns>

        public List<StudentViewModel> GetAllStudentsWithoutParam()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Student, StudentViewModel>();
                cfg.CreateMap<StudentViewModel, Student>();
            });

            try
            {
                var studentdata = _studentRepository.GetAll().ToList();
                var studentModelData = Mapper.Map<List<Student>, List<StudentViewModel>>(studentdata);
                return studentModelData;
            }
            catch (Exception e)
            {
                throw e;
            }


        }



        /// <summary>
        /// Gets the Student by identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public StudentViewModel GetStudentById(long id)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Student, StudentViewModel>();
                cfg.CreateMap<StudentViewModel, Student>();
            });
            try
            {
                var studentByIdData = _studentRepository.GetSingle(id);
                var studentModelData = Mapper.Map<Student, StudentViewModel>(studentByIdData);
                return studentModelData;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //Alpit..
        //Latest added
        /// <summary>
        /// Update the Student by Identifier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateStudentModel"></param>
        /// <returns></returns>

        public int UpdateStudent(long id, StudentViewModel updateStudentModel)
        {
            try
            {

                var user = _studentRepository.GetAll().SingleOrDefault(c => c.Id == id);

                if (user == null)
                {
                    return 0;
                }
                else
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<Student, StudentViewModel>();
                        cfg.CreateMap<StudentViewModel, Student>();
                    });
                    var Data = Mapper.Map<StudentViewModel, Student>(updateStudentModel);
                    Data.IsDeleted = false;
                    Data.Id = user.Id;

                    _studentRepository.Edit(user, Data); ;

                    _unitOfWork.Commit();
                    return 1;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }



        /// <summary>
        /// Delete the student by Identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public int DeleteStudent(long id)
        {
            //try
            //{
                var studentDetails = _studentRepository.FindBy(m => m.Id == id && m.IsDeleted == false).FirstOrDefault();
                if (studentDetails != null)
                    studentDetails.IsDeleted = true;
                _unitOfWork.Commit();
                return 1;
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }
    }
}
