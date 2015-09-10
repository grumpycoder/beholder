using System.Collections.Generic;
using System.Data.Entity;
using splc.domain;

namespace splc.data
{
    public class BeholderDbInitializer : DropCreateDatabaseAlways<BeholderContext>
    {
        protected override void Seed(BeholderContext context)
        {
            //var prefxes = new List<Prefix>
            //                  {
            //                      new Prefix{ PrefixName = "Mr."},
            //                      new Prefix{ PrefixName = "Mrs."},
            //                      new Prefix{ PrefixName = "Dr."}
            //                  };
            //prefxes.ForEach(s => context.Prefixes.Add(s));
            //context.SaveChanges();

            //var suffixes = new List<Suffix>
            //                   {
            //                       new Suffix{ SuffixName = "Jr."},
            //                       new Suffix{ SuffixName = "Sr."},
            //                       new Suffix{ SuffixName = "III."}
            //                   };
            //suffixes.ForEach(s => context.Suffixes.Add(s));
            //context.SaveChanges();

            //var states = new List<State>
            //                 {
            //                     new State{ Id = 1, StateCode = "AL"},
            //                     new State{ Id = 2, StateCode = "MS"},
            //                     new State{ Id = 3, StateCode = "GA"}
            //                 };
            //states.ForEach(s => context.States.Add(s));
            //context.SaveChanges();

            //var people = new List<Person>
            //                   {
            //                       new Person{ FirstName = "John", LastName = "Doe", Sex = "M"},
            //                       new Person{ FirstName = "Jane", LastName = "Doe", Sex = "F"},
            //                       new Person{ FirstName = "Chris", LastName = "Smith", Sex = "M", 
            //                       Addresses =  new List<Address>
            //                           {
            //                               new Address{Address1 = "1 Main Street", City = "Montgomery", StateId = 1, Zip4 = "12345"},
            //                                new Address{Address1 = "2 Chruch Street", City = "Montgomery", StateId = 1, Zip4 = "12345"}
            //                           }}
            //                   };
            //people.ForEach(s => context.People.Add(s));
            //context.SaveChanges();
        }


    }
}
//, Addresses = new List<Address>
//                                                                                                                   {
//                                                                                                                       new Address{Address1 = "1 Main Street", City = "Selma", StateId = 1, Zip4 = "12345"}

//new Person{ FirstName = "John", LastName = "Doe", SSN = "111223333", Sex = "M"},
//new Person{ FirstName = "Jane", LastName = "Doe", SSN = "111223333", Sex = "F"},
//new Person{ FirstName = "Chris", LastName = "Smith", SSN = "111223333", Sex = "M"},