//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using AutoMapper;
using Core.QuestionModule.Abstractions.Dtos;
using Core.QuestionModule.Entities;

namespace Core.QuestionModule.MapperServices;

internal class MapperConfig
{
    #region User Mapper Config
    public class QuestionMapperConfig : Profile
    {
        public QuestionMapperConfig()
        {
            CreateMap<QuestionEntity, QuestionGetDto>();
        }
    }

    #endregion Private Helper
}
