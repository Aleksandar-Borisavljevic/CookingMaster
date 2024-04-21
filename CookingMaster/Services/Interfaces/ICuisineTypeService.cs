using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMaster.Services.Interfaces
{
    public interface ICuisineTypeService
    {
        Task<IEnumerable<CuisineType>> GetCuisineTypesAsync();
    }
}
