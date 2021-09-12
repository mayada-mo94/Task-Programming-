using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class ParallelTask
    {
        #region Parallel Invoke Method
        static void Main1()
        {
            //Allowing three task to execute at a time
            ParallelOptions parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = 3
            };
            //parallelOptions.MaxDegreeOfParallelism = System.Environment.ProcessorCount - 1;
            //Passing ParallelOptions as the first parameter
            Parallel.Invoke(
                    parallelOptions,
                    () => DoSomeTask(1),
                    () => DoSomeTask(2),
                    () => DoSomeTask(3),
                    () => DoSomeTask(4),
                    () => DoSomeTask(5),
                    () => DoSomeTask(6),
                    () => DoSomeTask(7)
                );
            Console.ReadKey();
        }
        static void DoSomeTask(int number)
        {
            Console.WriteLine($"DoSomeTask {number} started by Thread {Thread.CurrentThread.ManagedThreadId}");
            //Sleep for 500 milliseconds
            Thread.Sleep(5000);
            Console.WriteLine($"DoSomeTask {number} completed by Thread {Thread.CurrentThread.ManagedThreadId}");
        }
        #endregion

        #region Task is executed by TaskFactory asynchronously and continue 
        static void Main2()
        {
            Task t2 = null;
            Task t = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Digging is in progress");
            });

            Console.WriteLine("");

            t2 = t.ContinueWith((a) =>
            {
                Console.WriteLine("Clean the area");
                for (int index = 1; index <= 5; index++)
                {
                    Console.WriteLine("Cleaning...." + index);
                }
                Console.WriteLine("Cleaning done");
            });
            Task.WaitAll(t, t2);// To ensure both tasks are completed  
            Console.Read();
        }
        #endregion

        #region the TaskFactory object passes the cancellation token to each of the tasks.  
        public static void Main3()
        {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            Random rnd = new Random();
            Object lockObj = new Object();

            List<Task<int[]>> tasks = new List<Task<int[]>>();
            TaskFactory factory = new TaskFactory(token);
            for (int taskCtr = 0; taskCtr <= 10; taskCtr++)
            {
                int iteration = taskCtr + 1;
                tasks.Add(factory.StartNew(() => {
                    int value;
                    int[] values = new int[10];
                    for (int ctr = 1; ctr <= 10; ctr++)
                    {
                        lock (lockObj)
                        {
                            value = rnd.Next(0, 101);
                        }
                        if (value == 0)
                        {
                            source.Cancel();
                            Console.WriteLine("Cancelling at task {0}", iteration);
                            break;
                        }
                        values[ctr - 1] = value;
                    }
                    return values;
                }, token));
            }
            try
            {
                Task<double> fTask = factory.ContinueWhenAll(tasks.ToArray(),
                                                             (results) => {
                                                                 Console.WriteLine("Calculating overall mean...");
                                                                 long sum = 0;
                                                                 int n = 0;
                                                                 foreach (var t in results)
                                                                 {
                                                                     foreach (var r in t.Result)
                                                                     {
                                                                         sum += r;
                                                                         n++;
                                                                     }
                                                                 }
                                                                 return sum / (double)n;
                                                             }, token);
                Console.WriteLine("The mean is {0}.", fTask.Result);
            }
            catch (AggregateException ex)
            {
                foreach (Exception e in ex.InnerExceptions)
                {
                    if (e is TaskCanceledException)
                        Console.WriteLine("Unable to compute mean: {0}",
                                          ((TaskCanceledException)e).Message);
                    else
                        Console.WriteLine("Exception: " + e.GetType().Name);
                }
            }
            finally
            {
                source.Dispose();
            }
        }
        #endregion

        #region Parallel Foreach 
        static void Main4()
        {
            List<int> integerList = Enumerable.Range(0, 10).ToList();
            Parallel.ForEach(integerList, i =>
            {
                Console.WriteLine(@"value of i = {0}, thread = {1}",
                    i, Thread.CurrentThread.ManagedThreadId);
            });

            Console.WriteLine("Press any key to exist");
            Console.ReadLine();
        }
        #endregion
        #region Task.AsyncState property
        class CustomData
        {
            public long CreationTime;
            public int Name;
            public int ThreadNum;
        }
        public static void Main5()
        {
            Task[] taskArray = new Task[10];
            for (int i = 0; i < taskArray.Length; i++)
            {
                taskArray[i] = Task.Factory.StartNew((Object obj) => {
                    CustomData data = obj as CustomData;
                    if (data == null)
                        return;

                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                },
                                                      new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
            }
            Task.WaitAll(taskArray);
            foreach (var task in taskArray)
            {
                var data = task.AsyncState as CustomData;
                if (data != null)
                    Console.WriteLine("Task #{0} created at {1}, ran on thread #{2}.",
                                      data.Name, data.CreationTime, data.ThreadNum);
            }
        }
        #endregion
    }
}
