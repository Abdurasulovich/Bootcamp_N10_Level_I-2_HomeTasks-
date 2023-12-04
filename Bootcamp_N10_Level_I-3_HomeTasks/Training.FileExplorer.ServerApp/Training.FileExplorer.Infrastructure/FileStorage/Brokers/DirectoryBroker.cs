using AutoMapper;
using Training.FileExplorer.Application.FileStorage.Brokers;
using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Infrastructure.FileStorage.Brokers;

public class DirectoryBroker : IDirectoryBroker
{
    private readonly IMapper _mapper;
    public DirectoryBroker(IMapper mapper)
    {
        _mapper = mapper;
    }
    public bool ExistsAsync(string exists)=>Directory.Exists(exists);

    public StorageDirectory GetByPathAsync(string path) =>_mapper.Map<StorageDirectory>(path);

    public IEnumerable<StorageDirectory> GetDirectories(string directoriesPath) => GetDirectoriesPath(directoriesPath)
        .Select(path => _mapper.Map<StorageDirectory>(new DirectoryInfo(path)));

    public IEnumerable<string> GetDirectoriesPath(string directoriesPath) => Directory.EnumerateDirectories(directoriesPath);

    public IEnumerable<string> GetFilesPath(string filesPath) => Directory.EnumerateFiles(filesPath);

}
