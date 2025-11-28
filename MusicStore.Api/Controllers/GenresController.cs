using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories;
using System.Net;

namespace MusicStore.Api.Controllers
{
    [Controller]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository repository;
        private readonly ILogger<GenresController> logger;

        public GenresController(IGenreRepository repository, ILogger<GenresController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        //api/genres
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new BaseResponseGeneric<ICollection<GenreResponseDto>>();
            try
            {
                response.Data = await repository.GetAsync();
                response.Success = true;
                logger.LogInformation("Obteniendo todos los géneros musicales...");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        //api/genres/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new BaseResponseGeneric<GenreResponseDto>();
            try
            {
                response.Data = await repository.GetAsync(id);
                response.Success = true;
                logger.LogInformation($"Obteniendo el género musical con id {id}.");
                //return response.Data is not null ? Ok(response) : NotFound(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        //api/genres
        [HttpPost]
        public async Task<IActionResult> Post(GenreRequestDto genre)
        {
            var response = new BaseResponseGeneric<int>();
            try
            {
                var genreId = await repository.AddAsync(genre);
                response.Data = genreId;
                response.Success = true;
                logger.LogInformation($"Género musical insertado con el id {genreId}.");
                //return Ok(response); //Retorna 200 pero se desea retornar un 201 (created)
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al insertar";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        //api/genres/1 --- x es el id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, GenreRequestDto genre)
        {
            var response = new BaseResponse();
            try
            {
                await repository.UpdateAsync(id, genre);
                response.Success = true;
                logger.LogInformation($"Género musical con id {id} fue actualizado.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al actualizar";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }

        //api/genres/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.DeleteAsync(id);
                response.Success = true;
                logger.LogInformation($"Género musical con id {id} fue eliminado.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al eliminar";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                return BadRequest(response);
            }
        }
    }
}
