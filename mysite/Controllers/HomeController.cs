using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysite.Data;
using mysite.Data.FileManager;
using mysite.Interfaces;
using mysite.Models;
using mysite.ViewModels;

namespace mysite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITechRepository _repo;
        private readonly IFileManager _fileManager;

        public HomeController(ITechRepository repo,
            IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
  
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            var members = _repo.GetAllMembers();
        
            return View(members);

        }

        public IActionResult Details(int id)
        {
            var member = _repo.GetMember(id);
            return View(member);

        }

        [HttpGet("/MemberPhoto/{memberPhoto}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult MemberPhoto(string memberPhoto)
        {
            var mine = memberPhoto.Substring(memberPhoto.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(memberPhoto), $"memberPhoto/{mine}");
        }

        public IActionResult UserPanel()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult New()
        {
            var viewModel = new HomeViewModel
            {
                Home = new Home()
              
            };

            return View("HomeForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Home home)
        {

            return View();
        }


        public IActionResult HomeEdit()
        {
            return View();
        }

        public IActionResult YouTube()
        {
            return View();
        }

        public IActionResult MyVideos() 
        {

            return View();
        }

    }
}