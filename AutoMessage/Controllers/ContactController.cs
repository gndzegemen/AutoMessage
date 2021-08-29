using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMessage.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Contact> objlist = _db.Contacts.ToList();

            return View(objlist);
        }

        
        public IActionResult Run(int id)
        {

            var autoMessageService = new AutoMessage.Services.AutoMessageService();
            autoMessageService.SendMessage();
            return View();


        }
    }
}
