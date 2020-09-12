using static System.Console;

namespace _02_Singleton
{
    public sealed class Singleton
    {
        private static Singleton _instance = null;
        private static int _instancesCount = 0;
        private static int _count = 0;

        private Singleton()
        {
            _instancesCount++;
            WriteLine($"Singleton Instances: {_instancesCount}");
        }

        public static Singleton GetSingleton()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
                return _instance;
            }

            return _instance;
        }

        public void AddCount()
        {
            WriteLine($"Count: {++_count}");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var singleton1 = Singleton.GetSingleton();
            singleton1.AddCount();
            singleton1.AddCount();
            singleton1.AddCount();

            var singleton2 = Singleton.GetSingleton();
            singleton2.AddCount();
            singleton2.AddCount();
            singleton2.AddCount();
        }
    }
}
