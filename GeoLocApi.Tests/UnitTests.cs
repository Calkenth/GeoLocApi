using GeoLocApi.Controllers;
using GeoLocApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject2
{
    public class UnitTests
    {
        [Fact]
        public void GetAllGeoLocationsSHouldReturnSuccess()
        {
            GeoLocation FirstGeoLoc = new GeoLocation("192.168.1.1", "Ochota", "Poland", "Europe")
            {
                ID = 1
            };
            GeoLocation SecondGeoLoc = new GeoLocation("134.201.250.155", "Los Angeles", "USA", "North America")
            {
                ID = 2
            };
            var builder = new DbContextOptionsBuilder<GeoLocApiContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            using var context = new GeoLocApiContext(builder.Options);
            context.GeoLocations.Add(FirstGeoLoc);
            context.GeoLocations.Add(SecondGeoLoc);
            context.SaveChanges();

            var controller = new GeoLocationsController(context);
            var task = controller.GetGeoLocation();
            Assert.True(task.IsCompletedSuccessfully);
        }
        [Fact]
        public void GetGeoLocationShouldreturnSuccess()
        {
            GeoLocation FirstGeoLoc = new GeoLocation("192.168.1.1", "Ochota", "Poland", "Europe")
            {
                ID = 1
            };
            GeoLocation SecondGeoLoc = new GeoLocation("134.201.250.155", "Los Angeles", "USA", "North America")
            {
                ID = 2
            };
            var builder = new DbContextOptionsBuilder<GeoLocApiContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            using var context = new GeoLocApiContext(builder.Options);
            context.GeoLocations.Add(FirstGeoLoc);
            context.GeoLocations.Add(SecondGeoLoc);
            context.SaveChanges();

            var controller = new GeoLocationsController(context);
            Task<IActionResult> task = controller.GetGeoLocation("192.168.1.1");
            Assert.True(task.IsCompletedSuccessfully);
        }
        [Fact]
        public async Task DeleteShouldReturnTrueFor1()
        {
            GeoLocation FirstGeoLoc = new GeoLocation("192.168.1.1", "Ochota", "Poland", "Europe")
            {
                ID = 1
            };
            GeoLocation SecondGeoLoc = new GeoLocation("134.201.250.155", "Los Angeles", "USA", "North America")
            {
                ID = 2
            };
            var builder = new DbContextOptionsBuilder<GeoLocApiContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            using var context = new GeoLocApiContext(builder.Options);
            context.GeoLocations.Add(FirstGeoLoc);
            context.GeoLocations.Add(SecondGeoLoc);
            context.SaveChanges();

            var controller = new GeoLocationsController(context);
            var taski = controller.DeleteGeoLocation("192.168.1.1");

            int num = await context.GeoLocations.CountAsync();
            Assert.Equal(1, num);
        }
        [Fact]
        public async Task PostShouldReturnTrueFor2()
        {
            GeoLocation FirstGeoLoc = new GeoLocation("192.168.1.1", "Ochota", "Poland", "Europe")
            {
                ID = 1
            };
            GeoLocation SecondGeoLoc = new GeoLocation("134.201.250.155", "Los Angeles", "USA", "North America")
            {
                ID = 2
            };
            var builder = new DbContextOptionsBuilder<GeoLocApiContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            using var context = new GeoLocApiContext(builder.Options);
            context.GeoLocations.Add(FirstGeoLoc);
            context.SaveChanges();

            var controller = new GeoLocationsController(context);
            await controller.PostGeoLocation(new InputModel() { ip = "134.201.250.155" });
            int num = await context.GeoLocations.CountAsync();

            Assert.Equal(2, num);
        }
    }
}
