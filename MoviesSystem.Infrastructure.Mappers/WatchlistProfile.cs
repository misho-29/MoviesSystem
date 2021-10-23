using AutoMapper;
using MoviesSystem.Domain.Models;
using MoviesSystem.Domain.Models.Responses;
using System;
using Db = MoviesSystem.Infrastructure.Store.Models;

namespace MoviesSystem.Infrastructure.Mappers
{
    public class WatchlistProfile : Profile
    {
        public WatchlistProfile()
        {
            CreateMap<Db.WatchListItem, WatchlistItemResponse>();
        }
    }
}
