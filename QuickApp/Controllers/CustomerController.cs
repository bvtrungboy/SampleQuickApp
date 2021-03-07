// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using QuickApp.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using QuickApp.Helpers;
using DAL.Models;

namespace QuickApp.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;


        public CustomerController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CustomerController> logger, IEmailSender emailSender)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailSender = emailSender;
        }



        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var allCustomers = _unitOfWork.Customers.GetAllCustomersData();
            return Ok(_mapper.Map<IEnumerable<CustomerViewModel>>(allCustomers));
        }

        [HttpGet("throw")]
        public IEnumerable<CustomerViewModel> Throw()
        {
            throw new InvalidOperationException("This is a test exception: " + DateTime.Now);
        }

        [HttpGet("email")]
        public async Task<string> Email()
        {
            string recepientName = "QickApp Tester"; //         <===== Put the recepient's name here
            string recepientEmail = "bvtrungboy@gmail.com"; //   <===== Put the recepient's email here

            string message = EmailTemplates.GetTestEmail(recepientName, DateTime.UtcNow);

            (bool success, string errorMsg) = await _emailSender.SendEmailAsync(recepientName, recepientEmail, "Test Email from QuickApp", message);

            if (success)
                return "Success";

            return "Error: " + errorMsg;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Customers = _unitOfWork.Customers.GetCustomer(id);
           return Ok(_mapper.Map<CustomerViewModel>(Customers));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Customer value)
        {
           _unitOfWork.Customers.Add(value);
            _unitOfWork.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Customer value)
        {
            var cus = _unitOfWork.Customers.GetCustomer(id);
            if (cus != null)
            {
                cus.Name = value.Name;
                cus.PhoneNumber = value.PhoneNumber;
                _unitOfWork.Customers.Update(cus);
                _unitOfWork.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var cus = _unitOfWork.Customers.GetCustomer(id);
            if (cus != null)
            {
                _unitOfWork.Customers.Remove(cus);
                _unitOfWork.SaveChanges();
            }
        }
    }
}
