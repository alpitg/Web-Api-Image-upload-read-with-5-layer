using Listing1.ENTITIES;
using Listing1.ENTITIES.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listing1.DATA
{
    public class StudentContext : DbContext
    {
        public StudentContext() : base("dbStudentEntities")
        {
            //Database.SetInitializer<StudentContext>(null);
        }
        public virtual int Commit()
        {
            return base.SaveChanges();
        }


        public DbSet<Student> Students { get; set; }

        //// public DbSet<ClassMain> ClassMains { get; set; }
        //public DbSet<AddStudent> AddStudents { get; set; }
        //public DbSet<ProfileStudent> ProfileStudents { get; set; }
        //public DbSet<ProfileClass> ProfileClasses { get; set; }
        //public DbSet<AddClass> AddClasses { get; set; }


    }
}
