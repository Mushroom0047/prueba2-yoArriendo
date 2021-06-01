using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba2_yoArriendo {
    class Arriendo {
        private string id_lease, id_client, id_patent, rut_empleado, date_lease, date_devolution;
        private int days, total_lease;
      
        public Arriendo(string id_lease, string id_client, string id_patent, string rut_empleado, int days, string date_lease, string date_devolution, int total_lease) {
            this.id_lease = id_lease;
            this.id_client = id_client;
            this.id_patent = id_patent;
            this.rut_empleado = rut_empleado;
            this.days = days;
            this.date_lease = date_lease;
            this.date_devolution = date_devolution;
            this.total_lease = total_lease;
        }

        public string Id_lease {
            get { return id_lease; }
        }
        public string Id_client {
            get { return id_client; }
        }
        public string Id_patent {
            get { return id_patent; }
        }
        public string Rut_empleado {
            get { return rut_empleado; }
        }
        public int Days {
            get { return days; }            
        }
        public string Date_lease {
            get { return date_lease; }
        }
        public string Date_devolution {
            get { return date_devolution; }
        }
        public int Total_lease {
            get { return total_lease; }
        }

    }
}
