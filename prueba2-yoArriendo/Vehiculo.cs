using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba2_yoArriendo {
    class Vehiculo {
        private String Patent_id, Brand, Model;
        private int Vehicle_value;
        private String State;

        public Vehiculo(string patent_id, string brand, string model, int vehicle_value, string state) {
            Patent_id = patent_id;
            Brand = brand;
            Model = model;
            Vehicle_value = vehicle_value;
            State = state;
        }
        public String patent_id {
            get { return Patent_id; }
        }
        public String brand {
            get { return Brand; }
        }
        public String model {
            get { return Model; }
        }
        public int vehicule_value {
            get { return Vehicle_value; }
        }
        public String state {
            get { return State; }
            set { State = value; }
        }
    }
}
