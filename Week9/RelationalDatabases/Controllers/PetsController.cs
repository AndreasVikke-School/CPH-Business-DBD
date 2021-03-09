using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RelationalDatabases.Controllers.Resources;
using RelationalDatabases.DataModels;
using RelationalDatabases.DataModels.QueryModels;
using RelationalDatabases.Persistent;
using RelationalDatabases.Persistent.Repositories;

namespace RelationalDatabases.Controllers
{
    [ApiController]
    [Route("/pets")]
    public class PetsController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly AddressRepository addressRepository;
        private readonly CityRepository cityRepository;
        private readonly CaretakerRepository caretakerRepository;
        private readonly VetRepository vetRepository;
        private readonly PetRepository petRepository;

        public PetsController (
            UnitOfWork unitOfWork,
            AddressRepository addressRepository,
            CityRepository cityRepository,
            CaretakerRepository caretakerRepository,
            VetRepository vetRepository,
            PetRepository petRepository
        ) {
            this.unitOfWork = unitOfWork;
            this.addressRepository = addressRepository;
            this.cityRepository = cityRepository;
            this.caretakerRepository = caretakerRepository;
            this.vetRepository = vetRepository;
            this.petRepository = petRepository;
        }
        
        [HttpGet("get/id")]
        public async Task<ActionResult<Pet>> GetPet(long id) {
            var pet = await this.petRepository.GetAsync(id, true);
            return Ok(pet);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<BaseQueryResult<Pet>>> GetAllPets() {
            var query = new BaseQueryModel {
                PageSize = -1
            };

            BaseQueryResult<Pet> pets = await this.petRepository.GetAllAsync(query);
            return Ok(pets);
        }

        #region Add Pets
        [HttpPost("pet/add")]
        public async Task<ActionResult<Pet>> AddPet([FromBody]PetResource pet) {
            List<Caretaker> caretakers = new List<Caretaker>();
            foreach(long id in pet.caretakerIds) {
                var caretaker = await this.caretakerRepository.GetAsync(id);
                if(caretaker == null)
                    return NotFound($"Caretaker with id {id} Not Found");
                caretakers.Add(caretaker);
            }

            var vet = await this.vetRepository.GetAsync(pet.vetId);
            if(vet == null)
                return NotFound("Vet Not Found");

            Pet newPet = new Pet {
                name = pet.name,
                age = pet.age,
                vet = vet,
                caretakers = caretakers
            };

            this.petRepository.Add(newPet);
            await this.unitOfWork.CompletedAsync();

            return Ok(newPet);
        }

        [HttpPost("dog/add")]
        public async Task<ActionResult<Dog>> AddDog([FromBody]DogResource dog) {
            List<Caretaker> caretakers = new List<Caretaker>();
            foreach(long id in dog.caretakerIds) {
                var caretaker = await this.caretakerRepository.GetAsync(id);
                if(caretaker == null)
                    return NotFound($"Caretaker with id {id} Not Found");
                caretakers.Add(caretaker);
            }

            var vet = await this.vetRepository.GetAsync(dog.vetId);
            if(vet == null)
                return NotFound("Vet Not Found");

            Dog newDog = new Dog {
                name = dog.name,
                age = dog.age,
                barkPitch = dog.barkPitch,
                vet = vet,
                caretakers = caretakers
            };

            this.petRepository.Add(newDog);
            await this.unitOfWork.CompletedAsync();

            return Ok(newDog);
        }

        [HttpPost("cat/add")]
        public async Task<ActionResult<Dog>> AddCat([FromBody]CatResource cat) {
            List<Caretaker> caretakers = new List<Caretaker>();
            foreach(long id in cat.caretakerIds) {
                var caretaker = await this.caretakerRepository.GetAsync(id);
                if(caretaker == null)
                    return NotFound($"Caretaker with id {id} Not Found");
                caretakers.Add(caretaker);
            }

            var vet = await this.vetRepository.GetAsync(cat.vetId);
            if(vet == null)
                return NotFound("Vet Not Found");

            Cat newCat = new Cat {
                name = cat.name,
                age = cat.age,
                lifeCount = cat.lifeCount,
                vet = vet,
                caretakers = caretakers
            };

            this.petRepository.Add(newCat);
            await this.unitOfWork.CompletedAsync();

            return Ok(newCat);
        }
        #endregion
    }
}
