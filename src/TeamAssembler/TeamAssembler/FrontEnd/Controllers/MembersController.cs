using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MembersController : Controller
    {
        public MembersController()
        {
        }

        // GET: api/Members
        [HttpGet("")]
        public IActionResult Get()
        {
            List<string> result = new List<string>
            {
                "Thor",
                "Hulk",
                "Captain America",
                "Black Widow",
                "Ironman",
                "Spiderman",
                "Vision",
                "Dr. Strange",
                "Black Panther"
            };

            return Json(result);
        }
    }
}
