using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba2_yoArriendo {
    class Empleado {
        private String Rut_employee, Name_employee, Position_employee;
        private int Password;

        public Empleado(string rut_employee, string name_employee, string position_employee, int password) {
            this.Rut_employee = rut_employee;
            this.Name_employee = name_employee;
            this.Position_employee = position_employee;
            this.Password = password;
        }

        public String rut_employee { 
            get { return Rut_employee; }            
        }
        public String name_employee {
            get { return Name_employee; }
        }
        public String position_employee {
            get { return Position_employee; }
        }
        public int password {
            get { return Password; }
        }
    }
}
