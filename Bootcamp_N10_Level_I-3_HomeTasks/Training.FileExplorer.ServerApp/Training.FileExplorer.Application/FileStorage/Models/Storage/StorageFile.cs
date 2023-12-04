using System.ComponentModel.DataAnnotations;

namespace Training.FileExplorer.Application.FileStorage.Models.Storage;

public class StorageFile : IStorageEntry
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;

    public string DirectoryPath { get; set; } = string.Empty;

    public long Size { get; set; }

    public string Extention { get; set; } = string.Empty;

    public StorageEntryType EntryType { get; set; } = StorageEntryType.File;
}
