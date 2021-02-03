using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mysite.Dtos;
using mysite.Interfaces;
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
    public class MembersController : ControllerBase
    {
        private readonly ITechRepository _repo;

        public MembersController(ITechRepository repo)
        {
            _repo = repo;
        }


        //GET: api/Members
        [HttpGet]
        public IEnumerable<Member> GetMembers()
        {
            return _repo.GetAllMembers();
        }

        //GET: api/Members/1
        [HttpGet("{id}")]
        public ActionResult GetMember([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = _repo.GetMember(id);

            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMember([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var member = _repo.GetMember(id);
            if (member == null)
            {
                return NotFound();
            }

            _repo.RemoveMember(id);
            _repo.SaveChangesAsync();
            return Ok(member);
        }




    }
}
