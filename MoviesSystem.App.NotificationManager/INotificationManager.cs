﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.App.NotificationManager
{
    public interface INotificationManager
    {
        public Task NotifyAboutUnwatchedMovies();
    }
}
