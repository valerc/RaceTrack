using AutoMapper;
using DAL;
using RaceTrack.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class VehicleService : IVehicleService
    {
        IRepository<Vehicle> repository;
        IMapper DbToDtoMapper;
        Random random;
        public VehicleService(IRepository<Vehicle> repository)
        {
            this.repository = repository;
            DbToDtoMapper = new MapperConfiguration(cfg => cfg.CreateMap<Vehicle, VehicleDTO>()).CreateMapper();
            random = new Random();
        }

        public IEnumerable<VehicleDTO> GetAllData()
        {
            var vehicles = repository.GetAll();
            return DbToDtoMapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleDTO>>(vehicles);
        }

        public VehicleDTO GetVehicle(int id)
        {
            var vehicle = repository.Get(id);
            return DbToDtoMapper.Map<Vehicle, VehicleDTO>(vehicle);
        }

        public IEnumerable<VehicleDTO> GetVehiclesOnTrack()
        {
            var vehicles = repository.GetAll()
                .OrderBy(x => random.Next())
                .Take(5)
                .OrderBy(x => x.TeamName)
                .ThenBy(x => x.Make);

            return DbToDtoMapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleDTO>>(vehicles);
        }

        public void AddVehicle(VehicleRegistrationModel vehicleDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<VehicleRegistrationModel, Vehicle>()).CreateMapper();
            var vehicle = mapper.Map<VehicleRegistrationModel, Vehicle>(vehicleDTO);
            repository.Add(vehicle);
            repository.Save();
        }

        public void UpdateVehicle(VehicleDTO vehicleDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<VehicleDTO, Vehicle>()).CreateMapper();
            var vehicle = mapper.Map<VehicleDTO, Vehicle>(vehicleDTO);
            repository.Update(vehicle);
            repository.Save();
        }

        public void DeleteVehicle(int Id)
        {
            repository.Delete(Id);
            repository.Save();
        }

        public ValidationResult ValidateVehicle(VehicleRegistrationModel vehicle)
        {
            var result = new ValidationResult();

            if (!vehicle.IsTowStrapAvailable)
            {
                result.Errors.Add(new PropertyError(nameof(vehicle.IsTowStrapAvailable), "Vehicle must have a tow strap!"));
            }

            if (vehicle.Year > DateTime.Now.Year || vehicle.Year < DateTime.Now.Year - Constants.MaxVehicleAgeAllowed)
            {
                result.Errors.Add(new PropertyError(nameof(vehicle.Year), "Incorrect value"));
            }

            if (vehicle.LiftingHeight < 0 || vehicle.LiftingHeight > 100)
            {
                result.Errors.Add(new PropertyError(nameof(vehicle.LiftingHeight), "Incorrect value"));
            }

            if (vehicle.TireWear < 0 || vehicle.TireWear > 100)
            {
                result.Errors.Add(new PropertyError(nameof(vehicle.TireWear), "Incorrect value"));
            }

            if (vehicle.VehicleType == VehicleType.Car)
            {
                if (vehicle.TireWear >= 85)
                {
                    result.Errors.Add(new PropertyError(nameof(vehicle.TireWear), "Tire wear must be less than 85%!"));
                }
            }
            else if (vehicle.VehicleType == VehicleType.Truck)
            {
                if (vehicle.LiftingHeight > 5)
                {
                    result.Errors.Add(new PropertyError(nameof(vehicle.LiftingHeight), "Vehicle lifting more than 5'' is not allowed!"));
                }
            }

            return result;
        }
    }
}
