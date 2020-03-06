using System;
using System.IO;

namespace todoApp
{
    public class task
    {
        public string title {get; set;}
        public DateTime startTime {get; set;}
        public DateTime endTime {get; set;}

        public DateTime created {get; set;}
        public bool done {get; set;}

        public bool isRecurring {get; set;}
        public task (string title, DateTime created, bool isRecurring = false)
        {
            this.isRecurring = isRecurring;
            this.title = title;
            this.done = false;
            this.created = created;
            this.startTime = DateTime.Now;
        }

        public void complete()
        {
            this.endTime = DateTime.Now;
            this.done = true;
            if (!this.isRecurring)
            {
                deleteFromDisk();
            }
        }

        public void deleteFromDisk()
        {
            // remove task from disk
            string[] Lines = File.ReadAllLines(@"save.txt");
            File.Delete(@"save.txt");// Deleting the file
            using (StreamWriter sw = File.AppendText(@"save.txt"))
            {
                foreach (string line in Lines)
                {
                    if (line.IndexOf(this.title) >= 0)
                    {
                        //Skip the line
                        continue;
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }

        public void SaveToDisk()
        {
            // Save to disk
            using (StreamWriter sw = File.AppendText("save.txt")) 
            {
                sw.WriteLine("%"+startTime.Date.ToShortDateString()+"%" + this.title);
            }
        }
    }
}