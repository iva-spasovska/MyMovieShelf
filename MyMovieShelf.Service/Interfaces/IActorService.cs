using MyMovieShelf.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieShelf.Service.Interfaces
{
    public interface IActorService
    {
        List<Actor> GetAll();
        Actor? GetById(Guid Id);
        Actor Create(Actor actor);
        Actor Update(Actor actor);
    }
}
