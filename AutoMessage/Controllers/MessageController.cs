using DataAccess.Data;
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
    public class MessageController : Controller
    {

        private readonly ApplicationDbContext _db;

        public MessageController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Message> objlist = _db.Messages.ToList();

            return View(objlist);
        }

        public IActionResult Update(int? id)
        {
            Message obj = new Message();
            if (id == null)
            {
                return View(obj);
            }
            obj = _db.Messages.FirstOrDefault(a => a.MessageId == id);

            if (obj == null)
            {
                return NotFound();

            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult Update(Message obj)
        {
            List<string> TextList = _db.Messages.Select(x => x.Text.ToLower()).ToList();

            if (ModelState.IsValid)
            {
                string text = obj.Text;
                int count = obj.Count;
                if (!TextList.Contains(text.ToLower()))
                {
                    if (obj.MessageId == 0)
                    {


                        _db.Messages.Add(new Message { Text = text, Count=count });



                    }
                    else
                    {

                        _db.Messages.Update(obj);
                    }

                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }

        public IActionResult Delete(int id)
        {
            var objDb = _db.Messages.FirstOrDefault(x => x.MessageId == id);
            _db.Messages.Remove(objDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteMultipleData()
        {
            IEnumerable<Message> MessageList = _db.Messages.OrderByDescending(a => a.MessageId).Take(10).ToList();



            _db.Messages.RemoveRange(MessageList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Obsolete]
        public IActionResult ImportMessages(IFormFile file)
        {
            List<Message> MessageList = new List<Message>();
            List<string> TextList = _db.Messages.Select(x => x.Text.ToLower()).ToList();

            using (var sreader = new StreamReader(file.OpenReadStream()))
            {
                string[] headers = sreader.ReadLine().Split(',');     //Title
                while (!sreader.EndOfStream)                          //get all the content in rows 
                {
                    string[] rows = sreader.ReadLine().Split(',');
                    string count = rows[0];
                    string text = rows[1];
                    if (TextList.Contains(text.ToLower()))
                    {
                        continue;
                    }
                    else { MessageList.Add(new Message{ Text=text, Count = int.Parse(count) }); }

                }

            }

            _db.Messages.AddRange(MessageList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
