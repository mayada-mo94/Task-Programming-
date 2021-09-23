using System;
using System.Threading;

namespace TaskProgramming
{
    class Threads
    {
        #region Foreground Thread
        static void Main12(string[] args)
        {

            // Creating and initializing thread
            Thread thr = new Thread(foregroundThreadMethod);
            thr.Start();
            Console.WriteLine("Main Thread Ends!!");
        }

        // Static method
        static void foregroundThreadMethod()
        {
            for (int c = 0; c <= 3; c++)
            {

                Console.WriteLine("mythread is in progress!!");
                Thread.Sleep(1000);
            }
            Console.WriteLine("mythread ends!!");
        }
        #endregion
        #region Background Thread
        static void Main13(string[] args)
        {
            // Creating and initializing thread
            Thread thr = new Thread(mythread);

            // Name of the thread is Mythread
            thr.Name = "Mythread";
            thr.Start();

            // IsBackground is the property of Thread
            // which allows thread to run in the background
            thr.IsBackground = true;

            Console.WriteLine("Main Thread Ends!!");
        }

        //// Static method
        static void mythread()
        {

            // Display the name of the 
            // current working thread
            Console.WriteLine("In progress thread is: {0}",
                                Thread.CurrentThread.Name);

            Thread.Sleep(2000);

            Console.WriteLine("Completed thread is: {0}",
                              Thread.CurrentThread.Name);
        }
        #endregion

        #region Joining Threads
        static Thread thread1, thread2;

        public static void Main20()
        {
            thread1 = new Thread(ThreadProc);
            thread1.Name = "Thread1";
            thread1.Start();

            thread2 = new Thread(ThreadProc);
            thread2.Name = "Thread2";
            thread2.Start();
        }

        private static void ThreadProc()
        {
            Console.WriteLine("\nCurrent thread: {0}", Thread.CurrentThread.Name);
            if (Thread.CurrentThread.Name == "Thread1" &&
                thread2.ThreadState != ThreadState.Unstarted)
                if (thread2.Join(2000))
                    Console.WriteLine("Thread2 has termminated.");
                else
                    Console.WriteLine("The timeout has elapsed and Thread1 will resume.");

            Thread.Sleep(4000);
            Console.WriteLine("\nCurrent thread: {0}", Thread.CurrentThread.Name);
            Console.WriteLine("Thread1: {0}", thread1.ThreadState);
            Console.WriteLine("Thread2: {0}\n", thread2.ThreadState);
        }
        #endregion
    }
}