using splc.domain;
using System.Data.Entity;

namespace splc.data
{
    public class BeholderContext : DbContext
    {

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Alias> Aliases { get; set; }
        public DbSet<AttributeColor> Colors { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<ConfidentialityType> ConfidentialityTypes { get; set; }
        public DbSet<DriverLicenseType> DriverLicenseTypes { get; set; }

        public DbSet<EmailAddress> EmailAddresses { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<MaritialStatus> MaritialStatuses { get; set; }

        public DbSet<Membership> Memberships { get; set; }

        public DbSet<MovementClass> MovementClasses { get; set; }

        public DbSet<OnlineIdentity> OnlineIdentities { get; set; }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }

        public DbSet<Prefix> Prefixes { get; set; }
        public DbSet<Suffix> Suffixes { get; set; }

        public DbSet<Race> Races { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<HairColor> HairColors { get; set; }
        public DbSet<EyeColor> EyeColors { get; set; }

        public BeholderContext():base("DefaultConnection")
        {

        }

    }
}

