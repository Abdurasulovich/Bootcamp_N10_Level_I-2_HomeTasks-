using Training.FileExplorer.Application.Common.Models.Filtering;

namespace Training.FileExplorer.Application.FileStorage.Models.Filtering;

public class StorageFileFilterDataModel
{
    public List<StorageFileSummary> FilterData { get; set; } = new();
}