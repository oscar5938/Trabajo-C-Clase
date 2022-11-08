using System;
using System.Text.Json;
using Newtonsoft.Json;


namespace Bankya
{
    class Program
    {
        static BankAccount Cuenta = null;
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido a Bankya");

            try
            {
                var account = new BankAccount("Alex", 1000);
                Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");
                account.MakeDeposit(500, DateTime.Now, "Bizum");
                account.MakeWithdrawal(100, DateTime.Now, "Gasofa");
                Console.WriteLine(account.GetAccountHistory());
                Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} actual balance.");
                var accountV = new BankAccount("Vanessa", 5000);
                Console.WriteLine($"Account {accountV.Number} was created for {accountV.Owner} with {accountV.Balance} initial balance.");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("ArgumentOutOfRangeException: " + e.ToString());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("InvalidOperationException: " + e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
            Menu();
        }
        static void Menu()
        {
            Console.WriteLine("Bienvenido a Bankya, que desea hacer?: \n 1:Crear cuenta \n 2:Meter dinero \n 3:Sacar dinero \n 4:Ver movimientos \n 5:Exportar datos \n 6:Importar datos \n 7: Salir");
            int num = int.Parse(Console.ReadLine());
            switch (num)
            {
                case 1:
                    Console.WriteLine("Introduce el nombre del propietario de la cuenta");
                    string NombreProp = Console.ReadLine();
                    Console.WriteLine("Introduce la cantidad que desea meter en la cuenta");
                    int DineroInicial = int.Parse(Console.ReadLine());
                    Cuenta = new BankAccount(NombreProp, DineroInicial);
                    Console.WriteLine($"Account {Cuenta.Number} was created for {Cuenta.Owner} with {Cuenta.Balance} initial balance.");
                    Menu();
                    break;
                case 2:
                    Console.WriteLine("Escribe tu numero de cuenta");
                    string NumeroCuenta = Console.ReadLine();
                    if (Cuenta.Number == NumeroCuenta)
                    {
                        Console.WriteLine("Cuanta cantidad va a depositar?");
                        int DineroAMeter = int.Parse(Console.ReadLine());
                        Console.WriteLine("Procedencia del ingreso");
                        string motivo = Console.ReadLine();
                        Cuenta.MakeDeposit(DineroAMeter, DateTime.Now, motivo);
                    }
                    Menu();
                    break;
                case 3:
                    Console.WriteLine("Escribe tu numero de cuenta");
                    NumeroCuenta = Console.ReadLine();
                    if (Cuenta.Number == NumeroCuenta)
                    {
                        Console.WriteLine("Cuanta cantidad va a sacar?");
                        int DineroASacar = int.Parse(Console.ReadLine());
                        Console.WriteLine("Motivo de la retirada?");
                        string motivoS = Console.ReadLine();
                        Cuenta.MakeWithdrawal(DineroASacar, DateTime.Now, motivoS);
                    }
                    Menu();
                    break;
                case 4:
                    Console.WriteLine("Escribe tu numero de cuenta");
                    NumeroCuenta = Console.ReadLine();
                    if (Cuenta.Number == NumeroCuenta)
                    {
                        Console.WriteLine(Cuenta.GetAccountHistory());
                    }
                    Menu();
                    break;
                case 5:
                    Console.WriteLine("Escribe tu numero de cuenta");
                    NumeroCuenta = Console.ReadLine();
                    if (Cuenta.Number == NumeroCuenta)
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions();
                        string jsonString = System.Text.Json.JsonSerializer.Serialize(Cuenta, options);
                        File.WriteAllText("Prueba.json", jsonString);
                        Console.WriteLine(jsonString);

                    }
                    Menu();
                    break;
                case 6:
                    string jsonString2 = File.ReadAllText("Prueba.json");
                    Cuenta = JsonConvert.DeserializeObject<BankAccount>(jsonString2);
                    Console.WriteLine(Cuenta);
                    Console.WriteLine($"Account {Cuenta.Number} was created for {Cuenta.Owner} with {Cuenta.Balance} initial balance.");
                    // Menu();
                    break;
                case 7:
                    break;
                default:
                    Console.WriteLine("Has metido un parametro no reconocido por la funcion \n Introduce un numero valido");
                    Menu();
                    break;
            }
        }
    }
}
