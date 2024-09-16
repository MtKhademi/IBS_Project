using Common.Interfaces;
using Core.IDBModule.Abstractions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IDBModule.ValidationServices
{
    public interface IIDBValidationService
        :IValidationInputService<IDBGetFilterDto>
    {
    }
}
