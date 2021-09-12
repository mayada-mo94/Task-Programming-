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
    class AsyncAwaitLocks
    {
        #region Regular Queue with a single thread
        static void Main8()
        {
            var phoneOrders = new Queue<string>();
            GetOrders("Prakash", phoneOrders);
            GetOrders("Aradhana", phoneOrders);

            foreach (var order in phoneOrders)
            {
                Console.WriteLine("Phone Order: {0}", order);
            }
        }

        #endregion
        #region Regular Queue with more than one thread
        static void Main9()
        {
            var phoneOrders = new Queue<string>();
            Task t1 = Task.Run(() => GetOrders("Prakash", phoneOrders));
            Task t2 = Task.Run(() => GetOrders("Aradhana", phoneOrders));
            Task.WaitAll(t1, t2);

            foreach (var order in phoneOrders)
            {
                Console.WriteLine("Phone Order: {0}", order);
            }
        }
        #endregion
        #region Regular Queue with manual lock and more than one thread
        static void Main10()
        {
            var phoneOrders = new Queue<string>();
            Task t1 = Task.Run(() => GetOrdersWithLock("Prakash", phoneOrders));
            Task t2 = Task.Run(() => GetOrdersWithLock("Aradhana", phoneOrders));
            Task.WaitAll(t1, t2);

            foreach (var order in phoneOrders)
            {
                Console.WriteLine("Phone Order: {0}", order);
            }
        }
        #endregion
        #region Concurrent Queue with more than one thread
        static void Main11()
        {
            var phoneOrders = new ConcurrentQueue<string>();
            Task t1 = Task.Run(() => GetOrdersForConcurrentQueue("Prakash", phoneOrders));
            Task t2 = Task.Run(() => GetOrdersForConcurrentQueue("Aradhana", phoneOrders));
            Task.WaitAll(t1, t2);

            foreach (var order in phoneOrders)
            {
                Console.WriteLine("Phone Order: {0}", order);
            }
        }
        #endregion
        #region Helper Methods 
        private static void GetOrders(string custName, Queue<string> phoneOrders)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(100);
                string order = string.Format("{0} needs {1} phones", custName, i + 5);
                phoneOrders.Enqueue(order);
            }
        }
        static object lockObj = new object();
        private static void GetOrdersWithLock(string custName, Queue<string> phoneOrders)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(100);
                string order = string.Format("{0} needs {1} phones", custName, i + 5);
                lock (lockObj)
                {
                    phoneOrders.Enqueue(order);
                }
            }
        }
        private static void GetOrdersForConcurrentQueue(string custName, ConcurrentQueue<string> phoneOrders)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(100);
                string order = string.Format("{0} needs {1} phones", custName, i + 5);
                phoneOrders.Enqueue(order);
            }
        }
        #endregion
    }
}