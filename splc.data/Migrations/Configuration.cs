using System;
using System.Collections.Generic;
using splc.domain;
using System.Data.Entity;

namespace splc.data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BeholderContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BeholderContext context)
        {

            context.EyeColors.AddOrUpdate(a => a.Name,
                             new EyeColor() { Id = 1, Name = "Blue" },
                             new EyeColor() { Id = 2, Name = "Brown" },
                             new EyeColor() { Id = 3, Name = "Green" },
                             new EyeColor() { Id = 4, Name = "Purple" }
                             );

            context.HairColors.AddOrUpdate(a => a.Name,
                             new HairColor() { Id = 1, Name = "Brown" },
                             new HairColor() { Id = 2, Name = "Black" },
                             new HairColor() { Id = 3, Name = "Blond" },
                             new HairColor() { Id = 4, Name = "Green" }
                             );
            
            context.Prefixes.AddOrUpdate(p => p.Name,
                                         new Prefix() { Id = 1, Name = "Mr." },
                                         new Prefix() { Id = 2, Name = "Mrs." },
                                         new Prefix() { Id = 3, Name = "Ms." },
                                         new Prefix() { Id = 4, Name = "Dr." }
                                         );

            context.Suffixes.AddOrUpdate(s => s.Name,
                                         new Suffix() { Id = 1, Name = "Jr." },
                                         new Suffix() { Id = 2, Name = "Sr." },
                                         new Suffix() { Id = 3, Name = "II." },
                                         new Suffix() { Id = 4, Name = "III." }
                                         );

            context.States.AddOrUpdate(s => s.Id,
                                       new State { Id = 1, Name = "Alabama", StateCode = "AL" },
                                       new State { Id = 2, Name = "Georgia", StateCode = "GA" },
                                       new State { Id = 3, Name = "Mississippi", StateCode = "MS" },
                                       new State { Id = 4, Name = "Florida", StateCode = "FL" });


            context.Genders.AddOrUpdate(g => g.Id,
                                             new Gender() { Id = 1, Name = "M" },
                                             new Gender() { Id = 2, Name = "F" }
                );

            context.DriverLicenseTypes.AddOrUpdate(d => d.Id,
                                                   new DriverLicenseType { Id = 1, Name = "CDL" },
                                                   new DriverLicenseType { Id = 2, Name = "BOAT" },
                                                   new DriverLicenseType { Id = 3, Name = "CAR" },
                                                   new DriverLicenseType { Id = 4, Name = "PILOT" }
                );

            context.AddressTypes.AddOrUpdate(a => a.Id,
                                             new AddressType { Id = 1, Name = "Home" },
                                             new AddressType { Id = 2, Name = "Office" },
                                             new AddressType { Id = 3, Name = "Other" }
                );

            context.Races.AddOrUpdate(a => a.Id,
                                 new Race() { Id = 1, Name = "Black" },
                                 new Race() { Id = 2, Name = "White" },
                                 new Race() { Id = 3, Name = "Other" }
            );

            context.MaritialStatuses.AddOrUpdate(m => m.Id,
                                             new MaritialStatus() { Id = 1, Status = "Single" },
                                             new MaritialStatus() { Id = 2, Status = "Married" },
                                             new MaritialStatus() { Id = 3, Status = "Divorced" }
                );

            context.MovementClasses.AddOrUpdate(m => m.Id,
                                             new MovementClass() { Id = 1, Name = "Racial" },
                                             new MovementClass() { Id = 2, Name = "Gay Rights" },
                                             new MovementClass() { Id = 3, Name = "Unknown" }
                );

            context.ConfidentialityTypes.AddOrUpdate(c => c.Id,
                                 new ConfidentialityType() { Id = 1, Name = "Public" },
                                 new ConfidentialityType() { Id = 2, Name = "Secret" },
                                 new ConfidentialityType() { Id = 3, Name = "Top Secret"}
                );

            context.PhoneTypes.AddOrUpdate(a => a.Id,
                                 new PhoneType { Id = 1, Name = "Home" },
                                 new PhoneType { Id = 2, Name = "Office" },
                                 new PhoneType { Id = 3, Name = "Other" }
                );

            context.EmailTypes.AddOrUpdate(a => a.Id,
                     new EmailType { Id = 1, Name = "Home" },
                     new EmailType { Id = 2, Name = "Office" },
                     new EmailType { Id = 3, Name = "Other" }
    );

            context.OrganizationTypes.AddOrUpdate(a => a.Id,
                                 new OrganizationType { Id = 1, Name = "Political" },
                                 new OrganizationType { Id = 2, Name = "Racial" },
                                 new OrganizationType { Id = 3, Name = "Other" }
                );

            for (int i = 1; i < 20; i++)
            {
                context.Organizations.AddOrUpdate(r => r.Id,
                                                  new Organization()
                                                      {
                                                          Id = i,
                                                          Name = DataGenerator.Generate.RandomCompanyName(),
                                                          OrganizationTypeId = 1,
                                                          Addresses = new List<Address>()
                                                                          {
                                                                              new Address()
                                                                                  {
                                                                                      Address1 = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      City = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      StateId = DataGenerator.Generate.RandomInt(1,5), 
                                                                                      Zip4 = DataGenerator.Generate.RandomZipCode(), 
                                                                                      AddressTypeId = 1
                                                                                  },
                                                                                  new Address(){
                                                                                      Address1 = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      City = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      StateId = DataGenerator.Generate.RandomInt(1,5), 
                                                                                      Zip4 = DataGenerator.Generate.RandomZipCode(),
                                                                                      AddressTypeId = 2
                                                                                  },
                                                                                  new Address(){
                                                                                      Address1 = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      City = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      StateId = DataGenerator.Generate.RandomInt(1,5), 
                                                                                      Zip4 = DataGenerator.Generate.RandomZipCode(),
                                                                                      AddressTypeId = 3
                                                                                  }
                                                                          }
                                                      });

            }

            for (int j = 1; j < 51; j++)
            {
                context.People.AddOrUpdate(r => r.Id,
                                                  new Person()
                                                  {
                                                      Id = j,
                                                      FirstName = DataGenerator.Generate.RandomFirstName(),
                                                      LastName = DataGenerator.Generate.RandomLastName(),
                                                      DOB = RandomDay(),
                                                      GenderId = DataGenerator.Generate.RandomInt(1, 2),
                                                      Addresses = new List<Address>()
                                                                          {
                                                                              new Address()
                                                                                  {
                                                                                      Address1 = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      City = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      StateId = DataGenerator.Generate.RandomInt(1,5), 
                                                                                      Zip4 = DataGenerator.Generate.RandomZipCode(), 
                                                                                      AddressTypeId = 1
                                                                                  },
                                                                                  new Address(){
                                                                                      Address1 = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      City = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      StateId = DataGenerator.Generate.RandomInt(1,5), 
                                                                                      Zip4 = DataGenerator.Generate.RandomZipCode(),
                                                                                      AddressTypeId = 2
                                                                                  },
                                                                                  new Address(){
                                                                                      Address1 = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      City = DataGenerator.Generate.RandomStreetAddress(), 
                                                                                      StateId = DataGenerator.Generate.RandomInt(1,5), 
                                                                                      Zip4 = DataGenerator.Generate.RandomZipCode(),
                                                                                      AddressTypeId = 3
                                                                                  }
                                                                          }
                                                  });
            }

            for (int i = 1; i < 51; i++)
            {

                for (int j = 1; j < 4; j++)
                {
                    context.EmailAddresses.AddOrUpdate(a => a.Id,
                                                    new EmailAddress()
                                                        {
                                                            PersonId = i,
                                                            Email = DataGenerator.Generate.RandomEmailAddress(),
                                                            EmailTypeId = j
                                                        });
                }
            }

            for (int i = 0; i < 50; i++)
            {
                context.Memberships.AddOrUpdate(m => m.Id,
                                                    new Membership()
                                                    {
                                                        OrganizationId = DataGenerator.Generate.RandomInt(1, 20),
                                                        PersonId = DataGenerator.Generate.RandomInt(1, 50),
                                                        DateEnrolled = RandomDay(),
                                                    });
            }

        }

        DateTime RandomDay()
        {
            var start = new DateTime(1995, 1, 1);
            var gen = new Random();

            var range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

    }

}

