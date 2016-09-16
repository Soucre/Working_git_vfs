using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public interface IVehicleRepository
    {
        void ClearCache();
        IEnumerable<string> GetVehicles();
    }
    public class VehicleRepository : IVehicleRepository
    {
        public IList<string> DataContext { get; set; }
        public ICacheProvider CacheProvider { get; set; }

        public VehicleRepository()
            : this(new DefaultCacheProvider()) {
        }

        public VehicleRepository(ICacheProvider cacheProvider) {
            this.DataContext = new List<string> {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "Time" };
            this.CacheProvider = cacheProvider;
        }


        public void ClearCache() {
            CacheProvider.Invalidate("vehicles");
        }

        public IEnumerable<string> GetVehicles() {
            IEnumerable<string> vehicleData = CacheProvider.Get("vehicles") as IEnumerable<string>;

            if (vehicleData == null) {
                vehicleData = this.DataContext;
                CacheProvider.Set("vehicles", vehicleData, 30);
            }

            return vehicleData;
        }
    }
}