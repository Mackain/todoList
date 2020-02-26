using System;

namespace todoApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            mainMenu();
        }

        static void mainMenu()
        {
            todoList todo = new todoList();
            todo.loadTasksFromDisk();
            string input = "";
            string weekday = DateTime.Now.DayOfWeek.ToString();
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) {
                weekday = "Wu-Tang Wednesday";
            }
            while(true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(DateTime.Now.ToShortDateString());
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n$$$$$$$$\\              $$\\           $$\\       $$\\             $$\\     ");
                Console.WriteLine("\\__$$  __|             $$ |          $$ |      \\__|            $$ |    ");
                Console.WriteLine("   $$ | $$$$$$\\   $$$$$$$ | $$$$$$\\  $$ |      $$\\  $$$$$$$\\ $$$$$$\\   ");
                Console.WriteLine("   $$ |$$  __$$\\ $$  __$$ |$$  __$$\\ $$ |      $$ |$$  _____|\\_$$  _|  ");
                Console.WriteLine("   $$ |$$ /  $$ |$$ /  $$ |$$ /  $$ |$$ |      $$ |\\$$$$$$\\    $$ |    ");
                Console.WriteLine("   $$ |$$ |  $$ |$$ |  $$ |$$ |  $$ |$$ |      $$ | \\____$$\\   $$ |$$\\ ");
                Console.WriteLine("   $$ |\\$$$$$$  |\\$$$$$$$ |\\$$$$$$  |$$$$$$$$\\ $$ |$$$$$$$  |  \\$$$$  |");
                Console.WriteLine("   \\__| \\______/  \\_______| \\______/ \\________|\\__|\\_______/    \\____/ ");                              
                Console.WriteLine("\n   =================== [ Tasks for "+ weekday +" ] ==================\n");
                todo.listTasks();
                Console.WriteLine("\n");
                 Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Enter a task or an index number to complete one:");
                Console.WriteLine("Use {yyyy-MM-dd} to create tasks for specific dates, and \"!\" to create urgent tasks");
                Console.ForegroundColor = ConsoleColor.Gray;
                input = Console.ReadLine();
                int index = 0;
                bool isNumeric = int.TryParse(input, out index);
                if (isNumeric) 
                {
                    todo.completeTask(index);
                } else {
                    todo.addTask(input);
                }
                
            }
        }
    }
}
