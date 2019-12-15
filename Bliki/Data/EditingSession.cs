using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bliki.Data
{
    public class EditingSession
    {
        public EditingSession(WikiPageModel model, string username)
        {
            PageModel = model;
            UserName = username;
            CheckedOutTime = DateTime.Now;
        }

        public WikiPageModel PageModel { get; set; }
        public DateTime CheckedOutTime { get; set; }
        public string UserName { get; set; }
    }
}
