using static System.Console;
/*namespace AbstractFactory
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }
        private Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return $"Point({X}, {Y})";
        }
        public class PointFactory
        {
            public static List<(WeakReference<Point>, string)> Points = new();
            public static Point CreateCartesien(int x, int y)
            {

                var result = new Point(x, y);
                // Store a weak reference to the point
                Points.Add((new WeakReference<Point>(result), "Cartesian"));
                return result;
            }
            public static Point CreatePolar(double radius, double angleInRadians)
            {
                int x = (int)(radius * Math.Cos(angleInRadians));
                int y = (int)(radius * Math.Sin(angleInRadians));
                var result = new Point(x, y);
                Points.Add((new WeakReference<Point>(result), "Polar"));
                return result;
            }
            public static void PrintPoints()
            {
                WriteLine("Points created:");
                foreach (var (weakPoint, type) in Points)
                {
                    if (weakPoint.TryGetTarget(out Point point))
                    {
                        WriteLine($"{type}: {point}");
                    }
                    else
                    {
                        WriteLine($"{type}: Point has been garbage collected.");
                    }
                }
            }
        }
    }
    public abstract class AbstractFactory 
    {
        public abstract Point CreateCartesien(int x, int y);
        public abstract Point CreatePolar(double radius, double angleInRadians);
    }
        class Program
        {

            static void Main(string[] args)
            {

               Point. PointFactory.CreateCartesien(3, 4);
               Point. PointFactory.CreatePolar(5, Math.PI / 4);
               Point. PointFactory.PrintPoints();
                // Force garbage collection
                GC.Collect();
                GC.WaitForPendingFinalizers();
                // Print points again to see if any have been collected
               Point.PointFactory.PrintPoints();
            }
        }
}
*/
namespace AbstractFactory
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nice but I'd prefer it with milk.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffee is delicious!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        public enum AvailableDrink // violates open-closed
        {
            Coffee, Tea
        }

        private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
          new Dictionary<AvailableDrink, IHotDrinkFactory>();

        private List<Tuple<string, IHotDrinkFactory>> namedFactories =
          new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            //break OCP Principle by using an enum to define available drinks
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
              var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
              factories.Add(drink, factory);
                namedFactories.Add(Tuple.Create(
             factory.GetType().Name.Replace("Factory", string.Empty), factory));
            }
            // Alternatively, we can use reflection to find all factories

            //foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            //{
            //    if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
            //    {
            //        namedFactories.Add(Tuple.Create(
            //          t.Name.Replace("Factory", string.Empty), (IHotDrinkFactory)Activator.CreateInstance(t)));
            //    }
            //}
        }

        public IHotDrink MakeDrink()
        {
            WriteLine("Available drinks");
            for (var index = 0; index < namedFactories.Count; index++)
            {
                var tuple = namedFactories[index];
                WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s;
                if ((s = ReadLine()) != null
                    && int.TryParse(s, out int i) // c# 7
                    && i >= 0
                    && i < namedFactories.Count)
                {
                    Write("Specify amount: ");
                    s = ReadLine();
                    if (s != null
                        && int.TryParse(s, out int amount)
                        && amount > 0)
                    {
                        return namedFactories[i].Item2.Prepare(amount);
                    }
                }
                WriteLine("Incorrect input, try again.");
            }
        }

        //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        //{
        //  return factories[drink].Prepare(amount);
        //}
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 300);
            //drink.Consume();

            IHotDrink drink = machine.MakeDrink();
            drink.Consume();
        }
    }
}
