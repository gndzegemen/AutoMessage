using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMessage.ViewModel
{
    public class ContactsAndMessages
    {

        //public ContactsAndMessages()
        //{
        //    contacts = new List<Contact>();
        //    messages = new List<Message>();
        //    contactMessages = new List<ContactMessage>();
        //}
        public List<Contact> contacts { get; set; }

        public List<Message> messages { get; set; }

        public IEnumerable<ContactMessage> contactMessages { get; set; }


        public int GetContactCount()
        {
            return contacts.Count();
        }

        public int GetMessageCount()
        {
            return messages.Count();
        }

        

    }
}
