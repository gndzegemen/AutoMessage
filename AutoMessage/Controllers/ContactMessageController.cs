using AutoMessage.ViewModel;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMessage.Controllers
{
    public class ContactMessageController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ContactMessageController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<ContactMessage> objList = _db.ContactMessages.Include(c=>c.Contact).Include(m=>m.Message).ToList();
            List<Contact> contact = _db.Contacts.ToList();
            List<Message> message = _db.Messages.ToList();

            ContactsAndMessages contactsAndMessages = new ContactsAndMessages();

            contactsAndMessages.contacts = contact;
            contactsAndMessages.messages = message;

            contactsAndMessages.contactMessages = objList;

            return View(contactsAndMessages);
        }

        
        public IActionResult SendAllMessagesToOneContact()
        {
            var autoMessageService = new Services.AutoMessageService();
            List<string> text = _db.Messages.Select(x=>x.Text).ToList();
            
            autoMessageService.SendManyMessagesToOneContact(text);
            return View();
        }

        public IActionResult SendAMessageToAllContacts()
        {
            var autoMessageService = new Services.AutoMessageService();
            List<string> name = _db.Contacts.Select(x=>x.Name).ToList();

            autoMessageService.SendOneMessageToManyContacts(name);
            return View();
        }

       
        [HttpPost]
        public IActionResult AddANumberOnAContact(int contactId, int messageId)
        {

            //Contact contact = _db.Contacts.Find(contactId);
            //Message message = _db.Messages.Find(messageId);

            _db.ContactMessages.Add(new ContactMessage
            {
                ContactId=contactId,
                MessageId= messageId
            });
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult OtherOption()
        {
            List<ContactMessage> objList = _db.ContactMessages.Include(c => c.Contact).Include(m => m.Message).ToList();

            List<Contact> contact = _db.Contacts.ToList();
            List<Message> message = _db.Messages.ToList();

            ContactsAndMessages contactsAndMessages = new ContactsAndMessages();

            contactsAndMessages.contacts = contact;
            contactsAndMessages.messages = message;

            contactsAndMessages.contactMessages = objList;
            return View(contactsAndMessages);
        }

    }
}
