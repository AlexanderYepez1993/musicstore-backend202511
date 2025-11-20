using Microsoft.AspNetCore.Mvc;
using MusicStore.Entities;
using MusicStore.Repositories;

namespace MusicStore.Api.Controllers
{
    [Controller]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly GenreRepository repository;

        public GenresController(GenreRepository repository)
        {
            this.repository = repository;
        }

        //api/genres
        [HttpGet]
        public ActionResult<List<Genre>> Get()
        {
            var data = repository.Get();
            return Ok(data);
        }

        //api/genres/1
        [HttpGet("{id:int}")]
        public ActionResult<Genre> Get(int id)
        {
            var item = repository.Get(id);
            return item is not null ? Ok(item) : NotFound();
        }

        //api/genres
        [HttpPost]
        public ActionResult<Genre> Post(Genre genre)
        {
            repository.Add(genre);
            return Ok(genre);
        }

        //api/genres/1 --- x es el id
        [HttpPut("{x:int}")]  
        public ActionResult Put(int x, Genre genre)
        {
            repository.Update(x, genre);
            return Ok();
        }
         
        //api/genres/1
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            return Ok();
        }
    }
}
