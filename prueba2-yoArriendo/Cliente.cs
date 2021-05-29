using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba2_yoArriendo {
    class Cliente {
        private String client_rut, client_name;
        private int client_phone;

        public Cliente(string client_rut, string client_name, int client_phone) {
            this.client_rut = client_rut;
            this.client_name = client_name;
            this.client_phone = client_phone;
        }
    }
}
