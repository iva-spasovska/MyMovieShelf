using MyMovieShelf.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Service.Interfaces
{
    public interface IGenreService
    {
        List<Genre> GetAll();
        Genre? GetById(Guid Id);
        Genre Create(Genre genre);
        Genre Update(Genre genre);
    }
}
