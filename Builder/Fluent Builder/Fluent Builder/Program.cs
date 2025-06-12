using static System.Console;
namespace Fluent_Builder
{
    public class Person
    {
        public string FullName;

        public int Age;
        public string Email;
        public class Builder :PersonEmailBuilder<Builder> { }
        public static Builder New=> new Builder();
        public override string ToString()
        {
            return $"{FullName}, Age: {Age}, Email: {Email}";
        }
    }
    public abstract class PersonBuilder
    {
        protected Person _person = new Person();
        public Person Build()
        {
            return _person;
        }

    }
    public class PersonNameBuider<SELF>: PersonBuilder where SELF : PersonNameBuider<SELF>
    {

        public SELF WithName(string fullName)
        {
            _person.FullName = fullName;
            return (SELF)this;
        }
    }
    public class PersonAgeBuilder<SELF> : PersonNameBuider<PersonAgeBuilder<SELF>> where SELF : PersonAgeBuilder<SELF>
    {
        public SELF WithAge(int age)
        {
            _person.Age = age;
            return (SELF)this;
        }
    }
    public class PersonEmailBuilder<SELF> : PersonAgeBuilder<PersonEmailBuilder<SELF>> where SELF : PersonEmailBuilder<SELF>
    {
        public SELF WithEmail(string email)
        {
            _person.Email = email;
            return (SELF)this;
        }
    }
    /*  public class PersonBuilder
      {
          private readonly Person _person = new();
          public PersonBuilder WithFirstName(string firstName)
          {
              _person.FirstName = firstName;
              return this;
          }
          public PersonBuilder WithLastName(string lastName)
          {
              _person.LastName = lastName;
              return this;
          }
          public PersonBuilder WithAge(int age)
          {
              _person.Age = age;
              return this;
          }
          public PersonBuilder WithEmail(string email)
          {
              _person.Email = email;
              return this;
          }
          public Person Build()
          {
              return _person;
          }
      }
    */
    public class Program
    {
        public static void Main()
        {
            var person = Person.New
                .WithName("abdo20omar20@gmail.com")
                .WithAge(30)
                .WithEmail("John Doe")
                .Build();
            WriteLine(person);

        }
    }
}
// class parent<SELF> where SELF : Parent<SELF>
// class Child: Parent<Child>
/*
 function in parent class
function in child class
object from child class 
object child call function in parent class then return object from parent class

 using SELF make the function in the parent class return object from child class 
 
 */