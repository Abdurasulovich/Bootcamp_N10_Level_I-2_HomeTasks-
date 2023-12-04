using AutoMapper;
using Training.FileExplorer.Application.Common.Models.Filtering;
using Training.FileExplorer.Application.Common.Querying.Extention;
using Training.FileExplorer.Application.FileStorage.Brokers;
using Training.FileExplorer.Application.FileStorage.Models.Storage;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Infrastructure.FileStorage.Services;

public class DirectoryService : IDirectoryService
{
    private readonly IDirectoryBroker _directoryBroker;
    private readonly IMapper _mapper;

    public DirectoryService(IDirectoryBroker directoryBroker, IMapper mapper)
    {
        _directoryBroker = directoryBroker;
        _mapper = mapper;
    }
    public IEnumerable<string> GetDirectoriesPath(string directoryPath, FilterPagination paginationOptions) =>
        _directoryBroker.GetFilesPath(directoryPath).ApplyPagination(paginationOptions);

    public IEnumerable<string> GetFilesPath(string filePath, FilterPagination paginationOptions) =>
        _directoryBroker.GetFilesPath(filePath).ApplyPagination(paginationOptions);

    public ValueTask<StorageDirectory?> GetByPathAsync(string directoyPath)
    {
        if(string.IsNullOrWhiteSpace(directoyPath))
            throw new ArgumentNullException(nameof(directoyPath));
        return new ValueTask<StorageDirectory?>(_directoryBroker.GetByPathAsync(directoyPath));
    }

    public async ValueTask<IList<StorageDirectory>> GetDirectoriesAsync(string directotyPath, FilterPagination pagnationOptions)
    {
        if (string.IsNullOrWhiteSpace(directotyPath))
            throw new ArgumentException(nameof(directotyPath));
        var directories = await Task.Run(() => _directoryBroker.GetDirectories(directotyPath).ApplyPagination(pagnationOptions).ToList());

        return directories;
    }


}
