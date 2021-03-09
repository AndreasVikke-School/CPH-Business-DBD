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

namespace VetExercise.Controllers
{
    [ApiController]
    [Route("api/vet")]
    public class VetController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly VetRepository vetRepository;
        private readonly AddressRepository addressRepository;
        private readonly CityRepository cityRepository;

        public VetController(
            UnitOfWork unitOfWork,
            VetRepository vetRepository,
            AddressRepository addressRepository,
            CityRepository cityRepository
        )
        {
            this.unitOfWork = unitOfWork;
            this.vetRepository = vetRepository;
            this.addressRepository = addressRepository;
            this.cityRepository = cityRepository;
        }

        [HttpGet("get/id")]
        public async Task<ActionResult<Vet>> GetVet(long id)
        {
            var vet = await this.vetRepository.GetAsync(id, true);
            return Ok(vet);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<Vet>> GetAllVets()
        {
            var vets = await this.vetRepository.GetAllAsync(true);
            return Ok(vets);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Vet>> CreateVet([FromBody]VetResource vet)
        {
            var city = await this.cityRepository.GetByZipAsync(vet.address.city.zip);
            if (city == null)
                city = new City {
                    zip = vet.address.city.zip,
                    city = vet.address.city.city
                };

            Vet newVet = new Vet {
                cvr = vet.cvr,
                name = vet.name,
                address = new Address {
                    street = vet.address.street,
                    city = city
                }
            };

            this.vetRepository.Add(newVet);
            await this.unitOfWork.CompletedAsync();

            return Ok(newVet);
        }
    }
}
