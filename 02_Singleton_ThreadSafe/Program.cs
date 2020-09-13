using static System.Console;
using System.Threading.Tasks;

namespace _02_Singleton_ThreadSafe
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
            WriteLine($"Singleton instances: {_instancesCount}");
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
            WriteLine($"Incremente Count: {++_count} from {sender}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parallel.Invoke(
                () => FromA(),
                () => FromB()
            );

            // Result:
            // --------------------------
            // Singleton instances: 1
            // Incremente Count: 2 from B
            // Incremente Count: 1 from A
            // Incremente Count: 3 from B
            // Incremente Count: 5 from B
            // Incremente Count: 4 from A
            // Incremente Count: 6 from A
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
