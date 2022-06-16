using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    //  Index se používá tam kde se podle property vyhledává (zrychlení)
    [Index(nameof(Email))]
    public class Person
    {
        #region konstruktory
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Person()
        {

        }
        #endregion

        #region vlastnosti

        public int Id { get; set; }

        [MaxLength(250)]    //Datová anotace, omezení délky stringu
        public string FirstName { get; set; } = "John";

        [MaxLength(250)]
        public string LastName { get; set; } = "Doe";

        public string FullName
        {
            get
            {
                return GetFullName();
            }
        }

        public DateTime DateOfBirth { get; set; }


        //  Date only nebude v DB a vytváříme ji jen proto že dejme tomu potřebujeme získat v nějaké jiné komponentě hodnotu typu DateOnly, Je to taková virtuální vlastnost
        [NotMapped]
        public DateOnly DateOfBirthDateOnly
        {
            get => DateOnly.FromDateTime(DateOfBirth);  //  Získám DateOnly z data narození který je typu DateTime
            private set => DateOfBirth = value.ToDateTime(new TimeOnly(0)); //  nastavím Datum narození pro DateOfBirth tak aby se to zapsalo na vlastnost DateOfBirth 
        }


        public Address HomeAddress { get; set; } = new Address();

        [MaxLength(270)]
        [Required]  // Povinný údaj
        public string Email { get; set; }

        public List<Contract> Contracts { get; set; }
                                    = new List<Contract>();
        #endregion

        #region metody
        public override string ToString()
            => $"{FirstName} {LastName}";


        public int Age()
        {
            DateTime today = DateTime.Today;

            int age = today.Year - DateOfBirth.Year;

            if (today.Month < DateOfBirth.Month ||
           ((today.Month == DateOfBirth.Month) && (today.Day < DateOfBirth.Day)))
            {
                age--;  //birthday in current year not yet reached, we are 1 year younger ;)
            }

            return age;
        }

        public string GetFullName()
            => $"{FirstName} {LastName}";

        #endregion

    }
}