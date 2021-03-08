using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BL;
using DAL;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceTrack.Common;
using RaceTrack.Controllers;
using RaceTrack.Models;

namespace RaceTrackTest
{
    [TestClass]
    public class RaceTrackTests
    {
        [TestMethod]
        public void VehiclesOnTrackLimit()
        {
            var vehicles = new List<Vehicle>(10);
            for (int i = 0; i < vehicles.Capacity; i++)
            {
                vehicles.Add(new Vehicle());
            }

            var fakeRepository = A.Fake<IRepository<Vehicle>>();
            A.CallTo(() => fakeRepository.GetAll()).Returns(vehicles);

            var vehicleService = new VehicleService(fakeRepository);
            var controller = new HomeController(vehicleService);

            var result = controller.Index() as ViewResult;
            var vehiclesCount = ((IEnumerable<VehicleViewModel>)result.Model).Count();

            Assert.AreEqual(5, vehiclesCount, "Only 5 vehicles maximum can be on the race-track");
        }

        [TestMethod]
        public void AddVehiclesThatMeetRequirements()
        {
            var car = new VehicleRegistrationModel
            {
                TeamName = "Test1",
                VehicleType = VehicleType.Car,
                Make = "Test",
                Model = "Test",
                IsTowStrapAvailable = true,
                LiftingHeight = 0,
                TireWear = 0,
                Year = DateTime.Now.Year
            };

            var truck = new VehicleRegistrationModel
            {
                TeamName = "Test2",
                VehicleType = VehicleType.Truck,
                Make = "Test",
                Model = "Test",
                IsTowStrapAvailable = true,
                LiftingHeight = 0,
                TireWear = 0,
                Year = DateTime.Now.Year
            };

            var fakeRepository = A.Fake<IRepository<Vehicle>>();
            var vehicleService = new VehicleService(fakeRepository);
            var controller = new RaceManagementController(vehicleService);

            controller.Create(car);
            controller.Create(truck);

            A.CallTo(() => fakeRepository.Add(A<Vehicle>._)).MustHaveHappenedTwiceExactly();
        }

        [TestMethod]
        public void AddVehicleWithoutTowStrap()
        {
            var car = new VehicleRegistrationModel
            {
                TeamName = "Test",
                VehicleType = VehicleType.Car,
                Make = "Test",
                Model = "Test",
                IsTowStrapAvailable = false,
                LiftingHeight = 0,
                TireWear = 0,
                Year = DateTime.Now.Year
            };

            var fakeRepository = A.Fake<IRepository<Vehicle>>();
            var vehicleService = new VehicleService(fakeRepository);
            var controller = new RaceManagementController(vehicleService);

            controller.Create(car);

            A.CallTo(() => fakeRepository.Add(A<Vehicle>._)).MustNotHaveHappened();
        }

        [TestMethod]
        public void AddCarWithHighTireWear()
        {
            var car = new VehicleRegistrationModel
            {
                TeamName = "Test",
                VehicleType = VehicleType.Car,
                Make = "Test",
                Model = "Test",
                IsTowStrapAvailable = true,
                LiftingHeight = 0,
                TireWear = 85,
                Year = DateTime.Now.Year
            };

            var fakeRepository = A.Fake<IRepository<Vehicle>>();
            var vehicleService = new VehicleService(fakeRepository);
            var controller = new RaceManagementController(vehicleService);

            controller.Create(car);

            A.CallTo(() => fakeRepository.Add(A<Vehicle>._)).MustNotHaveHappened();
        }

        [TestMethod]
        public void AddTruckWithHightLifting()
        {
            var car = new VehicleRegistrationModel
            {
                TeamName = "Test",
                VehicleType = VehicleType.Truck,
                Make = "Test",
                Model = "Test",
                IsTowStrapAvailable = true,
                LiftingHeight = 5.1,
                TireWear = 0,
                Year = DateTime.Now.Year
            };

            var fakeRepository = A.Fake<IRepository<Vehicle>>();
            var vehicleService = new VehicleService(fakeRepository);
            var controller = new RaceManagementController(vehicleService);

            controller.Create(car);

            A.CallTo(() => fakeRepository.Add(A<Vehicle>._)).MustNotHaveHappened();
        }
    }
}
