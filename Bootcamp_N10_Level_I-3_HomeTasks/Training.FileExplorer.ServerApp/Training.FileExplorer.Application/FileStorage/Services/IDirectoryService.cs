using Training.FileExplorer.Application.Common.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Application.FileStorage.Services;

public interface IDirectoryService
{
    IEnumerable<string> GetDirectoriesPath(string directoryPath, FilterPagination paginationOptions);

    IEnumerable<string> GetFilesPath(string filePath, FilterPagination paginationOptions);

    ValueTask<IList<StorageDirectory>> GetDirectoriesAsync(string directotyPath, FilterPagination pagnationOptions);

    ValueTask<StorageDirectory?> GetByPathAsync(string directoyPath);
}
