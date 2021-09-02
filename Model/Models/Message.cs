using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        public int Count { get; set; }
        public string Text { get; set; }

        public ICollection<ContactMessage> ContactMessages { get; set; }
    }
}
