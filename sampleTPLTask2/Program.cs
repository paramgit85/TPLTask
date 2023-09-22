using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sampleTPLTask2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sync call
            // Calculate();


            //Async call
            //  CalculateAsync();

            //Task.WaitAll()
            // CalculateAsyncWaitAll();


            //Task.Oncomplete
            // CalculateAsyncWaitAllOnComplete();


            //.ConnueWtih
            CalculateContinueWith();

            //Async-Await
           // CalculateAysncAwait();

            Console.ReadLine();
        }

        static void Calculate()
        {
            Calculate1();
            Calculate2();
            Calculate3();
        }


        static void CalculateAsync()
        {
            Task.Run(() =>
            {
                Calculate1();
            });

            Task.Run(() =>
            {
                Calculate2();
            });

            Task.Run(() =>
            {
                Calculate3();
            });
        }


        static void CalculateAsyncWaitAll()
        {
            var task1 = Task.Run(() =>
            {
                return Calculate1();
            });

            var task2 = Task.Run(() =>
            {
                return Calculate2();
            });


            Task.WaitAll(task1, task2);

            var result1 = task1.GetAwaiter().GetResult();
            var result2 = task2.GetAwaiter().GetResult();

            Calculate3(result1, result2);
        }

        static void CalculateAsyncWaitAllOnComplete()
        {
            var task1 = Task.Run(() =>
            {
                return Calculate1();
            });

            var awaiter1 = task1.GetAwaiter();

            awaiter1.OnCompleted(() =>
            {
                var result1 = awaiter1.GetResult();
                Calculate2(result1);
            });

            Calculate3();
        }

        static void CalculateAysncAwait()
        {
            Calculate1And2();
            Calculate3();
        }


        static void CalculateContinueWith()
        {
            var task1 = Task.Run(() =>
            {
                var result1 = Calculate1();
                Console.WriteLine ( "Result1: "+ result1);
                return result1;
            })
             .ContinueWith(t1=> 
             {
                 var result2 = Calculate2(t1.Result);
                 Console.WriteLine("Result2: " + result2);
                 return result2;


             })
             .ContinueWith(t2=>
             {
                var res3 =  Calculate3();
                 var sum = t2.Result + res3;
                 Console.WriteLine("Result3: " + sum);
                 
             });          


        }


        //Async-await
        async static void Calculate1And2()
        {
           var result1 =  await Task.Run(() =>
            {
                Console.WriteLine("intit taks" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                Thread.Sleep(4000);

                Console.WriteLine("end task"+ DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                 return Calculate1();
               
            });

            Console.WriteLine("next line executed after task completed" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            Calculate2(result1);
        }





        static int Calculate1()
        {
            Thread.Sleep(2000);
            Console.WriteLine("calculating result1 - " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            return 100;
        }

        static int Calculate2()
        {
            Thread.Sleep(2000);
            Console.WriteLine("calculating result2 - " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            int val = 200;
            Console.WriteLine("Calculate2" + val);
            return val;
        }


        static int Calculate2(int result1)
        {
            Thread.Sleep(2000);
            Console.WriteLine("calculating result2 - " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

            int val = result1 + 200; ;
            Console.WriteLine("Calculate2 - " + val);
            return val;
        }


        static int Calculate3()
        {
            Thread.Sleep(2000);
            Console.WriteLine("calculating result3 - " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            return 300;
        }


        static int Calculate3(int result1, int result2)
        {
            Thread.Sleep(2000);
            Console.WriteLine("calculating result3 - " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            int result3 = result1 + result2;
            Console.WriteLine("result3: {0} ", result3);

            return result3;
        }



       
    }

}
