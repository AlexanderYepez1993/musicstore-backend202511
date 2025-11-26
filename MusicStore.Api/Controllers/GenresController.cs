using Microsoft.AspNetCore.Mvc;
using MusicStore.Entities;
using MusicStore.Repositories;

namespace MusicStore.Api.Controllers
{
    [Controller]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository repository;

        public GenresController(IGenreRepository repository)
        {
            this.repository = repository;
        }

        //api/genres
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await repository.GetAsync();
            return Ok(data);
        }

        //api/genres/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await repository.GetAsync(id);
            return item is not null ? Ok(item) : NotFound();
        }

        //api/genres
        [HttpPost]
        public async Task<IActionResult> Post(Genre genre)
        {
            await repository.AddAsync(genre);
            return Ok(genre);
        }

        //api/genres/1 --- x es el id
        [HttpPut("{x:int}")]  
        public async Task<IActionResult> Put(int x, Genre genre)
        {
            await repository.UpdateAsync(x, genre);
            return Ok();
        }
         
        //api/genres/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id);
            return Ok();
        }
    }
}
