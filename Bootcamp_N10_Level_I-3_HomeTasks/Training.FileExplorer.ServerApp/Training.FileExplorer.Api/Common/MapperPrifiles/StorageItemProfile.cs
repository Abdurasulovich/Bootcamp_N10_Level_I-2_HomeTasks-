using AutoMapper;
using Training.FileExplorer.Api.Models.Dtos;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Common.MapperPrifiles
{
    public class StorageItemProfile : Profile
    {
        public StorageItemProfile()
        {
            CreateMap<IStorageEntry, IStorageItemDto>();
            CreateMap<IStorageItemDto, IStorageEntry>();
        }
    }
}
