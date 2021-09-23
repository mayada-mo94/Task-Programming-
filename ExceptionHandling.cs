using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class ExceptionHandling
    {
        #region AggregateException is represents one or more errors that occur during application execution.
        static async Task Main15(string[] args)
        {
            await ExceptionInAsyncCodeDemo();
            Console.Read();
        }
        public async static Task ExceptionInAsyncCodeDemo()
        {
            try
            {
                var task1 = Task.Run(() => throw new
                   IndexOutOfRangeException
                   ("IndexOutOfRangeException is thrown."));
                var task2 = Task.Run(() => throw new
                   ArithmeticException
                   ("ArithmeticException is thrown."));
                Task.WaitAll(task1, task2);
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex =>
                {
                    if (ex is IndexOutOfRangeException)
                        Console.WriteLine(ex.Message);
                    return ex is InvalidOperationException;
                });
            }
        }

        #endregion

        #region Exception Handling with the standard Queue classes are not thread-safe without appropriate locking.

        static long _total;
        static Queue<int> _queued;
        static void Main20()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 1000000);
            _queued = new Queue<int>(numbers);
            _total = 0;

            Task task1 = Task.Run(() => ProcessQueue());
            Task task2 = Task.Run(() => ProcessQueue());
            Task.WaitAll(task1, task2);

            Console.WriteLine("Total: {0}", _total);
        }
        static void ProcessQueue()
        {
            try
            {
                while (true)
                {
                    Interlocked.Add(ref _total, _queued.Dequeue());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().ToString());
            }
        }
        #endregion
    }
}
