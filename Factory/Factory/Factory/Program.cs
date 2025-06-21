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
            /// <summary>
            /// Creates a point using polar coordinates.
            /// </summary>
            /// <param name="radius">The radius from the origin.</param>
            /// <param name="theta">The angle in radians.</param>
            public static async Task<Point> CreatePolarPoint(double radius, double theta)
            {
                await Task.Delay(1000); // Simulate some asynchronous operation
                return new Point(radius * Math.Cos(theta), radius * Math.Sin(theta));
            }

            /// <summary>
            /// Synchronously creates a point using polar coordinates.
            /// </summary>
            /// <param name="radius">The radius from the origin.</param>
            /// <param name="theta">The angle in radians.</param>
            public static Point CreateRawPoint(double radius, double theta)
            {
                return new Point(radius * Math.Cos(theta), radius * Math.Sin(theta));
            }
        }

    }
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var point = await Point.PointFactory.CreatePolarPoint(1, 1);
            WriteLine(point);
        }
    }
}
