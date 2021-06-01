using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba2_yoArriendo {
    class Conexion {    
        public string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\C#\prueba2-yoArriendo\prueba2-yoArriendo\Database1.mdf;Integrated Security=True";
        SqlConnection cnn;
        SqlCommand query;
        SqlDataReader dataReader;

        public void fillTheEmployee() {         
            try {
                string queryLogin = "SELECT * FROM empleado";
                cnn = new SqlConnection(connStr);
                cnn.Open();
                query = new SqlCommand(queryLogin, cnn);
                dataReader = query.ExecuteReader();

                while (dataReader.Read()) {
                    Empleado e = new Empleado(dataReader["rut_empleado"].ToString(), dataReader["nombre"].ToString(), dataReader["cargo"].ToString(), dataReader.GetInt32(3));
                    Control.addEmployee(e);                    
                }
                dataReader.Close();

            } catch (Exception e) {
                Debug.WriteLine("Error en la conexión " + e.ToString());
            }
        }

        public void fillClients() {
            try {
                string queryLogin = "SELECT * FROM cliente";
                cnn = new SqlConnection(connStr);
                cnn.Open();
                query = new SqlCommand(queryLogin, cnn);
                dataReader = query.ExecuteReader();

                while (dataReader.Read()) {
                    Cliente e = new Cliente(dataReader["id_cliente"].ToString(), dataReader["nombre_cliente"].ToString(), dataReader.GetInt32(2));
                    Control.addClient(e);                    
                }
                dataReader.Close();

            } catch (Exception e) {
                Debug.WriteLine("Error en la conexión " + e.ToString());
            }
        }

        public void fillVehicle() {
            Control.vehicles.Clear();
            try {
                string queryLogin = "SELECT * FROM vehiculo";
                cnn = new SqlConnection(connStr);
                cnn.Open();
                query = new SqlCommand(queryLogin, cnn);
                dataReader = query.ExecuteReader();

                while (dataReader.Read()) {
                    Vehiculo v = new Vehiculo(dataReader["id_patente"].ToString(), dataReader["marca"].ToString(), dataReader["modelo"].ToString(), dataReader.GetInt32(1), dataReader["estado"].ToString());
                    Control.addVehicle(v);                    
                }
                dataReader.Close();

            } catch (Exception e) {
                Debug.WriteLine("Error en la conexión " + e.ToString());
            }
        }     

        public void justQuery(string queryTodo) {
            try {               
                cnn = new SqlConnection(connStr);
                cnn.Open();
                query = new SqlCommand(queryTodo, cnn);
                dataReader = query.ExecuteReader();              
                dataReader.Close();
                Debug.WriteLine($"query realizada ! = {queryTodo}");
            } catch (Exception e) {
                Debug.WriteLine("TAG - Error en la conexión " + e.ToString());
            }
        }      
        
        public void fillLeases() {
            Control.arriendos.Clear();
            try {
                string queryLogin = "SELECT * FROM arriendo";
                cnn = new SqlConnection(connStr);
                cnn.Open();
                query = new SqlCommand(queryLogin, cnn);
                dataReader = query.ExecuteReader();

                while (dataReader.Read()) {
                    Arriendo a = new Arriendo(dataReader["id_arriendo"].ToString(), dataReader["id_cliente"].ToString(), dataReader["id_patente"].ToString(), dataReader["rut_empleado"].ToString(), dataReader.GetInt32(1), dataReader["fecha_arriendo"].ToString(), dataReader["fecha_termino"].ToString(), dataReader.GetInt32(4));
                    Control.addArriendo(a);                   
                }
                dataReader.Close();

            } catch (Exception e) {
                Debug.WriteLine("Error en la conexión " + e.ToString());
            }
        }

        public void reportAudit() {
            try {
                string queryLogin = "SELECT * FROM auditoria";
                cnn = new SqlConnection(connStr);
                cnn.Open();
                query = new SqlCommand(queryLogin, cnn);
                dataReader = query.ExecuteReader();

                while (dataReader.Read()) {
                    Console.WriteLine($"{dataReader["id_auditoria"]} | {dataReader["fecha"]} | {dataReader["hora"]} | {dataReader["id_arriendo"]} | {dataReader.GetInt32(4)} | {dataReader["fecha_arriendo"]} | {dataReader["fecha_termino"]} | {dataReader.GetInt32(7)} | {dataReader["rut_empleado"]}");
                }
                dataReader.Close();

            } catch (Exception e) {
                Debug.WriteLine("Error en la conexión " + e.ToString());
            }
        }

        public void reportVehicle() {
            try {
                string queryLogin = "SELECT * FROM vehiculo WHERE estado='disponible'";
                cnn = new SqlConnection(connStr);
                cnn.Open();
                query = new SqlCommand(queryLogin, cnn);
                dataReader = query.ExecuteReader();
                int i = 1;
                while (dataReader.Read()) {
                    Console.WriteLine($"{i} | {dataReader["id_patente"]} | {dataReader.GetInt32(1)} | {dataReader["marca"]} | {dataReader["modelo"]} | {dataReader["estado"]}");
                    i++;
                }                
                dataReader.Close();

            } catch (Exception e) {
                Debug.WriteLine("Error en la conexión " + e.ToString());
            }
        }
    }
}
