using System;
using System.Collections.Generic;
using System.Text;
using VidRental.DataAccess.DbModels;
using VidRental.Services.Services;
using Xunit;

namespace VidRental.Tests
{
    public class UserRent
    {
        [Fact]
        public void CanRent_ToManyRentals_False()
        {
            var rentals = new List<Rental>
            {
                new Rental(),
                new Rental(),
                new Rental()
            };

            var result = ShopUserService.CanRent(rentals);
            Assert.False(result);
        }

        [Fact]
        public void CanRent_OutDatedRental_False()
        {
            var rentals = new List<Rental>
            {
                new Rental{ Rented = new DateTime(1999,1,1) },
            };

            var result = ShopUserService.CanRent(rentals);
            Assert.False(result);
        }

        [Fact]
        public void CanRent_OutDatedRentalWMixedRentals_False()
        {
            var rentals = new List<Rental>
            {
                new Rental{ Rented = new DateTime(1999,1,1) },
                new Rental(),
            };

            var result = ShopUserService.CanRent(rentals);
            Assert.False(result);
        }

        [Fact]
        public void CanRent_TooManyOutdatedRentals_False()
        {
            var rentals = new List<Rental>
            {
                new Rental{ Rented = new DateTime(1999,1,1) },
                new Rental{ Rented = new DateTime(1999,1,1) },
                new Rental{ Rented = new DateTime(1999,1,1) }
            };

            var result = ShopUserService.CanRent(rentals);
            Assert.False(result);
        }

        [Fact]
        public void CanRent_TwoRentals_True()
        {
            var rentals = new List<Rental>
            {
                new Rental{ Rented = new DateTime(3000,1,1) },
                new Rental{ Rented = new DateTime(3000,1,1) }
            };

            var result = ShopUserService.CanRent(rentals);
            Assert.True(result);
        }

    }
}
