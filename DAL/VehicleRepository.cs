using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class VehicleRepository : IRepository<Vehicle>
    {
        private VehicleContext db;

        public VehicleRepository()
        {
            db = new VehicleContext();
        }

        public void Add(Vehicle item)
        {
            db.Vehicles.Add(item);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return db.Vehicles;
        }

        public Vehicle Get(int id)
        {
            return db.Vehicles.Find(id);
        }

        public void Update(Vehicle item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var vehicle = db.Vehicles.Find(id);
            if (vehicle != null)
            {
                db.Vehicles.Remove(vehicle);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
