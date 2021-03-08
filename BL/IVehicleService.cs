using System.Collections.Generic;

namespace BL
{
    public interface IVehicleService
    {
        void AddVehicle(VehicleRegistrationModel vehicleDTO);
        void DeleteVehicle(int Id);
        IEnumerable<VehicleDTO> GetAllData();
        VehicleDTO GetVehicle(int id);
        IEnumerable<VehicleDTO> GetVehiclesOnTrack();
        void UpdateVehicle(VehicleDTO vehicleDTO);
        ValidationResult ValidateVehicle(VehicleRegistrationModel vehicle);
    }
}