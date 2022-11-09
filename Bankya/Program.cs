using System;
using System.Text.Json;
using Newtonsoft.Json;


namespace Bankya
{
    class Program
    {
        static BankAccount Cuenta = null;
        static List<BankAccount> CuentasBankya = new List<BankAccount>();
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido a Bankya");
            Menu();
        }
        static void Menu()
        {
            Console.WriteLine("Bienvenido a Bankya, que desea hacer?: \n 1:Crear cuenta \n 2:Meter dinero \n 3:Sacar dinero \n 4:Ver movimientos \n 5:Exportar datos \n 6:Importar datos \n 7: Salir");
            int num = int.Parse(Console.ReadLine());
            switch (num)
            {
                case 1:
                    CrearCuenta();
                    Menu();
                    break;
                case 2:
                    MeteDineros();
                    Menu();
                    break;
                case 3:
                    SacaDineros();
                    Menu();
                    break;
                case 4:
                    VerMovimientos();
                    Menu();
                    break;
                case 5:
                    ImportarDatos();
                    Menu();
                    break;
                case 6:
                    ExportarDatos();
                    Menu();
                    break;
                case 7:
                    break;
                default:
                    Console.WriteLine("Has metido un parametro no reconocido por la funcion \n Introduce un numero valido");
                    Menu();
                    break;
            }
        }
        static void CrearCuenta()
        {
            Console.WriteLine("Introduce el nombre del propietario de la cuenta");
            string NombreProp = Console.ReadLine();
            Console.WriteLine("Introduce la cantidad que desea meter en la cuenta");
            int DineroInicial = int.Parse(Console.ReadLine());
            Cuenta = new BankAccount(NombreProp, DineroInicial);
            Console.WriteLine($"Account {Cuenta.Number} was created for {Cuenta.Owner} with {Cuenta.Balance} initial balance.");
            CuentasBankya.Add(Cuenta);
        }
        static void MeteDineros()
        {
            Console.WriteLine("Cuanta cantidad va a depositar?");
            int DineroAMeter = int.Parse(Console.ReadLine());
            Console.WriteLine("Procedencia del ingreso");
            string motivo = Console.ReadLine();
            CuentasBankya[PreguntarCuenta()].MakeDeposit(DineroAMeter, DateTime.Now, motivo);

        }
        static void SacaDineros()
        {
            Console.WriteLine("Cuanta cantidad va a sacar?");
            int DineroASacar = int.Parse(Console.ReadLine());
            Console.WriteLine("Motivo de la retirada?");
            string motivoS = Console.ReadLine();
            CuentasBankya[PreguntarCuenta()].MakeWithdrawal(DineroASacar, DateTime.Now, motivoS);
        }
        static void VerMovimientos()
        {
            Console.WriteLine(CuentasBankya[PreguntarCuenta()].GetAccountHistory());
        }

        static int PreguntarCuenta()
        {
            Console.WriteLine("Escribe tu numero de cuenta");
            int NumeroCuenta = int.Parse(Console.ReadLine());
            return NumeroCuenta - 1;
        }
        static void ImportarDatos()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            string jsonString = System.Text.Json.JsonSerializer.Serialize(CuentasBankya, options);
            File.WriteAllText("Lista_Clientes.json", jsonString);
            Console.WriteLine(jsonString);
        }
        static void ExportarDatos()
        {
            string jsonString2 = File.ReadAllText("Lista_Clientes.json");
            CuentasBankya = JsonConvert.DeserializeObject<List<BankAccount>>(jsonString2);
            Console.WriteLine(CuentasBankya[0].Owner);
        }
    }
}
