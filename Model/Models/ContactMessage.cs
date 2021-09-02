using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ContactMessage
    {
        public int ContactMessageId { get; set; }

        [ForeignKey("Hotel")]
        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        [ForeignKey("Message")]
        public int MessageId { get; set; }
        public Message Message { get; set; }

        
    }
}
