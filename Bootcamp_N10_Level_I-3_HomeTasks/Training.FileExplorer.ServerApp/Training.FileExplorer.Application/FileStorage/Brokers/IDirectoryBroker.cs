using Training.FileExplorer.Application.FileStorage.Models.Storage;

namespace Training.FileExplorer.Application.FileStorage.Brokers;

public interface IDirectoryBroker
{
    IEnumerable<string> GetDirectoriesPath(string directoriesPath);

    IEnumerable<string> GetFilesPath(string filesPath);

    IEnumerable<StorageDirectory> GetDirectories(string directoriesPath);
    
    StorageDirectory GetByPathAsync(string path);

    bool ExistsAsync(string exists);

    //bool SetAccessControl(string accessControl);
}
