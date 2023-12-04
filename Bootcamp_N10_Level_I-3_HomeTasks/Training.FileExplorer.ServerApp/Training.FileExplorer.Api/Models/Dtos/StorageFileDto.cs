using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Models.Dtos;

public class StorageFileDto
{
    public string Name { get; set; } = String.Empty;

    public string Path { get; set; } = String.Empty;

    public string DirectoryPath { get; set; } = String.Empty;

    public long Size { get; set; }

    public string Extention { get; set; } = String.Empty;

    public StorageEntryType ItemType { get; set; } = StorageEntryType.File;
}
