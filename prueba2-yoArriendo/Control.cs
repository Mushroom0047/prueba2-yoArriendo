using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba2_yoArriendo {
    class Control {
        public static List<Empleado> empleados = new List<Empleado>();
        public static List<Cliente> clients = new List<Cliente>();
        public static List<Vehiculo> vehicles = new List<Vehiculo>();
        public static List<Vehiculo> freeVehicles = new List<Vehiculo>();
        public static List<Arriendo> arriendos = new List<Arriendo>();

        public static void addEmployee(Empleado e) {
            empleados.Add(e);
        }

        public static void addClient(Cliente c) {
            clients.Add(c);
        }

        public static void addVehicle(Vehiculo v) {
            vehicles.Add(v);
        }

        public static void addArriendo(Arriendo a) {
            arriendos.Add(a);
        }

        public static void fillTheFreeVehicles() {
            freeVehicles.Clear();
            foreach (Vehiculo v in vehicles) {
                if (v.state == "disponible") {
                    freeVehicles.Add(v);
                    //vehicles.Remove(v);                    
                }
            }
        }
    }
}
