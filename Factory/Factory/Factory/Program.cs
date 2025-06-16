using Factory;
using static System.Console;
namespace Factory 
{
   

    public class Point 
    {
        private double x;
        private double y;
       
        private  Point(double x, double y) 
        {
            this.x = x;
            this.y = y;
        }
        
        public override string ToString() 
        {
            return $"({x}, {y})";
        }
        public static class PointFactory
        {
            public static async Task<Point> CreatePolarPoint(double x, double y)
            {
                await Task.Delay(1000); // Simulate some asynchronous operation
                return new Point(x, y);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="raw"></param>
            /// <param name="theata"></param>
            /// <returns></returns>
            public static Point CreateRawPoint(double raw, double theata)
            {
                return new Point(raw * Math.Cos(theata), raw * Math.Sin(theata));
            }
        }

    }
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var point = await Point.PointFactory.CreatePolarPoint(1, 1);
        }
    }
}
