using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Models.Dtos;

public interface IStorageItemDto
{
    string Path { get; set; }

    StorageEntryType ItemType { get; set; }
}
