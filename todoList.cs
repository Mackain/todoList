using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace todoApp
{
    public class todoList
    {
        private List<task> tasks;
        public todoList()
        {
            this.tasks = new List<task>();
        }

        public void loadTasksFromDisk()
        {
            foreach (string line in File.ReadLines(@"save.txt"))
            {
                // check if line contains date
                if(line.Contains('{'))
                {
                    loadSchedueldTask(line);
                } else if (!line.Contains("//"))
                {
                    tasks.Add(new task(line));
                }
                
            }
        }

        public void loadSchedueldTask(string line)
        {
            string dateOrDay = line.Split('{', '}')[1];
            string format = "yyyy-MM-dd";
            var provider = new CultureInfo("en-US");
            DateTime date;

            if(DateTime.Now.DayOfWeek.ToString().ToUpper() == dateOrDay.ToUpper())
            {
                // Add a reocurring task that will not be deleted on completion
                tasks.Add(new task(line, true));
            }

            if(DateTime.TryParseExact(dateOrDay, format, provider, DateTimeStyles.RoundtripKind, out date)){
                if (date.Date <= DateTime.Now.Date)
                {
                    tasks.Add(new task(line));
                }
            } 

        }

        public void listTasks()
        {
            int i = 0;
            foreach(task t in tasks)
            {
                if (t.done)
                {   
                    Console.ForegroundColor = ConsoleColor.Green;
                    TimeSpan span = t.endTime-t.startTime;
                    double totalMinutes = span.TotalMinutes;
                    Console.WriteLine(i + ". [X] " + t.title);
                } else {
                    Console.ForegroundColor = t.title.Contains('{') ? ConsoleColor.Yellow : ConsoleColor.Gray;
                    if (t.title.Contains('!'))
                    {
                        // Urgent always overides color, even if it is dated also.
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine(i + ". [ ] " + t.title);
                }
                i ++;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void addTask(string title)
        {
            string input = "";
            var t = new task(title);
            Console.WriteLine("Create task \""+ t.title +"\"? [Y/N]");
            input = Console.ReadLine();

            switch(input.ToUpper())
            {
                case "": 
                case "Y": 
                    Console.WriteLine("Saving task.");

                    // Save to disk
                    using (StreamWriter sw = File.AppendText("save.txt")) 
                    {
                        sw.WriteLine(title);
 
                    }

                    tasks.Add(t);
                    break;
                case "N":
                    Console.WriteLine("Canceling task.");
                    break;
                default:
                    Console.WriteLine("Invalid input. Canceling task.");
                    break;
            }
        }

        public void completeTask(int index)
        {

            if (index < 0 || index >= tasks.Count)
            {
                Console.WriteLine("Invalid index. Stop trying to ruin my stuff.");
            }

            string input = "";
            Console.WriteLine("Complete task \""+ tasks[index].title +"\"? [Y/N]");
            input = Console.ReadLine();
            switch(input.ToUpper())
            {
                case "": 
                case "Y": 
                    tasks[index].complete();
                    Console.WriteLine("Done!");
                    break;
                case "N":
                    Console.WriteLine("OK.");
                    break;
                default:
                    Console.WriteLine("Invalid input. Canceling operation.");
                    break;
            }
        }

        public void completeTask()
        {
            string input = "";
            Console.WriteLine("pick task to complete:");
            int i = 1;
            foreach(task t in this.tasks)
            {
                Console.WriteLine(i + " " + t.title);
                i++;
            }
            input = Console.ReadLine();

            int index = int.Parse(input)-1;
            Console.WriteLine("Complete task \""+ tasks[index].title +"\"? [Y/N]");
            input = Console.ReadLine();
            switch(input.ToUpper())
            {
                case "": 
                case "Y": 
                    tasks[index].complete();
                    Console.WriteLine("Done!");
                    break;
                case "N":
                    Console.WriteLine("OK.");
                    break;
                default:
                    Console.WriteLine("Invalid input. Canceling operation.");
                    break;
            }
        }
    }
}