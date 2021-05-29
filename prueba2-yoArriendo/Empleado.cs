using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba2_yoArriendo {
    class Empleado {
        private String rut_employee, name_employee, position_employee;
        private int password;

        public Empleado(string rut_employee, string name_employee, string position_employee, int password) {
            this.rut_employee = rut_employee;
            this.name_employee = name_employee;
            this.position_employee = position_employee;
            this.password = password;
        }
    }
}
