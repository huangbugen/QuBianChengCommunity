using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BBSSystem.Application.Contracts.AreaApp.Dtos;
using BBSSystem.Application.Contracts.PostApp.Dtos;
using BBSSystem.Application.Contracts.ReplyApp.Dtos;
using BBSSystem.Application.Contracts.SectionApp.Dtos;
using BBSSystem.Domain.PostInfo;

namespace BBSSystem.Application
{
    public class BBSSystemProfile : Profile
    {
        public BBSSystemProfile()
        {
            CreateMap<Area, AreaDto>();
            CreateMap<Section, SectionDto>();
            CreateMap<SectionLordUserMapping, SectionLordUserMappingDto>();
            CreateMap<Section, SectionSimpleDto>();
            CreateMap<Post, PostDto>();
            CreateMap<PostCreateDto, Post>().ConstructUsing(m => new Post(Guid.NewGuid().ToString("N")));
            CreateMap<ReplyCreateDto, Reply>().ConstructUsing(m => new Reply(Guid.NewGuid().ToString("N")));
            CreateMap<Post, PostListDisplayDto>();
            CreateMap<PostType, PostTypeDto>();
            CreateMap<ReplyCreateOnlyDto, Reply>().ConstructUsing(m => new Reply(Guid.NewGuid().ToString("N")));
            CreateMap<ReplyUpdateContentDto, Reply>();
            CreateMap<Post, PostInDetailPageDto>().ForMember(dest => dest.PostTypeName, opts => opts.MapFrom(src => src.PostType.PostTypeName));
            CreateMap<Reply, ReplyDto>();
        }
    }
}