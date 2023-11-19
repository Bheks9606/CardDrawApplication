using AutoMapper;
using DataInterface.DTOs.CardDealing;
using DataInterface.DTOs.Player;
using DataInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            //legal mappings configurations
            CreateMap<Player,RegisterPlayerDto>().ReverseMap();
        }
    }
}
