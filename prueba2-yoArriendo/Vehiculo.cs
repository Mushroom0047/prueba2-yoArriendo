using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba2_yoArriendo {
    class Vehiculo {
        private String patent_id, brand, model;
        private int vehicle_value;

        public Vehiculo(string patent_id, string brand, string model, int vehicle_value) {
            this.patent_id = patent_id;
            this.brand = brand;
            this.model = model;
            this.vehicle_value = vehicle_value;
        }
    }
}
