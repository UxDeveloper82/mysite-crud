using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mysite.Data.FileManager;
using mysite.Models;
using mysite.Dtos;
using mysite.Interfaces;
using mysite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace mysite.Areas.Member.Controllers
{

    public class MembersController : Controller
    {
    
        private readonly ITechRepository _repo;
        private readonly IFileManager _fileManager;

        public MembersController(ITechRepository repo, 
            IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            
            return View();
         
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new MemberViewModel());
            else
            {
                var member = _repo.GetMember((int)id);
                return View(new MemberViewModel
                {
                    Id = member.Id,
                    UserName = member.UserName,
                    DateOfBirth = member.DateOfBirth,
                    KnownAs = member.KnownAs,
                    Created = member.Created,
                    LastActive = member.LastActive,
                    Gender = member.Gender,
                    Introduction = member.Introduction,
                    LookingFor = member.LookingFor,
                    Interests = member.Interests,
                    City = member.City,
                    Country = member.Country,
                    CurrentImage = member.Photo
                });

            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(MemberViewModel vm)
        {
            var user = new Models.Member
            {
               Id = vm.Id,
               UserName = vm.UserName,
               DateOfBirth = vm.DateOfBirth,
               KnownAs = vm.KnownAs,
               Created = vm.Created,
               LastActive = vm.LastActive,
               Gender = vm.Gender,
               Introduction = vm.Introduction,
               LookingFor = vm.LookingFor,
               Interests = vm.Interests,
               City = vm.City,
               Country = vm.Country
            };
            if (vm.Photo == null)
            {
                user.Photo = vm.CurrentImage;
            }
            else
            {
                user.Photo= await _fileManager.SaveImage(vm.Photo);
            }
            if (user.Id > 0)
                _repo.UpdateMember(user);
            else
                _repo.AddMember(user);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(user);
        }

        [HttpGet("/MemberPhoto/{memberPhoto}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult MemberPhoto(string memberPhoto)
        {
            var mine = memberPhoto.Substring(memberPhoto.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(memberPhoto), $"memberPhoto/{mine}");
        }

    }
}
