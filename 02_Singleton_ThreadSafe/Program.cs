using System;
using System.Threading.Tasks;

namespace _02_Singleton_ThreadSafe
{
    class Program
    {
        public class SingletonThreadSafe
        {
            private static SingletonThreadSafe _instance = null;
            private static int _instancesCount = 0;
            private static int _count = 0;
            private static object _instanceLock = new object();

            private SingletonThreadSafe()
            {
                _instancesCount++;
                Console.WriteLine($"Singleton instances: {_instancesCount}");
            }

            public static SingletonThreadSafe GetInstance()
            {
                // this will create two instances running in parallel
                //if (_instance == null)
                //{
                //    if (_instance == null)
                //    {
                //        _instance = new SingletonThreadSafe();
                //    }
                //}
                //return _instance;


                // double-checked locking for better performance
                if (_instance == null)
                {
                    lock (_instanceLock) // lock lower the performance
                    {
                        if (_instance == null)
                        {
                            _instance = new SingletonThreadSafe();
                        }
                    }
                }
                return _instance;
            }

            public void IncrementCount(string sender)
            {
                Console.WriteLine($"Incremente Count: {++_count} from {sender}");
            }
        }

        static void Main(string[] args)
        {
            Parallel.Invoke(
                () => FromA(),
                () => FromB()
            );            
        }

        public static void FromA()
        {
            var singleton1 = SingletonThreadSafe.GetInstance();
            singleton1.IncrementCount("A");
            singleton1.IncrementCount("A");
            singleton1.IncrementCount("A");
        }

        public static void FromB()
        {
            var singleton2 = SingletonThreadSafe.GetInstance();
            singleton2.IncrementCount("B");
            singleton2.IncrementCount("B");
            singleton2.IncrementCount("B");
        }
    }
}
