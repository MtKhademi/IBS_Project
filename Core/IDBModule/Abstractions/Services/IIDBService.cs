using Common.Interfaces;
using Core.IDBModule.Abstractions.Dtos;

namespace Core.IDBModule.Abstractions.Services;

public interface IIDBService
    : IGetsByFilterServiceAsync<IDBGetDto, IDBGetFilterDto>
{
    Task AddWithExcellFileAsync(string filePath);
}
