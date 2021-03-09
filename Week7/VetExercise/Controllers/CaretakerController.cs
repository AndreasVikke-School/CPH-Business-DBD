using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VetExercise.Controllers.Resources;
using VetExercise.DataModels;
using VetExercise.Persistent;
using VetExercise.Persistent.Repositories;
using VetExercise.Persistent.Repositories.Pets;

namespace VetExercise.Controllers
{
    [ApiController]
    [Route("api/caretaker")]
    public class CaretakerController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly CaretakerRepository caretakerRepository;
        private readonly PetRepository petRepository;
        private readonly AddressRepository addressRepository;
        private readonly CityRepository cityRepository;

        public CaretakerController(
            UnitOfWork unitOfWork,
            CaretakerRepository caretakerRepository,
            PetRepository petRepository,
            AddressRepository addressRepository,
            CityRepository cityRepository
        )
        {
            this.unitOfWork = unitOfWork;
            this.caretakerRepository = caretakerRepository;
            this.petRepository = petRepository;
            this.addressRepository = addressRepository;
            this.cityRepository = cityRepository;
        }

        [HttpGet("get/id")]
        public async Task<ActionResult<Caretaker>> GetCaretaker(long id)
        {
            var caretaker = await this.caretakerRepository.GetAsync(id, true);
            return Ok(caretaker);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<Caretaker>> GetAllCaretakers()
        {
            var caretakers = await this.caretakerRepository.GetAllAsync(true);
            return Ok(caretakers);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Caretaker>> CreateCaretaker([FromBody]CaretakerResource caretaker)
        {
            var city = await this.cityRepository.GetByZipAsync(caretaker.address.city.zip);
            if (city == null)
                city = new City {
                    zip = caretaker.address.city.zip,
                    city = caretaker.address.city.city
                };

            Caretaker newCaretaker = new Caretaker {
                name = caretaker.name,
                address = new Address {
                    street = caretaker.address.street,
                    city = city
                }
            };

            this.caretakerRepository.Add(newCaretaker);
            await this.unitOfWork.CompletedAsync();

            return Ok(newCaretaker);
        }

        [HttpPost("pet/link")]
        public async Task<ActionResult<Caretaker>> LinkPet([FromBody]CaretakerPetLinkResource caretakerPetLinkResource)
        {
            var pet = await this.petRepository.GetAsync(caretakerPetLinkResource.petId, true);
            if (pet == null)
                return NotFound("Pet Not Found");

            var caretaker = await this.caretakerRepository.GetAsync(caretakerPetLinkResource.caretakerId, true);
            if (caretaker == null)
                return NotFound("Caretaker Not Found");

            caretaker.pets.Add(pet);
            pet.caretakers.Add(caretaker);
            await this.unitOfWork.CompletedAsync();

            return Ok(caretaker);
        }
    }
}
