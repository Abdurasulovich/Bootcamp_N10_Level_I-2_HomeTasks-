using Microsoft.Extensions.Options;
using Training.FileExplorer.Application.Common.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Brokers;
using Training.FileExplorer.Application.FileStorage.Models.Filtering;
using Training.FileExplorer.Application.FileStorage.Models.Settings;
using Training.FileExplorer.Application.FileStorage.Models.Storage;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Infrastructure.FileStorage.Services;

public class FileService : IFileService
{
    private readonly FileFilterSettings _fileFilterSettings;
    private readonly FileStorageSettings _fileStorageSettings;
    private readonly IFIleBroker _broker;

    public FileService(IFIleBroker broker, 
        IOptions<FileStorageSettings> fileStorageSettings, 
        IOptions<FileFilterSettings> fileFilterSettings)
    {
        _broker = broker;
        _fileFilterSettings = fileFilterSettings.Value;
        _fileStorageSettings = fileStorageSettings.Value;

    }
    public async ValueTask<IList<StorageFile>> GetFilesByPathAsync(IEnumerable<string> filesPath)
    {
        var files = await Task.Run(() => { return filesPath.Select(filePath => _broker.GetByPath(filePath)).ToList(); });
        return files;
    }
    public ValueTask<StorageFile> GetFileByPathAsync(string filePath) =>
        !string.IsNullOrWhiteSpace(filePath)
        ? new ValueTask<StorageFile>(_broker.GetByPath(filePath))
        : throw new ArgumentException(nameof(filePath));

    public IEnumerable<StorageFileSummary> GetFilesSummary(IEnumerable<StorageFile> files)
    {
        var filesType = files.Select(file => (File: file, Type: GetFileType(file.Path)));
        return filesType.GroupBy(file => file.Type)
            .Select(filesGroups => new StorageFileSummary
            {
                FileType = filesGroups.Key,
                DisplayName = _fileFilterSettings.FileExtensions
                                  .FirstOrDefault(extension => extension.FileType == filesGroups.Key)?.DisplayName ??
                              "Other files",
                Count = filesGroups.Count(),
                Size = filesGroups.Sum(file => file.File.Size),
                ImageUrl = _fileFilterSettings.FileExtensions
                               .FirstOrDefault(extension => extension.FileType == filesGroups.Key)?.ImageUrl ??
                           _fileStorageSettings.FileImgUrl
            });
    }

    public StorageFileType GetFileType(string filePath)
    {
        var fileExtention = Path.GetExtension(filePath).TrimStart('.');
        var matchedFileType =
            _fileFilterSettings.FileExtensions.FirstOrDefault(extentions =>
                extentions.Extensions.Contains(fileExtention));
        return matchedFileType?.FileType ?? StorageFileType.Other;
    }
}
