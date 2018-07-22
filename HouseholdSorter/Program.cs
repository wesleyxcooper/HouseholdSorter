using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdSorter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<string[]> People = new List<string[]>();
            List<Person> SortedPeople = new List<Person>();
            List<string> UniqueAddress = new List<string>();
            Dictionary<string, int> household = new Dictionary<string, int>();

            GetPeople(People);
            Console.WriteLine("===Raw Data===");
            People.ForEach(PrintAll);
            GetAddresses(People, UniqueAddress, household);

            Console.WriteLine("\n===Households===");
            UniqueAddress.ForEach(delegate (String Address)
            {
                Console.WriteLine($"{Address} - {household[Address]} occupants");
            });

            List<Person> SortedList = OrganizePeople(People, SortedPeople);
            Console.WriteLine("\n===Eligible===");
            SortedList.ForEach(PrintList);
            Console.ReadKey();
        }


        private static List<Person> OrganizePeople(List<string[]> People, List<Person> SortedPeople)
        {
            foreach (var p in People)
            {
                Person person = new Person(p[0], p[1], p[2], p[3], p[4], p[5]);
                SortedPeople.Add(person);
            }
            var SortedList = SortedPeople.OrderBy(x => x.lastname).ThenBy(x => x.firstname).ToList();
            return SortedList;
        }

        private static void GetAddresses(List<string[]> People, List<string> UniqueAddress, Dictionary<string, int> household)
        {
            foreach (var p in People)
            {
                string address = string.Join(" ", p[2].ToLower(), p[3].ToLower(), p[4].ToLower());

                if (address.Contains(",") || address.Contains("."))
                {
                    address = address.Replace(",", "");
                    address = address.Replace(".", "");
                }
                address = address.Replace("  ", " ");
                address = address.Trim();

                var exists = UniqueAddress.Any(x => string.Compare(x, address, StringComparison.OrdinalIgnoreCase) == 0);

                int occupants = 1;
                
                if (!exists)
                {
                    UniqueAddress.Add(address);
                    int thisOccupants = occupants;
                    household.Add(address, thisOccupants);
                    //Console.WriteLine($"index = {address} occupants = {household[address]}");
                }
                else
                {
                    int thisOccupants = occupants + 1;
                    household[address]++;
                    //Console.WriteLine($"index = {address} occupants = {household[address]}");
                }
            }
        }

        private static void PrintList(Person p)
        {
            if (int.Parse(p.age) > 18)
                Console.WriteLine($"{p.firstname} {p.lastname}, {p.street}, {p.city}, {p.state}");
        }

        private static void GetPeople(List<string[]> People)
        {
            string[] person1 = { "Dave", "Smith", "123 main st.", "seattle", "wa", "43" };
            string[] person2 = { "Alice", "Smith", "123 Main St.", "Seattle", "WA", "45" };
            string[] person3 = { "Bob", "Williams", "234 2nd Ave.", "Tacoma", "WA", "26" };
            string[] person4 = { "Carol", "Johnson", "234 2nd Ave", "Seattle", "WA", "67" };
            string[] person5 = { "Eve", "Smith", "234 2nd Ave.", "Tacoma", "WA", "25" };
            string[] person6 = { "Frank", "Jones", "234 2nd Ave.", "Tacoma", "FL", "23" };
            string[] person7 = { "George", "Brown", "345 3rd Blvd., Apt. 200", "Seattle", "WA", "18" };
            string[] person8 = { "Helen", "Brown", "345 3rd Blvd. Apt. 200", "Seattle", "WA", "18" };
            string[] person9 = { "Ian", "Smith", "123 main st ", "Seattle", "Wa", "18" };
            string[] person10 = { "Jane", "Smith", "123 Main St.", "Seattle", "WA", "13" };
            //string[] person11 = { "Wesley", "Cooper", "123 Main St.", "Seattle", "WA", "26" };
            People.Add(person1);
            People.Add(person2);
            People.Add(person3);
            People.Add(person4);
            People.Add(person5);
            People.Add(person6);
            People.Add(person7);
            People.Add(person8);
            People.Add(person9);
            People.Add(person10);
            //People.Add(person11);
        }
        private static void PrintAll(string[] obj)
        {
            Console.WriteLine(string.Join(" ", obj[0], obj[1], obj[2], obj[3], obj[4], obj[5]));
        }
    }
}

public class Person
{
    public string firstname;
    public string lastname;
    public string street;
    public string city;
    public string state;
    public string age;

    public Person(string firstname, string lastname, string street, string city, string state, string age)
    {
        this.firstname = firstname;
        this.lastname = lastname;
        this.street = street;
        this.city = city;
        this.state = state;
        this.age = age;
    }
}