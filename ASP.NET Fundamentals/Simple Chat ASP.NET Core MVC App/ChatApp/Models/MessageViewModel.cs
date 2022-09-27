using ChatApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Models
{
    public class MessageViewModel
    {
        public string Sender { get; set; }

        public string MessageText { get; set; }
        
    }
}
