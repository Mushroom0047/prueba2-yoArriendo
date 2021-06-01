using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace prueba2_yoArriendo {
    class Program {
        public static void yellowBackground(string s) {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        public static void redBackground(string s) {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        public static void greenBackground(string s) {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        public static Empleado currentEmployee;
        public static Conexion conn = new Conexion();
        static void Main(string[] args) {
            // execute a query and fill the array of everything
            
            conn.fillTheEmployee();
            conn.fillClients();
            conn.fillVehicle();
            conn.fillLeases();
            Control.fillTheFreeVehicles();

            yellowBackground("-----------YO ARRIENDO-------------");            

            /*------------LOGIN EMPLOYEE----------------------*/
            int loginOption = -1;
            String userRut, userName;
            int userPass;
            while (loginOption != 0) {
                yellowBackground("---------Ingreso usuario-----------");
                Console.Write("Rut: ");
                userRut = Console.ReadLine();
                Console.Write("Clave: ");
                try {
                    userPass = int.Parse(Console.ReadLine());
                } catch (FormatException fe) {
                    userPass = -1;
                }
                
                if (userRut != null && userPass != -1 ) {
                    foreach (Empleado e in Control.empleados) {
                        if (userRut.Equals(e.rut_employee) && userPass.Equals(e.password)) {
                            currentEmployee = e;
                            greenBackground($"Bienvenido { currentEmployee.name_employee}");
                            loginOption = 0;
                            break;
                        }
                    }
                } else {
                    redBackground("Debe ingresar un usuario !");
                }
            }
            /*---------------SHOW OPTION TO THE EMPLOYEE------------------*/
            int menuOption = -1;
            while (menuOption !=0) {
                yellowBackground("Seleccione una opcion!");
                Console.WriteLine("1: Arriendo de vehiculo\n2: Devolución de vehiculo\n3: Anular Arriendo\n" +
                    "4: Generar reporte(Supervisor)\n5: Reportes generales\n0: SALIR");
                try {
                    menuOption = int.Parse(Console.ReadLine());
                }catch(FormatException e){
                    menuOption = -1;
                }

                switch (menuOption) {
                    //Arrendar
                    case 1:
                        int menuLease = -1;
                        while (menuLease != 0) {
                            yellowBackground("Seleccione una opción !");
                            Console.WriteLine("1: Realizar arriendo\n2: Agregar más días\n0: SALIR");
                            try {
                                menuLease = int.Parse(Console.ReadLine());
                            } catch (FormatException e) {
                                menuLease = -1;
                            }
                            // Check the opntion entered
                            if (menuLease != -1) {
                                switch (menuLease) {
                                    case 1:
                                        // Lease a vehicle
                                        /*------------SELECT A CLIENTE------------*/
                                        yellowBackground("Seleccione un cliente");
                                        int i = 1;
                                        int clientOption = -1;
                                        int carOption = -1;    
                                        
                                        Console.WriteLine("Num|RUT|NOMBRE|TELEFONO");
                                        foreach (Cliente c in Control.clients) {
                                            Console.WriteLine(i + "  |" + c.client_rut + "|" + c.client_name + "|" + c.client_phone);
                                            i++;
                                        }
                                        i = 1;
                                        try {
                                            clientOption = int.Parse(Console.ReadLine())-1;
                                        } catch (FormatException e) {
                                            clientOption = -1;
                                        }

                                        //Check the option and save client
                                        if (clientOption >= 0 && clientOption < Control.clients.Count) {                                             
                                            /*--------------SELECT A CAR------------------*/
                                            greenBackground($"Cliente seleccionado: {Control.clients[clientOption].client_name}");
                                            yellowBackground("Seleccione el vehiculo que desea arrendar");
                                            Console.WriteLine("Num|PATENTE|VALOR POR DIA|MARCA|MODELO");
                                            foreach (Vehiculo v in Control.freeVehicles) {
                                                if (v.state.Equals("disponible")) {
                                                    Console.WriteLine($"{i} | {v.patent_id} | {v.vehicule_value} | {v.brand} | {v.model}");
                                                    i++;
                                                }
                                            }
                                            i = 1;
                                            try {
                                                carOption = int.Parse(Console.ReadLine())-1;
                                            } catch (FormatException e) {
                                                carOption = -1;
                                            }                                           
                                            
                                            /*-----------------SELECT TOTAL OF DAYS------------------*/
                                            greenBackground($"Cliente seleccionado: {Control.clients[clientOption].client_name}");
                                            greenBackground($"Vehiculo seleccionad: {Control.freeVehicles[carOption].brand}-{Control.freeVehicles[carOption].model}");
                                            yellowBackground("Cuantos días desea arrendar el vehiculo ?");
                                            int daysToLease = -1;
                                            
                                            try {
                                                daysToLease = int.Parse(Console.ReadLine());
                                            } catch (FormatException e) {
                                                daysToLease = -1;
                                            }
                                            int confirmChoise = -1;
                                            while (confirmChoise !=0) {
                                                greenBackground($"Cliente seleccionado: {Control.clients[clientOption].client_name}");
                                                greenBackground($"Vehiculo seleccionad: {Control.freeVehicles[carOption].brand}-{Control.freeVehicles[carOption].model}");
                                                greenBackground($"Dias de arriendo: {daysToLease}");
                                                greenBackground("-----------------------------------------");
                                                greenBackground($"Valor a pagar: ${Control.freeVehicles[carOption].vehicule_value*daysToLease}");

                                                Console.WriteLine("Esta seguro que desea arrendar este vehiculo ?");
                                                Console.WriteLine("1: SI\n2: NO");

                                                try {
                                                    confirmChoise = int.Parse(Console.ReadLine());
                                                } catch (FormatException e) {
                                                    confirmChoise = -1;
                                                }
                                                if (confirmChoise == 1) {
                                                    /*------------------CALL DATABASE-------------------*/
                                                    string date = DateTime.Now.ToString("M/dd/yyyy");
                                                    DateTime newDate = DateTime.Now;
                                                    //newDate = newDate.AddDays(daysToLease);
                                                    //string strNewDate = newDate.ToString("M/dd/yyyy");

                                                    //Control.vehicles[carOption].state = "arrendado";                                            
                                                    string queryVehicle = $"UPDATE vehiculo SET estado='arrendado' WHERE id_patente='{Control.freeVehicles[carOption].patent_id}'";
                                                    Debug.WriteLine(queryVehicle);
                                                    string n = "NULL";
                                                    string queryLease = $"INSERT INTO arriendo VALUES({daysToLease}, '{date}', {n}, {Control.freeVehicles[carOption].vehicule_value * daysToLease},'{Control.clients[clientOption].client_rut}', '{Control.freeVehicles[carOption].patent_id}','{currentEmployee.rut_employee}')";
                                                    Debug.WriteLine(queryLease);
                                                    conn.justQuery(queryVehicle);
                                                    conn.justQuery(queryLease);
                                                    Control.freeVehicles.RemoveAt(carOption);
                                                    confirmChoise = 0;
                                                    conn.fillLeases();
                                                    greenBackground("Registro ingresado correctamente !");
                                                } else {
                                                    confirmChoise = -1;
                                                    break;
                                                }
                                            }                                            

                                        }
                                        break;
                                    case 2:
                                        // increase days of lease
                                        int iii = 1;
                                        yellowBackground("Seleccione un cliente");
                                        Console.WriteLine("Num|RUT|NOMBRE|TELEFONO");
                                        foreach (Cliente c in Control.clients) {
                                            Console.WriteLine(iii + "  |" + c.client_rut + "|" + c.client_name + "|" + c.client_phone);
                                            iii++;
                                        }
                                        try {
                                            clientOption = int.Parse(Console.ReadLine())- 1;
                                        } catch (FormatException e) {
                                            clientOption = -1;
                                        }
                                        iii = 1;

                                        if (clientOption >= 0 && clientOption < Control.clients.Count) {
                                            Boolean clientExist = false;
                                            string codes = "";
                                            
                                            //Check if the client have leases                                                                                       
                                            foreach (Arriendo a in Control.arriendos) {
                                                if (a.Id_client.Equals(Control.clients[clientOption].client_rut)) {
                                                    clientExist = true;
                                                    codes += $"código de arriendo :{a.Id_lease}\n";
                                                }
                                            }
                                            if (clientExist) {
                                                greenBackground($"Cliente seleccionado: {Control.clients[clientOption].client_name}");
                                                yellowBackground("ingrese el codigo del arriendo que desea modificar");
                                                Console.WriteLine(codes);
                                                int codeToChange = -1;
                                                try {
                                                    codeToChange = int.Parse(Console.ReadLine());
                                                } catch (FormatException e) {
                                                    codeToChange = -1;
                                                }
                                                if (codeToChange > 0) {                                                   
                                                    // Enter total of days
                                                    greenBackground($"Cliente seleccionado: {Control.clients[clientOption].client_name}");
                                                    greenBackground($"Arriendo seleccionado: {codeToChange}");
                                                    yellowBackground("ingrese la cantidad de días");
                                                    int daysToMod = -1;
                                                    try {
                                                        daysToMod = int.Parse(Console.ReadLine());                                                                                                                       
                                                    } catch (FormatException e) {
                                                        daysToMod = -1;
                                                    }
                                                                                                  
                                                    // Define variables
                                                    string daysM = daysToMod.ToString();
                                                    //DateTime newDate2, newDate3;                                                     
                                                    //string dateM;
                                                    int totalM = 0; 
                                                    string idArrM;
                                                    foreach (Arriendo a in Control.arriendos) {
                                                        if (a.Id_lease.Equals(codeToChange.ToString())) {
                                                            if (daysToMod > a.Days) {
                                                                //newDate2 = DateTime.Parse(a.Date_lease);
                                                                //newDate3 = newDate2.AddDays(daysToMod);
                                                                //dateM = newDate3.ToString("M/dd/yyyy");
                                                                idArrM = codeToChange.ToString();

                                                                foreach (Vehiculo v in Control.vehicles) {
                                                                    if (v.patent_id.Equals(a.Id_patent)) {
                                                                        totalM = v.vehicule_value * daysToMod;
                                                                    }
                                                                }
                                                                string queryTOmodify = $"UPDATE arriendo SET dias={daysM}, total_arriendo={totalM} where id_arriendo='{idArrM}'";                                                                
                                                                conn.justQuery(queryTOmodify);
                                                                greenBackground("Actualización realizada !");
                                                            } else {
                                                                redBackground("No puede disminuir la cantidad de días !");
                                                            }
                                                        }
                                                    }                                                                                                                                                           
                                                } else {
                                                    redBackground("Código invalido !");
                                                }
                                                
                                            } else {
                                                redBackground("El cliente no posee vehiculos arrendados!");
                                                menuLease = 0;
                                            }
                                        }
                                            break;
                                    case 0:
                                        // Get out
                                        menuLease = 0;
                                        break;
                                }
                            } else {
                                Console.WriteLine("Ingrese una opción valida");
                            }
                        }                        
                        break;

                    //Devolución
                    case 2:
                        int optionToDev;
                        yellowBackground("Ingrese el vehiculo que desea devolver");
                        foreach (Arriendo a in Control.arriendos) {
                            if (a.Date_devolution == null || a.Date_devolution.Equals("")) {
                                Console.WriteLine($"{a.Id_lease} | {a.Id_patent}");
                            }
                            //Console.WriteLine($"{a.Id_lease} | {a.Id_patent} | {a.Date_devolution}");
                        }
                        try {
                            optionToDev = int.Parse(Console.ReadLine());
                        } catch (FormatException e) {
                            optionToDev = -1;
                        }
                        if (optionToDev != -1) {
                            foreach (Arriendo ar in Control.arriendos) {
                                if (ar.Id_lease.Equals(optionToDev.ToString())) {
                                    string date = DateTime.Now.ToString("M/dd/yyyy");
                                    Debug.WriteLine(date.ToString());
                                    string updateArriendo = $"UPDATE arriendo SET fecha_termino='{date}' where id_patente='{ar.Id_patent}'";
                                    string queryUpdateV = $"update vehiculo set estado='disponible' where id_patente='{ar.Id_patent}'";
                                    conn.justQuery(updateArriendo);
                                    conn.justQuery(queryUpdateV);
                                    conn.fillLeases();
                                    conn.fillVehicle();
                                    Control.fillTheFreeVehicles();
                                    greenBackground("Devoluciónn realizada con exito !");
                                    break;
                                }
                            }
                        }
                        break;

                    //Anular (super)
                    case 3:
                        if (currentEmployee.position_employee.Equals("Supervisor")) {
                            int optionToAnu;
                            yellowBackground("Ingrese el registro que desea anular");
                            foreach (Arriendo a in Control.arriendos) {
                                Console.WriteLine($"{a.Id_lease} | {a.Id_patent}");
                            }
                            try {
                                optionToAnu = int.Parse(Console.ReadLine());
                            } catch (FormatException e) {
                                optionToAnu = -1;
                            }
                            if (optionToAnu != -1) {
                                foreach (Arriendo arr in Control.arriendos) {
                                    if (arr.Id_lease.Equals(optionToAnu.ToString())) {
                                                                               
                                        string cuDate = DateTime.Now.ToString("M/dd/yyyy");
                                        string cuTime = DateTime.Now.ToString(format: "HH:mm:ss");
                                        DateTime arrDateLease = DateTime.Parse(arr.Date_lease);
                                        DateTime arrDateDevolution = DateTime.Parse(arr.Date_devolution);
                                        //Debug.WriteLine(cuTime);
                                        string queryAudit = $"INSERT INTO auditoria VALUES('{cuDate}', '{cuTime}', {arr.Id_lease}, {arr.Days}, '{arrDateLease.ToString("M/dd/yyyy")}', '{arrDateDevolution.ToString("M/dd/yyyy")}', {arr.Total_lease}, '{currentEmployee.rut_employee}')";
                                        string queryDelete = $"Delete FROM arriendo where id_arriendo='{optionToAnu}'";
                                        string queryUpdateV = $"update vehiculo set estado='disponible' where id_patente='{arr.Id_patent}'";
                                        //Debug.WriteLine(queryAudit);
                                        conn.justQuery(queryAudit);
                                        conn.justQuery(queryUpdateV);
                                        conn.justQuery(queryDelete);
                                        conn.fillLeases();
                                        conn.fillVehicle();
                                        Control.fillTheFreeVehicles();
                                        greenBackground("Anulación realizada con exito !");
                                        break;
                                    }
                                }
                            }
                        } else {
                            redBackground("NO tiene autorización para esto !");
                        }
                        break;

                    //Generar reporte (super)
                    case 4:
                        yellowBackground("Reportes SUPERVISOR");
                        if (currentEmployee.position_employee.Equals("Supervisor")) {

                            int optReport = -1;
                            while (optReport != 0) {
                                yellowBackground("Ingrese una opción");
                                Console.WriteLine("1: Vehiculos pendientes de entrega\n2: Informe por fechas\n3: Transacciones críticas\n0: SALIR");
                                try {
                                    optReport = int.Parse(Console.ReadLine());
                                } catch (FormatException e) {
                                    optReport = -1;
                                }
                                switch (optReport) {
                                    case 1:
                                        //Vehiculos pendientes de entrega
                                        break;
                                    case 2:
                                        //Informe por fechas
                                        break;
                                    case 3:
                                        //Transacciones críticas
                                        string title = "id_auditoria | fecha | hora | dias | total arriendo | fecha_arriendo | id_termino | total_arriendo | rut_empleado";
                                        Console.WriteLine(title.ToUpper());
                                        conn.reportAudit();
                                        greenBackground("Reporte generado!");
                                        break;
                                    case 0:
                                        //SALIR
                                        optReport = 0;
                                        break;
                                }

                            }
                        } else {
                            redBackground("NO tiene autorización para esto !");
                        }
                        
                        break;

                    //Reporte general
                    case 5:
                        if (currentEmployee.position_employee.Equals("Ejecutivo")) {
                            yellowBackground("Reportes EJECUTIVO");
                            int ejeReport = -1;
                            while (ejeReport != 0) {
                                yellowBackground("Ingrese una opción");
                                Console.WriteLine("1: Vehiculos disponibles\n2: Busqueda por marca/Modelo\n0: SALIR");
                                try {
                                    ejeReport = int.Parse(Console.ReadLine());
                                } catch (FormatException e) {
                                    ejeReport = -1;
                                }
                                switch (ejeReport) {
                                    case 1:
                                        //Vehiculos disponibles
                                        string titleVe = "NUM | id_patente| valor | marca | modelo| estado";
                                        Console.WriteLine(titleVe.ToUpper());
                                        conn.reportVehicle();
                                        greenBackground("Reporte generado!");
                                        break;
                                    case 2:
                                        //Busqueda por marca/Modelo
                                        yellowBackground("Ingrese la Marca/Modelo que desea buscar");
                                        string wordToSearch = Console.ReadLine();                                       
                                        foreach (Vehiculo v in Control.vehicles) {
                                            string chain = $"{v.brand}|{v.model}";
                                            Boolean contain = chain.Contains(wordToSearch);
                                            if (contain) {
                                                greenBackground($"marca: {v.brand} | modelo: {v.model} | estado: {v.state}");
                                            }
                                        }
                                        
                                        break;                                    
                                    case 0:
                                        //SALIR
                                        ejeReport = 0;
                                        break;
                                }

                            }
                        } else {
                            redBackground("NO tiene autorización para esto !");
                        }
                        break;
                    //Salir
                    case 0:
                        menuOption = 0;                       
                        break;
                }
            }
        }
    }
}
