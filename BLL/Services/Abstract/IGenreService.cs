using CIL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IGenreService
    {
        public Task<IEnumerable<Genre>> Get();
        public Task<Genre> Add(Genre item);
    }
}
