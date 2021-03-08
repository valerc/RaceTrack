using AutoMapper;
using BL;
using RaceTrack.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RaceTrack.Controllers
{
    public class HomeController : Controller
    {
        IVehicleService service;

        public HomeController(IVehicleService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            var vehicles = service.GetVehiclesOnTrack();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<VehicleDTO, VehicleViewModel>()).CreateMapper();
            var viewModel = mapper.Map<IEnumerable<VehicleViewModel>>(vehicles);

            return View(viewModel);
        }
    }
}
