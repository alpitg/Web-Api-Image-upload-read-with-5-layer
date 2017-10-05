using Listing1.DATA;
using Listing1.DATA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listing1.DATA.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private StudentContext studentContext;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        public StudentContext Init()
        {
            return studentContext ?? (studentContext = new StudentContext());
        }

        protected override void DisposeCore()
        {
            studentContext?.Dispose();
        }
    }
}
