using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class IdGenerator
    {
        private static int id = 0;

        public static int GetNextValue()
        {
            return ++id;
        }
    }
}
