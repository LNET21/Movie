using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Services
{
    public interface IGenreSelectListService
    {
        Task<IEnumerable<SelectListItem>> GetGenreAsync();
    }
}