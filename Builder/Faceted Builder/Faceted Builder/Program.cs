using static System.Console;
namespace FacetedBuilder 
{
    public class Person
    {

        //job
        public string Position { get; set; }    
        public string Company { get; set; }
        public double AnnualSalary { get; set; }
        //address
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Person() 
        { }
        public override string ToString()
        {
            return $"Position: {Position}, Company: {Company}, Annual Salary: {AnnualSalary}, " +
                   $"Address: {Street}, {City}, {State}, {ZipCode}";
        }

    }
    public class PersonBuilder
    {
        protected Person _person = new();
        public PersonBuilder()
        {
            _person = new Person();
        }

        public PersonJobBuilder Works => new(_person);
        public PersonAddressBuilder Lives => new(_person);
        public static implicit operator Person(PersonBuilder builder)
        {
            return builder._person;
        }
        public Person Build()
        {
            return _person;
        }
    }
    public class PersonJobBuilder : PersonBuilder
    {
       
        public PersonJobBuilder(Person person) {
           this. _person = person;
        }
        public PersonJobBuilder WorksAs(string position)
        {
            _person.Position = position;
            return this;
        }
        public PersonJobBuilder At(string company)
        {
            _person.Company = company;
            return this;
        }
        public PersonJobBuilder Earning(double annualSalary)
        {
            _person.AnnualSalary = annualSalary;
            return this;
        }
    }
    public class PersonAddressBuilder : PersonBuilder
    {
       
        public PersonAddressBuilder(Person person) 
        {
          this.  _person=person;
        }
        public PersonAddressBuilder LivesAt(string street)
        {
            _person.Street = street;
            return this;
        }
        public PersonAddressBuilder In(string city)
        {
            _person.City = city;
            return this;
        }
        public PersonAddressBuilder WithState(string state)
        {
            _person.State = state;
            return this;
        }
        public PersonAddressBuilder WithZipCode(string zipCode)
        {
            _person.ZipCode = zipCode;
            return this;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new PersonBuilder();
            Person person = builder
                .Works
                    .WorksAs("Software Engineer")
                    .At("Tech Company")
                    .Earning(100000)
                .Lives
                    .LivesAt("123 Main St")
                    .In("Springfield")
                    .WithState("IL")
                    .WithZipCode("62701").Build()
                ;
            WriteLine(person); // Output: Hello World!
        }
    }
}