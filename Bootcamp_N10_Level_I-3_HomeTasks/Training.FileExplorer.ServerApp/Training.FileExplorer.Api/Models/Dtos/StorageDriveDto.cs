using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Api.Models.Dtos;

public class StorageDriveDto
{
    public string Name { get; set; } = String.Empty;

    public string Path { get; set; } = String.Empty;

    public string Format { get; set; } = String.Empty;

    public string Type { get; set; } = String.Empty;

    public long TotalSpace { get; set; }

    public long FreeSpace { get; set; }

    public long UnavailableSpace { get; set; }

    public long UsedSpace { get; set; }

    public StorageEntryType ItemType { get; set; } = StorageEntryType.Drive;
}
