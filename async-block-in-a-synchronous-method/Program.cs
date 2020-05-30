using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace async_block_in_a_synchronous_method
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine(
                stopwatch.Elapsed.ToString() + Environment.NewLine +
                "Ready to start Task to do some work." + Environment.NewLine
            );

            Task
                .Run(() =>
                {
                    // Asynchronous task inside a Synchronous method
                    LongRunningTask();
                })
                .GetAwaiter()   // Lets us await inside a non-async method
                .OnCompleted(() =>
                {
                    Console.WriteLine(
                        Environment.NewLine +
                        stopwatch.Elapsed.ToString() + Environment.NewLine +
                        "This block is an async callback." + Environment.NewLine + 
                        "It executes when Task is done." + Environment.NewLine +
                        "PRESS ANY KEY TO EXIT." + Environment.NewLine
                    );
                });

            Console.WriteLine(
                stopwatch.Elapsed.ToString() + Environment.NewLine +
                "This line does NOT wait to execute." + Environment.NewLine +
                "We see this print almost immediately." + Environment.NewLine 
           );
            // T I C K S
            Console.WriteLine("Tick");
            Task.Delay(1000).Wait();
            Console.WriteLine("Tick");
            Task.Delay(1000).Wait();
            Console.WriteLine("Tick");
            Task.Delay(1000).Wait();

            Console.WriteLine(
                Environment.NewLine +
                stopwatch.Elapsed.ToString() + Environment.NewLine +
                "To prevent the process from exiting we" + Environment.NewLine +
                "will block here with ReadKey(). Otherwise." + Environment.NewLine +
                "the unfinished task will die." + Environment.NewLine +
                Environment.NewLine 
            ); 
            Task.Delay(2000).Wait();
            Console.WriteLine(
                stopwatch.Elapsed.ToString() + Environment.NewLine +
                "Please wait. Task will finish soon!" + Environment.NewLine
            );
            Console.ReadKey();
        }
        static void LongRunningTask()
        {
            // T O C K S
            Task.Delay(500).Wait();
            Console.WriteLine("Tock");
            Task.Delay(1000).Wait();
            Console.WriteLine("Tock");
            Task.Delay(1000).Wait();
            Console.WriteLine("Tock" + Environment.NewLine);
            Task.Delay(1000).Wait();

            Task.Delay(6500).Wait();
        }
    }
}
