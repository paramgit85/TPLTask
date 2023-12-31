﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

           // Func<int, int, int>  Sum  = (x, y) => x + y;
           // Predicate<string> isUpper = s => s.Equals(s.ToUpper());
            //bool result = isUpper("hello world!!");

            Action<object> action = (object obj) =>
            {
                Console.WriteLine("Task={0}, Time={1}", Task.CurrentId, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                Task.CurrentId, obj,
                Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Task={0}, doing job ", Task.CurrentId);
                Thread.Sleep(5000);
                Console.WriteLine("Task={0}, job done", Task.CurrentId);
                Console.WriteLine();
            };


            Action action1 = new Action(() =>
            {

                Console.WriteLine("Task={0}, Time={1}", Task.CurrentId, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                Console.WriteLine("Task={0},  Thread={2}",
                Task.CurrentId,
                Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Task={0}, doing job ", Task.CurrentId);
                Thread.Sleep(10000);
                Console.WriteLine("Task={0}, job done", Task.CurrentId);

            });

            




            //Approach-1
            // Create a task but do not start it.
            Task t1 = new Task(action, "alpha");

                       
            


            //Approach-2
            // Construct a started task
            Task t2 = Task.Factory.StartNew(action, "beta");
            // Block the main thread to demonstrate that t2 is executing
            t2.Wait();

            // Launch t1 
            t1.Start(); 
            Console.WriteLine("t1 has been launched. (Main Thread={0})",
                              Thread.CurrentThread.ManagedThreadId);
            // Wait for the task to finish.
             t1.Wait();



            //Approach-3
            // Construct a started task using Task.Run.
            String taskData = "delta";
            Task t3 = Task.Run(() =>
            {
                Console.WriteLine("Task={0}, Time={1}", Task.CurrentId, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                Console.WriteLine("Task={0}, obj={1}, Thread={2}",
                                                         Task.CurrentId, taskData,
                                                          Thread.CurrentThread.ManagedThreadId);


                Console.WriteLine("Task={0}, doing job ", Task.CurrentId);
                Thread.Sleep(5000);
                Console.WriteLine("Task={0}, job done", Task.CurrentId);

            });
            // Wait for the task to finish.
            t3.Wait();





            ////Approach-4
            // Construct an unstarted task
            Task t4 = new Task(action, "gamma");
            // Run it synchronously
            t4.RunSynchronously();
            // Although the task was run synchronously, it is a good practice
            // to wait for it in the event exceptions were thrown by the task.
            t4.Wait();

            Console.ReadLine();
        }


        private async static void Test()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("intit taks" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                Thread.Sleep(4000);
                Console.WriteLine("end task" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));                

            });

            Console.WriteLine("next line executed after task completed-" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        }


        private async void TaskReturn()
        {
            int[] numbers = Enumerable.Range(0, 1000000).ToArray();

            // Use Task.Run to start a task that computes the sum of numbers
            Task<long> task = Task.Run(() =>
            {
                long total = 0;
                for (int i = 0; i < numbers.Length; i++)
                {
                    total += numbers[i];
                }
                return total;
            });

            Console.WriteLine("Doing some other work...");

            // Await the task to retrieve the result
            long result = await task;
            Console.WriteLine($"Sum is {result}");
        }


    }

}
