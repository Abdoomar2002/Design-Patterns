using static System.Console;
namespace Stepwise_Builder 
{
    public enum CarType 
    {
        Sedan,SUV,Truck
    }
    public class Car 
    {
        public CarType CarType;
        public int WheelSize;
        public override string ToString() 
        {
            return $"Car Type: {CarType}, Wheel Size: {WheelSize} inches";
        }
    }
    public class CarBuilder 
    {
        private class Impl : ISpecficationCarType, ISpeceficationWheelSize, IBuildCar 
        {
            private Car _car = new Car();
            public ISpeceficationWheelSize OfType(CarType type) 
            {
                _car.CarType = type;
                return this;
            }
            public IBuildCar WheelSize(int size) 
            {
                //conditional
                if (size < 15 || size > 22) 
                {
                    throw new ArgumentOutOfRangeException(nameof(size), "Wheel size must be between 15 and 22 inches.");
                }
                if (_car.CarType == CarType.Truck && size < 18) 
                {
                    throw new ArgumentException("Truck wheel size must be at least 18 inches.");
                }
                if (_car.CarType == CarType.SUV && size < 16) 
                {
                    throw new ArgumentException("SUV wheel size must be at least 16 inches.");
                }if (_car.CarType == CarType.Sedan && size < 15) 
                {
                    throw new ArgumentException("Sedan wheel size must be at least 15 inches.");
                }
                _car.WheelSize = size;
                return this;
            }
            public Car Build() 
            {
                return _car;
            }
        }
        public static ISpecficationCarType Create() 
        {
            return new Impl();
        }
    }
    public interface ISpecficationCarType 
    {
        public ISpeceficationWheelSize OfType(CarType type);
    }
    public interface ISpeceficationWheelSize 
    {
        public IBuildCar WheelSize(int size);
    }
    public interface IBuildCar 
    {
        public Car Build();
    }
    public class Program 
    {
        public static void Main(string[] args) 
        {
            var car = CarBuilder.Create().OfType(CarType.SUV).WheelSize(10).Build();
            WriteLine(car);
        }
    }
}