using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        public User(string firstName, string lastName, string drivingLicenceNumber)
        {
            FirstName= firstName;
            LastName= lastName; 
            DrivingLicenseNumber= drivingLicenceNumber;
        }
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.FirstNameNull);
                }
                firstName = value;
            }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LastNameNull);
                }
                lastName = value;
            }
        }

        private string drivingLicenseNumber;

        public string DrivingLicenseNumber
        {
            get { return drivingLicenseNumber; }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DrivingLicenseRequired);
                }
                drivingLicenseNumber = value;
            }
        }
        public double Rating { get; private set; }


        public bool IsBlocked { get; private set; }
        public void IncreaseRating()
        {
            if (Rating + 0.5 >=10)
            {
                Rating = 10;
            }
            Rating += 0.5;
        }
        public void DecreaseRating()
        {
            if (Rating - 2 <= 0)
            {
                Rating = 0;
                IsBlocked = true;
                return;
            }
            Rating -= 2;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} Driving license: {drivingLicenseNumber} Rating: {Rating}";
        }
    }
}
