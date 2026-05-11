using System.Transactions;

namespace BudgetTracker;

class Program
{
    static void Main(string[] args)
    {

        List<string> descriptions = new List<string>();
        List<double> amounts = new List<double>();

        Console.WriteLine("Welcome to BudgetTracker!");
        Console.WriteLine("Select your currency:");
        Console.WriteLine("1. USD ($)");
        Console.WriteLine("2. EUR (€)");
        Console.WriteLine("3. RSD (din)");
        Console.WriteLine("4. RUB (₽)");
        
        Console.Write("\nChoose an option:");
        string currencyChoice = Console.ReadLine();

        string currencySymbol;

        switch (currencyChoice)
        {
            case "1": currencySymbol = "$"; break;
            case "2": currencySymbol = "€"; break;
            case "3": currencySymbol = "din"; break;
            case "4": currencySymbol = "₽"; break;
            default: currencySymbol = "$"; break;
        }

        Console.WriteLine($"Currency set to {currencySymbol}\n");

        string filepath = "transactions.txt";

        if (File.Exists(filepath))
        {
            string[] lines = File.ReadAllLines(filepath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                descriptions.Add(parts[0]);
                amounts.Add(double.Parse(parts[1]));
            }

            Console.WriteLine($"Loaded {descriptions.Count} transactions from file.");
        }

        
        while (true)
        {
            Console.WriteLine("!=== Budget Tracker ===");
            Console.WriteLine("1. Add Transaction");
            Console.WriteLine("2. View Transactions");
            Console.WriteLine("3. View Balance");
            Console.WriteLine("4. Delete Transaction");
            Console.WriteLine("5. Quit");
        
            Console.Write("\nChoose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter a description: (e.g. salary, rent)");
                    string description = Console.ReadLine();
                    
                    Console.Write("Enter amount (positive for income, negative for expenses): ");
                    if (!double.TryParse(Console.ReadLine(), out double amount))
                    {
                        Console.WriteLine("Invalid amount. Try again.");
                        break;
                    }
                    descriptions.Add(description);
                    amounts.Add(amount);
                    File.AppendAllText(filepath, $"{description},{amount}\n");
                    Console.WriteLine("Transaction successfully added.");
                    break;
                case "2":
                    if (descriptions.Count == 0)
                    {
                        Console.WriteLine("No transactions yet!");
                        break;
                    }

                    Console.WriteLine("\n--- Transactions ---");
                    for (int i = 0; i < descriptions.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {descriptions[i]}. {currencySymbol} {amounts[i]}");
                    }
                    break;
                case "3":
                    double balance = 0;

                    foreach (double a in amounts)
                    {
                        balance += a;
                    }

                    Console.WriteLine($"\nYour current balance is {currencySymbol} {balance}");
                    break;
                case "4":

                    if (descriptions.Count == 0)
                    {
                        Console.WriteLine("No transactions to delete!");
                        break;
                    }

                    Console.WriteLine("\n --- Transactions ---");
                    for (int i = 0; i < descriptions.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {descriptions[i]}. {currencySymbol} {amounts[i]}");
                    }

                    Console.WriteLine("\n Enter transaction number to delete");
                    if (!int.TryParse(Console.ReadLine(), out int index))
                    {
                        Console.WriteLine("Invalid Number. Try again.");
                        break;
                    }

                    if (index < 1 || index > descriptions.Count)
                    {
                        Console.WriteLine("Transactions not found!");
                        break;
                    }
                    
                    descriptions.RemoveAt(index - 1);
                    amounts.RemoveAt(index - 1);
                    File.WriteAllLines(filepath, descriptions.Select((d, i) => $"{d},{amounts[i]}"));
                    Console.WriteLine("Transactions deleted!");
                    break;
                
                case "5" :
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}