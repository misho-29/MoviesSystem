using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.App.NotificationManager
{
    public class HtmlGenerator
    {
        public string GenerateReminderMessage(string title, string description, string rating, string imageUrl)
        {
            var content = string.Format(@"<center><h2 style='color:red;'>{0} - {1}</h2><img src=""{2}""></center><div>{3}</div>",
                title, rating, imageUrl, description);

            return content;
        }
    }
}
