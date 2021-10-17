using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSystem.Domain.Repositories
{
    public interface IWatchListRepository
    {
        void GetWatchListItems(int userId);
    }
}
