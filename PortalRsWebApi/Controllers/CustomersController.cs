using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalRSApi.Data;
using PortalRSApi.Models;

namespace PortalRSApi.Controllers
{   
   
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
        public IActionResult Post([FromBody]Customer customers)
        {
            customers.Name = customers.Name.Trim();
            customers.RegisterDate = DateTime.Today;

            if (_db.Customers.Count(c => c.Name == customers.Name) >= 1)
            {
                return BadRequest($"Cliente já existente: {customers.Name}");
            }

            _db.Customers.Add(customers);
            _db.SaveChanges();
            return Json($"Cliente salvo: {customers.Name}");
        }

        [HttpPut]
        public IActionResult Put([FromBody]Customer customers)
        {
            _db.Entry(customers).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }
    }
}