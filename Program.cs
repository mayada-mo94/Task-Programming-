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
    class Program
    {
        #region Using chanining Tasks by using continuation tasks with Handle exceptions thrown from continuations
        //static async Task Main(string[] args)
        //{
        //    Task<int> task = Task.Run(
        //   () =>
        //   {
        //       Console.WriteLine($"Executing task {Task.CurrentId}");
        //       return 54;
        //   });

        //    var continuation = task.ContinueWith(
        //        antecedent =>
        //        {
        //            Console.WriteLine($"Executing continuation task {Task.CurrentId}");
        //            Console.WriteLine($"Value from antecedent: {antecedent.Result}");

        //            //throw new InvalidOperationException();
        //        });

        //    try
        //    {
        //        await task;
        //        await continuation;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        #endregion

        #region Using the Task from the Task Parallel library 
        //static async Task Main(string[] args)
        //{
        //    // Retrieve Goncharov's "Oblomov" from Gutenberg.org.
        //    string[] words = CreateWordArray(@"http://www.gutenberg.org/files/54700/54700-0.txt");

        //    #region ParallelTasks

        //    await Task.Run(() => {
        //        // Perform three tasks in parallel on the source array
        //        Parallel.Invoke(() =>
        //        {
        //            Console.WriteLine("Begin first task...");
        //            GetLongestWord(words);
        //        },  // close first Action

        //                         () =>
        //                         {
        //                             Console.WriteLine("Begin second task...");
        //                             GetMostCommonWords(words);
        //                         }, //close second Action

        //                         () =>
        //                         {
        //                             Console.WriteLine("Begin third task...");
        //                             GetCountForWord(words, "sleep");
        //                         } //close third Action
        //                     ); //close parallel.invoke

        //    });

        //    Console.WriteLine("Returned from Parallel.Invoke");
        //    #endregion

        //    Console.WriteLine("Press any key to exit");
        //    Console.ReadKey();
        //}
        //#region HelperMethods
        //private static void GetCountForWord(string[] words, string term)
        //{
        //    var findWord = from word in words
        //                   where word.ToUpper().Contains(term.ToUpper())
        //                   select word;

        //    Console.WriteLine($@"Task 3 -- The word ""{term}"" occurs {findWord.Count()} times.");
        //}

        //private static void GetMostCommonWords(string[] words)
        //{
        //    var frequencyOrder = from word in words
        //                         where word.Length > 6
        //                         group word by word into g
        //                         orderby g.Count() descending
        //                         select g.Key;

        //    var commonWords = frequencyOrder.Take(10);

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("Task 2 -- The most common words are:");
        //    foreach (var v in commonWords)
        //    {
        //        sb.AppendLine("  " + v);
        //    }
        //    Console.WriteLine(sb.ToString());
        //}

        //private static string GetLongestWord(string[] words)
        //{
        //    var longestWord = (from w in words
        //                       orderby w.Length descending
        //                       select w).First();

        //    Console.WriteLine($"Task 1 -- The longest word is {longestWord}.");
        //    return longestWord;
        //}

        //// An http request performed synchronously for simplicity.
        //static string[] CreateWordArray(string uri)
        //{
        //    Console.WriteLine($"Retrieving from {uri}");

        //    // Download a web page the easy way.
        //    string s = new WebClient().DownloadString(uri);

        //    // Separate string into an array of words, removing some common punctuation.
        //    return s.Split(
        //        new char[] { ' ', '\u000A', ',', '.', ';', ':', '-', '_', '/' },
        //        StringSplitOptions.RemoveEmptyEntries);
        //}
        //#endregion
        #endregion

        #region Async Await inside C# Locks
        //SemaphoreSlim represents a lightweight alternative to Semaphore that limits the number of threads 
        //that can access a resource or pool of resources concurrently.In this case this objects handles locking.
        //private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        //static async Task Main(string[] args)
        //{
        //    //object lockObject = new object();
        //    //lock (lockObject)
        //    //{
        //    //    //Cannot await in the body of lock statement
        //    //    //await Task.Delay(1000);
        //    //};

        //    //Pattern of locking of async/await methods with SemaphoreSlim
        //    try
        //    {
        //        await _semaphoreSlim.WaitAsync();
        //        await Task.Delay(1000);
        //    }
        //    finally
        //    {
        //        _semaphoreSlim.Release();
        //    }
        //}
        #endregion

        #region Concurrent Collections - Concuurent Queue (Thread Safe)
        //ConcurrentQueue is a thread-safe FIFO data structure.
        //It’s a specialized data structure and can be used in cases when we want to process data in a First In First Out manner.
        //Enqueue method of Queue is not designed to work with more than one thread parallelly if you want to solve it use The famous lock or use concurrent queue  keyword
        //static void Main(string[] args)
        //{
        //    var phoneOrders = new ConcurrentQueue<string>();
        //    Task t1 = Task.Run(() => GetOrdersForConcurrentQueue("Prakash", phoneOrders));
        //    Task t2 = Task.Run(() => GetOrdersForConcurrentQueue("Aradhana", phoneOrders));
        //    Task.WaitAll(t1, t2);

        //    foreach (var order in phoneOrders)
        //    {
        //        Console.WriteLine("Phone Order: {0}", order);
        //    } 
        //}
        //private static void GetOrdersForConcurrentQueue(string custName, ConcurrentQueue<string> phoneOrders)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        Thread.Sleep(100);
        //        string order = string.Format("{0} needs {1} phones", custName, i + 5);
        //        phoneOrders.Enqueue(order);
        //    }
        //}

        #endregion

        #region Concurrent Collections - Concuurent Bag (Thread Safe)
        //Represents a thread-safe, unordered collection of objects.
        //static void Main(string[] args)
        //{
        //    ConcurrentBag<int> cb = new ConcurrentBag<int>();
        //    List<Task> bagAddTasks = new List<Task>();
        //    for (int i = 0; i < 500; i++)
        //    {
        //        var numberToAdd = i;
        //        bagAddTasks.Add(Task.Run(() => cb.Add(numberToAdd)));
        //    }

        //    // Wait for all tasks to complete
        //    Task.WaitAll(bagAddTasks.ToArray());

        //    // Consume the items in the bag
        //    List<Task> bagConsumeTasks = new List<Task>();
        //    int itemsInBag = 0;
        //    while (!cb.IsEmpty)
        //    {
        //        bagConsumeTasks.Add(Task.Run(() =>
        //        {
        //            int item;
        //            if (cb.TryTake(out item))
        //            {
        //                Console.WriteLine(item);
        //                itemsInBag++;
        //            }
        //        }));
        //    }
        //    Task.WaitAll(bagConsumeTasks.ToArray());

        //    Console.WriteLine($"There were {itemsInBag} items in the bag");

        //    // Checks the bag for an item
        //    // The bag should be empty and this should not print anything
        //    int unexpectedItem;
        //    if (cb.TryPeek(out unexpectedItem))
        //        Console.WriteLine("Found an item in the bag when it should be empty");
        //}
        #endregion

        #region Exception Handling in C# Asynchronous Programming

        //static async Task Main(string[] args)
        //{
        //    await ExceptionInAsyncCodeDemo();
        //    Console.Read();
        //}
        //public async static Task ExceptionInAsyncCodeDemo()
        //{
        //    try
        //    {
        //        var task1 = Task.Run(() => throw new
        //           IndexOutOfRangeException
        //           ("IndexOutOfRangeException is thrown."));
        //        var task2 = Task.Run(() => throw new
        //           ArithmeticException
        //           ("ArithmeticException is thrown."));
        //        Task.WaitAll(task1, task2);
        //    }
        //    catch (AggregateException ae)
        //    {
        //        ae.Handle(ex =>
        //        {
        //            if (ex is IndexOutOfRangeException)
        //                Console.WriteLine(ex.Message);
        //            return ex is InvalidOperationException;
        //        });
        //    }
        //}

        #endregion
        #region Exception Handling with the standard Queue classes are not thread-safe without appropriate locking. 
        static long _total;
        static Queue<int> _queued;

        static void Main()
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
                    //Non-Blocking Synchronization (Interlocked, volatile)
                    //my ref : https://www.c-sharpcorner.com/UploadFile/1d42da/interlocked-class-in-C-Sharp-threading/#:~:text=The%20methods%20of%20this%20class,executing%20concurrently%20on%20separate%20processors.
                    Interlocked.Add(ref _total, _queued.Dequeue());
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.GetType().ToString());
            }
        }

        #endregion
    }
}