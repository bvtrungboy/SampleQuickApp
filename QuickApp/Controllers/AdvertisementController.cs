using AutoMapper;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickApp.ViewModels;
using System;
using System.Collections.Generic;

namespace QuickApp.Controllers
{
    [Route("api/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public AdvertisementController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<AdvertisementController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allAdvertisement = _unitOfWork.Advertisement.GetAll();
            return Ok(_mapper.Map<IEnumerable<AdvertisementViewModel>>(allAdvertisement));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var advertisement = _unitOfWork.Advertisement.Get(id);
            return Ok(_mapper.Map<AdvertisementViewModel>(advertisement));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] AdvertisementViewModel value)
        {
            Console.WriteLine(value);
            var advertisement = new Advertisement()
            {
                Name = value.Name,
                Price = value.Price,
                StartDate = value.StartDate,
                EndDate = value.EndDate
            };

           _unitOfWork.Advertisement.Add(advertisement);
           _unitOfWork.SaveChanges();
        }
    }
}
