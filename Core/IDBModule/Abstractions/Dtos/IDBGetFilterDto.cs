﻿using Core.IDBModule.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IDBModule.Abstractions.Dtos
{
    public class IDBGetFilterDto
    {
        public ETypeOfIDBType? TypeOfIDB { get; set; }
    }
}
