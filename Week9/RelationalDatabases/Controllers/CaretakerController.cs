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
    [Route("api/caretaker")]
    public class CaretakerController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly AddressRepository addressRepository;
        private readonly CaretakerRepository caretakerRepository;

        public CaretakerController (
            UnitOfWork unitOfWork,
            AddressRepository addressRepository,
            CaretakerRepository caretakerRepository
        ) {
            this.unitOfWork = unitOfWork;
            this.addressRepository = addressRepository;
            this.caretakerRepository = caretakerRepository;
        }

        [HttpGet("get/id")]
        public async Task<ActionResult<Caretaker>> GetCaretaker(long id) {
            var caretaker = await this.caretakerRepository.GetAsync(id, true);
            return Ok(caretaker);
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<BaseQueryResult<Caretaker>>> GetAllCaretakers() {
            var query = new BaseQueryModel {
                PageSize = -1
            };

            BaseQueryResult<Caretaker> caretaker = await this.caretakerRepository.GetAllAsync(query);
            return Ok(caretaker);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Caretaker>> AddCaretaker([FromBody]CaretakerResource caretaker) {
            var address = await this.addressRepository.GetAsync(caretaker.addressId);
            if(address == null)
                return NotFound($"Address with id {caretaker.addressId} Not Found");

            Caretaker newCaretaker = new Caretaker {
                name = caretaker.name,
                phone = caretaker.phone,
                address = address
            };

            this.caretakerRepository.Add(newCaretaker);
            await this.unitOfWork.CompletedAsync();

            return Ok(newCaretaker);
        }
    }
}