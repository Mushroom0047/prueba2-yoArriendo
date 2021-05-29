using System;

namespace prueba2_yoArriendo {
    class Program {
        public static void yellowBackground(string s) {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        static void Main(string[] args) {         
            /*------------LOGIN employee----------------------*/
            int loginOption = -1;
            String user;
            int pass;
            while (loginOption != 0) {
                yellowBackground("---------Ingreso-----------");
                Console.Write("Usuario: ");
                user = Console.ReadLine();
                Console.Write("Clave: ");
                pass = int.Parse(Console.ReadLine());
                if (user != null && pass != null ) {
                    Console.WriteLine("hola");
                    loginOption = 0;
                }
            }
        }
    }
}
