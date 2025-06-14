using static System.Console;
namespace FunctionalBuilder 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var person = new PersonAddressBuilder()
                .WithAddress("")
                .WithAge(30)
                .WithName("John Doe")
                .Build();
            WriteLine($"Name: {person.Name}, Age: {person.Age}, Address: {person.Address}");
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
    public abstract class PersonBuilder<TSubject,TFunctionalBuilder>
        where TSubject : Person, new()
        where TFunctionalBuilder : PersonBuilder<TSubject, TFunctionalBuilder>, new()
    {
        protected TSubject _subject = new TSubject();
       private List<Func<Person, Person>> _actions = new List<Func<Person, Person>>();
       public TFunctionalBuilder AddAction(Func<Person, Person> action)
        {
            _actions.Add(p=>action(p));
            return (TFunctionalBuilder)this;
        }
        public Person Build()
        {
            var person = new Person();
            foreach (var action in _actions)
            {
                action(person);
            }
            return person;
        }
    }

    public class PersonNameBuilder : PersonBuilder<Person, PersonNameBuilder>
    {
       
        public PersonNameBuilder WithName(string name)
        {
             AddAction(p => { p.Name = name; return p; });
            return this;
        }
    }
    public class PersonAgeBuilder : PersonNameBuilder    {
        public PersonAgeBuilder WithAge(int age)
        {
               AddAction(p => { p.Age = age; return p; });
            return this;
        }
    }
    public class PersonAddressBuilder : PersonAgeBuilder
    {
        public PersonAddressBuilder WithAddress(string address)
        {
            AddAction(p => { p.Address = address; return p; });
            return this;
        }
    }

}


namespace DotNetDesignPatternDemos.Creational.Builder
{
    public class Person
    {
        public string Name, Position;
      public override string ToString()
        {
            return $"{Name} works as {Position}";
        }
    }

    public sealed class PersonBuilder
    {
        public readonly List<Action<Person>> Actions
          = new List<Action<Person>>();

        public PersonBuilder Called(string name)
        {
            Actions.Add(p =>  p.Name = name );
            return this;
        }

        public Person Build()
        {
            var p = new Person();
            Actions.ForEach(a => a(p));
            return p;
        }
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAsA
          (this PersonBuilder builder, string position)
        {
            builder.Actions.Add(p =>
            {
                p.Position = position;
            });
            return builder;
        }
    }

    public class FunctionalBuilder
    {
        public static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            var person = pb.Called("Dmitri").WorksAsA("Programmer").Build();
            WriteLine(person);
        }
    }
}