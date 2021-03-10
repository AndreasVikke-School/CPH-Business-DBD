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
    [Route("api/vet")]
    public class VetController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly AddressRepository addressRepository;
        private readonly VetRepository vetRepository;
    
        public VetController (
            UnitOfWork unitOfWork,
            AddressRepository addressRepository,
            VetRepository vetRepository
        ) {
            this.unitOfWork = unitOfWork;
            this.addressRepository = addressRepository;
            this.vetRepository = vetRepository;
        }

        [HttpGet("get/id")]
        public async Task<ActionResult<Vet>> GetVet(long id) {
            var vet = await this.vetRepository.GetAsync(id, true);
            return Ok(vet);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<BaseQueryResult<Vet>>> GetAllVets() {
            var query = new BaseQueryModel {
                PageSize = -1
            };

            BaseQueryResult<Vet> vet = await this.vetRepository.GetAllAsync(query);
            return Ok(vet);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Vet>> AddVet([FromBody]VetResource vet) {
            var address = await this.addressRepository.GetAsync(vet.addressId);
            if(address == null)
                return NotFound($"Address with id {vet.addressId} Not Found");

            Vet newVet = new Vet {
                cvr = vet.cvr,
                name = vet.name,
                phone = vet.phone,
                address = address
            };

            this.vetRepository.Add(newVet);
            await this.unitOfWork.CompletedAsync();

            return Ok(newVet);
        }

    }
}