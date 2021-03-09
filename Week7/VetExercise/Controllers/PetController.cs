using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VetExercise.Controllers.Resources;
using VetExercise.DataModels;
using VetExercise.DataModels.QueryModels;
using VetExercise.Persistent;
using VetExercise.Persistent.Repositories;
using VetExercise.Persistent.Repositories.Pets;

namespace VetExercise.Controllers
{
    [ApiController]
    [Route("api/pet")]
    public class PetController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly CatRepository catRepository;
        private readonly DogRepository dogRepository;
        private readonly VetRepository vetRepository;
        private readonly CaretakerRepository caretakerRepository;

        public PetController(
            UnitOfWork unitOfWork,
            CatRepository catRepository,
            DogRepository dogRepository,
            VetRepository vetRepository,
            CaretakerRepository caretakerRepository
        )
        {
            this.unitOfWork = unitOfWork;
            this.catRepository = catRepository;
            this.dogRepository = dogRepository;
            this.vetRepository = vetRepository;
            this.caretakerRepository = caretakerRepository;
        }

        #region Cat
        [HttpGet("cat/get/id")]
        public async Task<ActionResult<Cat>> GetCat(long id)
        {
            var cat = await this.catRepository.GetAsync(id, true);
            return Ok(cat);
        }

        [HttpGet("cat/get/all")]
        public async Task<ActionResult<BaseQueryResult<Cat>>> GetAllCats()
        {
            var cats = await this.catRepository.GetAllAsync(true);
            return Ok(cats);
        }

        [HttpPost("cat/add")]
        public async Task<ActionResult<Cat>> CreateCat([FromBody]CatResource cat)
        {
            var vet = await this.vetRepository.GetAsync(cat.vetId);
            if(vet == null)
                return NotFound("Vet not found");

            Cat newCat = new Cat {
                name = cat.name,
                age = cat.age,
                lifeCount = cat.lifeCount,
                vet = vet
            };
            this.catRepository.Add(newCat);
            await this.unitOfWork.CompletedAsync();
            return Ok(newCat);
        }
        #endregion

        #region Dog
        [HttpGet("dog/get/id")]
        public async Task<ActionResult<Dog>> GetDog(long id)
        {
            var dog = await this.dogRepository.GetAsync(id, true);
            return Ok(dog);
        }

        [HttpGet("dog/get/all")]
        public async Task<ActionResult<BaseQueryResult<Dog>>> GetAllDogs()
        {
            var dogs = await this.dogRepository.GetAllAsync(true);
            return Ok(dogs);
        }

        [HttpPost("dog/add")]
        public async Task<ActionResult<Dog>> CreateDog([FromBody]DogResource dog)
        {
            var vet = await this.vetRepository.GetAsync(dog.vetId);
            if(vet == null)
                return NotFound("Vet not found");

            Dog newDog = new Dog {
                name = dog.name,
                age = dog.age,
                barkPitch = dog.barkPitch,
                vet = vet
            };
            this.dogRepository.Add(newDog);
            await this.unitOfWork.CompletedAsync();
            return Ok(newDog);
        }
        #endregion
    }
}
