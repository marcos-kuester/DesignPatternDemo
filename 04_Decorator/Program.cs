using static System.Console;

namespace _04_Decorator
{
    // abstract class
    public abstract class Burger
    {
        private string _description;

        public Burger(string description)
        {
            _description = description;
        }

        public string GetDescription()
        {
            return _description;
        }

        // abstract method
        public abstract decimal Cost();
    }

    public class CheeseBurger : Burger
    {
        public CheeseBurger() : base("Cheese Burger")
        {            
        }

        public override decimal Cost()
        {
            return 8.99m;
        }
    }

    // abstract class
    public abstract class AddOn : Burger
    {
        protected Burger _burger;
        public AddOn(string description, Burger burger) : base(description)
        {
            _burger = burger;
        }

        public abstract new string GetDescription();
    }

    public class Bacon : AddOn
    {
        public Bacon(Burger burger) : base(burger.GetDescription() + " +Bacon", burger)
        {
        }

        public override decimal Cost()
        {
            return _burger.Cost() + 0.99m;
        }

        public override string GetDescription()
        {
            return _burger.GetDescription() + " +Bacon";
        }
    }

    public class Ketchup : AddOn
    {
        public Ketchup(Burger burger) : base(burger.GetDescription() + " +Ketchup", burger)
        {
        }

        public override decimal Cost()
        {
            return _burger.Cost() + 0.50m;
        }

        public override string GetDescription()
        {
            return _burger.GetDescription() + " +Ketchup";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var burger = new CheeseBurger();
            WriteLine($"{burger.GetDescription()} ${burger.Cost()}");

            var bacon = new Bacon(burger);
            WriteLine($"{bacon.GetDescription()} ${bacon.Cost()}");

            var ketchup = new Ketchup(bacon);
            WriteLine($"{ketchup.GetDescription()} ${ketchup.Cost()}");

            var doubleBacon = new Bacon(ketchup);
            WriteLine($"{doubleBacon.GetDescription()} ${doubleBacon.Cost()}");
        }
    }
}
