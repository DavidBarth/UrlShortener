using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            #region .   DTO Database to Model   .

            CreateMap<DTO.API.UrlRequestDTO, Models.Url>();
            CreateMap<DTO.API.UrlResponseDTO, Models.Url>()
                .ForMember(model => model.ShortUrl, opt =>  opt.MapFrom(dto => dto.ShortUrl.ToString()));

            #endregion

            #region .   Model to DTO Database.

            CreateMap<Models.Url, DTO.API.UrlRequestDTO>();
            CreateMap<Models.Url, DTO.API.UrlResponseDTO>()
                      .ForMember(dto => dto.ShortUrl, opt =>  opt.MapFrom(model =>  new Uri(model.ShortUrl)));

            #endregion

        }
    }
}