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
    public class ActorService : IActorService
    {
        private readonly IRepository<Actor> _repository;

        public ActorService(IRepository<Actor> repository)
        {
            _repository = repository;
        }

        public Actor Create(Actor actor)
        {
            return _repository.Insert(actor);
        }

        public List<Actor> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public Actor? GetById(Guid Id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == Id);
        }

        public Actor Update(Actor actor)
        {
            return _repository.Update(actor);
        }
    }
}
