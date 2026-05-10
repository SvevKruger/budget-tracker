using System.Transactions;

namespace BudgetTracker;

class Program
{
    static void Main(string[] args)
    {

        List<string> descriptions = new List<string>();
        List<double> amounts = new List<double>();

        while (true)
        {
            Console.WriteLine("!=== Budget Tracker ===");
            Console.WriteLine("1. Add Transaction!");
            Console.WriteLine("2. View Transactions");
            Console.WriteLine("3. View Balance");
            Console.WriteLine("4. Quit");
        
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
                        Console.WriteLine($"{i + 1}. {descriptions[i]}. {amounts[i]}");
                    }
                    break;
                case "3":
                    double balance = 0;

                    foreach (double a in amounts)
                    {
                        balance += a;
                    }

                    Console.WriteLine($"\nYour current balance is {balance:C}");
                    break;
                
                case "4" :
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}