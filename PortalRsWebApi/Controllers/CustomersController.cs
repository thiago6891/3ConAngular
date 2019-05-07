using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PortalRSApi.Data;
using PortalRSApi.Models;

namespace PortalRSApi.Controllers
{   
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        protected readonly ApplicationDbContext _db;

        public CustomersController(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            return Ok( _db.Customers.OrderBy(c => c.Name).ToList());
                
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var customer = _db.Customers.Find(id);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!TryValidateUsername(customer, out string error))
            {
                return BadRequest(error);
            }

            customer.RegisterDate = DateTime.Today;

            _db.Customers.Add(customer);
            _db.SaveChanges();
            return Json($"Cliente salvo: {customer.Name}");
        }

        [HttpPut]
        public IActionResult Put([FromBody]Customer customer)
        {
            if (!TryValidateUsername(customer, out string error))
            {
                return BadRequest(error);
            }

            _db.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }

        private bool TryValidateUsername(Customer customer, out string error)
        {
            error = "";
            customer.Name = customer.Name.Trim();

            if (customer.Name.ToLower() == "3con")
            {
                error = "Nome de usuário não permitido.";
                return false;
            }

            if (_db.Customers.Count(c => c.Name == customer.Name && c.Id != customer.Id) >= 1)
            {
                error = $"Cliente já existente: {customer.Name}";
                return false;
            }

            return true;
        }
    }
}