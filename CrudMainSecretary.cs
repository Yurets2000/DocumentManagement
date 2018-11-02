using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement
{
    public class CrudMainSecretary
    {
        /*
        public static MainSecretary CreateMainSecretary(MainSecretary mainSecretary)
        {
            int id = SqlMainSecretary.AddMainSecretary(mainSecretary);
            mainSecretary.EmployeeId = id;
            SqlPerson.UpdatePerson(mainSecretary);
            //Добавляем управляющего в секретариат
            mainSecretary.Company.CompanyChancery.MainSecretary = mainSecretary;
            return mainSecretary;
        }

        public static MainSecretary ConstructMainSecretary(Person person, Chancery chancery, int salary)
        {
            MainSecretary mainSecretary = new MainSecretary(person, chancery.ChanceryCompany, salary);
            return mainSecretary;
        }

        public static MainSecretary ReadMainSecretary(int id)
        {
            return SqlMainSecretary.GetMainSecretary(id);
        }

        public static void UpdateMainSecretary(MainSecretary mainSecretary)
        {
            SqlMainSecretary.UpdateMainSecretary(mainSecretary);
        }

        public static void DeleteMainSecretary(MainSecretary mainSecretary)
        {
            mainSecretary.Working = false;
            SqlMainSecretary.DeleteMainSecretary(mainSecretary.EmployeeId);
            SqlPerson.UpdatePerson(mainSecretary);
            mainSecretary = null;
        }
        */
    }
}
