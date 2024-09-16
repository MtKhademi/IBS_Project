using Core.IDBModule.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IDBModule.Abstractions.Dtos
{
    public class IDBGetDto
    {
        public ETypeOfIDBType TypeOfIDB { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
