﻿using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public IActionResult Update(int? id)
        {
            Contact obj = new Contact();
            if (id == null)
            {
                return View(obj);
            }
            obj = _db.Contacts.FirstOrDefault(a => a.ContactId == id);

            if (obj == null)
            {
                return NotFound();

            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult Update(Contact obj)
        {
            List<string> NameList = _db.Contacts.Select(x => x.Name.ToLower()).ToList();

            if (ModelState.IsValid)
            {
                string name = obj.Name;
                string phoneNumber = obj.PhoneNumber;
                if (!NameList.Contains(name.ToLower()))
                {
                    if (obj.ContactId == 0)
                    {


                        _db.Contacts.Add(new Contact { Name = name, PhoneNumber = phoneNumber });



                    }
                    else
                    {

                        _db.Contacts.Update(obj);
                    }

                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }

        public IActionResult Delete(int id)
        {
            var objDb = _db.Contacts.FirstOrDefault(x => x.ContactId == id);
            _db.Contacts.Remove(objDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteMultipleData()
        {
            IEnumerable<Contact> ContactList = _db.Contacts.OrderByDescending(a => a.ContactId).Take(10).ToList();



            _db.Contacts.RemoveRange(ContactList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Obsolete]
        public IActionResult ImportContacts(IFormFile file)
        {
            List<Contact> ContactList = new List<Contact>();
            List<string> NameList = _db.Contacts.Select(x => x.Name.ToLower()).ToList();

            using (var sreader = new StreamReader(file.OpenReadStream()))
            {
                string[] headers = sreader.ReadLine().Split(',');     //Title
                while (!sreader.EndOfStream)                          //get all the content in rows 
                {
                    string[] rows = sreader.ReadLine().Split(',');
                    string name = rows[0];
                    string phoneNumber = rows[1];
                    if (NameList.Contains(name.ToLower()))
                    {
                        continue;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(phoneNumber))
                            ContactList.Add(new Contact { Name = name, PhoneNumber = phoneNumber });
                    }

                }

            }

            _db.Contacts.AddRange(ContactList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }




        //public IActionResult Run()
        //{
        //    List<String> phoneNumbers = _db.Contacts.Select(x=>x.PhoneNumber).ToList();


        //    var autoMessageService = new AutoMessage.Services.AutoMessageService();
        //    autoMessageService.SendMessage( phoneNumbers );


        //    return View();


        //}
    }
}
