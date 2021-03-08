using AutoMapper;
using BL;
using RaceTrack.Common;
using RaceTrack.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RaceTrack.Controllers
{
    public class RaceManagementController : Controller
    {
        IVehicleService service;
        IMapper dtoToViewMapper;

        public RaceManagementController(IVehicleService service)
        {
            this.service = service;
            dtoToViewMapper = new MapperConfiguration(cfg => cfg.CreateMap<VehicleDTO, VehicleViewModel>()).CreateMapper();
        }

        public ActionResult Index()
        {
            var vehicles = service.GetAllData();
            var viewModel = dtoToViewMapper.Map<IEnumerable<VehicleViewModel>>(vehicles);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(VehicleRegistrationModel vehicle)
        {
            var validationResult = service.ValidateVehicle(vehicle);

            if (ModelState.IsValid && validationResult.IsSuccess)
            {
                service.AddVehicle(vehicle);
                return RedirectToAction(nameof(Index));
            }

            foreach (var item in validationResult.Errors)
            {
                ModelState.AddModelError(item.Name, item.Message);
            }

            return View(vehicle);
        }

        public ActionResult Edit(int id)
        {
            var vehicleDto = service.GetVehicle(id);
            var vehicle = dtoToViewMapper.Map<VehicleViewModel>(vehicleDto);

            return View(vehicle);
        }

        [HttpPost]
        public ActionResult Edit(VehicleViewModel vehicle)
        {
            if (vehicle.Year > DateTime.Now.Year || vehicle.Year < DateTime.Now.Year - Constants.MaxVehicleAgeAllowed)
            {
                ModelState.AddModelError(nameof(vehicle.Year), "Incorrect value");
            }

            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<VehicleViewModel, VehicleDTO>()).CreateMapper();
                var vehicleDto = mapper.Map<VehicleDTO>(vehicle);
                service.UpdateVehicle(vehicleDto);

                return RedirectToAction(nameof(Index));
            }

            return View(vehicle);
        }

        public ActionResult Delete(int id)
        {
            service.DeleteVehicle(id);
            return RedirectToAction(nameof(Index));
        }
    }
}