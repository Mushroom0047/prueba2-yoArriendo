using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba2_yoArriendo {
    class Control {
        public static ArrayList empleados = new ArrayList();

        public static void addEmployee(Empleado e) {
            empleados.Add(e);
        }
    }
}
