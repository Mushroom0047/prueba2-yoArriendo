using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba2_yoArriendo {
    class Cliente {
        private String Client_rut, Client_name;
        private int Client_phone;

        public Cliente(string client_rut, string client_name, int client_phone) {
            this.Client_rut = client_rut;
            this.Client_name = client_name;
            this.Client_phone = client_phone;
        }
        public String client_rut {
            get { return Client_rut; }
        }
        public String client_name {
            get { return Client_name; }
        }
        public int client_phone {
            get { return Client_phone; }
        }
    }
}
