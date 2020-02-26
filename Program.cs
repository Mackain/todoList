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
            window ui = new window();
            todo.loadTasksFromDisk();
            string input = "";
            string weekday = DateTime.Now.DayOfWeek.ToString();
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) {
                weekday = "Wu-Tang Wednesday";
            }
            while(true)
            {
                Console.Clear();
                ui.printHeader();
                todo.listTasks();
                ui.printFooter();
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
