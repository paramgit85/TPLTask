using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace awaitSample
{
    class Program
    {
        static void Main(string[] args)
        {
           // Task1();
            //Task2();


            ExceptionHandleling();
            Console.ReadLine();
        }


        private async static void Task1()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("init Task1 -- " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                Thread.Sleep(3000);
                Console.WriteLine("end Task1 -- " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            });

            Console.WriteLine("next line executed after Task1 completed-" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        }


        private  static void Task2()
        {
             Task.Run(() =>
            {
                Console.WriteLine("init Task2 -- " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                Thread.Sleep(3000);
                Console.WriteLine("end Task2 -- " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            });

            Console.WriteLine("next line executed after Task2 completed-" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        }



        static  void ExceptionHandleling()
        {           

            try
            {
                var task = Task.Run(() => Divide(10, 0));
                var result = task.Result;
            }
            catch (AggregateException ae)
            {
                ae.Flatten().Handle(e =>
                {
                    if (e is DivideByZeroException)
                    {
                        Console.WriteLine(e.Message);
                        return true;
                    }
                    else
                    {
                        throw e;
                    }
                });
            }
        }

        static decimal Divide(decimal a, decimal b)
        {
            Thread.Sleep(1000);

            return a / b;
        }
    }
}
 