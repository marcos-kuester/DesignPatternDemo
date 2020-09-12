using static System.Console;

namespace _01_Factory
{
    public interface Shape
    {
        void Draw();
    }

    public class Square : Shape
    {
        public void Draw()
        {
            WriteLine("Draw Square");
        }
    }

    public class Circle : Shape
    {
        public void Draw()
        {
            WriteLine("Draw Circle");
        }
    }

    public enum ShapeType
    {
        Square,
        Circle
    }

    public class ShapeFactory
    {
        public Shape GetShape(ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.Square: 
                    return new Square();
                case ShapeType.Circle:
                    return new Circle();
                default:
                    return null;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ShapeFactory shapeFactory = new ShapeFactory();

            var mySquare = shapeFactory.GetShape(ShapeType.Square);
            mySquare.Draw();

            var myCircle = shapeFactory.GetShape(ShapeType.Circle);
            myCircle.Draw();
        }
    }
}
