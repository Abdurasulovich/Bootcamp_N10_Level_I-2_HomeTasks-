using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Models.Dtos;

public class StorageDirectoryDto
{
    public string Name { get; set; } = String.Empty;

    public string Path { get; set; } = String.Empty;

    public long ItemsCount { get; set; }

    public StorageEntryType ItemType { get; set; }
}
