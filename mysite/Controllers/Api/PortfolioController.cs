using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mysite.Data.FileManager;
using mysite.Data.Repository;
using mysite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Controllers.Api
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortRepository _repo;
        private readonly IFileManager _fileManager;

        public PortfolioController(IPortRepository repo,
                                   IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Portfolio> GetPorts()
        {
            return _repo.GetAllPortfolios();
        }

        //GET: api/Members/1
        [HttpGet("{id}")]
        public ActionResult GetPort([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var port = _repo.GetPort(id);

            if (port == null)
            {
                return NotFound();
            }
            return Ok(port);
        }

    }
}
