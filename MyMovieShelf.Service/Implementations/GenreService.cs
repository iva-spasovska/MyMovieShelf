using Microsoft.EntityFrameworkCore;
using MyMovieShelf.Domain.DomainModels;
using MyMovieShelf.Repository.Interfaces;
using MyMovieShelf.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Service.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _repository;

        public GenreService(IRepository<Genre> repository)
        {
            _repository = repository;
        }

        public Genre Create(Genre genre)
        {
            return _repository.Insert(genre);
        }

        public List<Genre> GetAll()
        {
            return _repository.GetAll(selector: x => x).OrderBy(y => y.Name).ToList();
        }

        public Genre? GetById(Guid Id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == Id);
        }

        public Genre Update(Genre genre)
        {
            return _repository.Update(genre);
        }
    }
}
