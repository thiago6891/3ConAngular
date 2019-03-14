using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using PortalRSApi.Data;
using System.Linq;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalRSApi.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _db;

        public BaseController
            (            ApplicationDbContext applicationDbContext
           )
        {
           
            _db = applicationDbContext;
        }

        
    }
}