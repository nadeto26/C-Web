using ChatApp.Models.Message;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController:Controller
    {
        private static List<KeyValuePair<string, string>> _messages = new List<KeyValuePair<string, string>>();

       

        public IActionResult Show()
        {
            if (_messages.Count < 1)
            {
                // If there are no messages, return an empty view.
                return View(new ChatViewModel { Messages = new List<MessageViewModel>() });
            }

            var chatModel = new ChatViewModel
            {
                Messages = _messages
                    .Select(m => new MessageViewModel
                    {
                        Sender = m.Key,
                        MessageText = m.Value
                    })
                    .ToList()
            };

            return View(chatModel);
        }
        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            if (chat != null && chat.CurrentMessage != null)
            {
                var newMessage = chat.CurrentMessage;
                _messages.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));
            }

            // Redirect to the Show action after sending a message.
            return RedirectToAction("Show");
        }

    }
}
