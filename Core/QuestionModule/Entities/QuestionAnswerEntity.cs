using Common.Entities;
using Core.UserManagement.Entities;

namespace Core.QuestionModule.Entities;

public class QuestionAnswerEntity : BaseEntity<int>
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public UserEntity User { get; set; }


    public int QuestionId { get; set; }
    public QuestionEntity Question { get; set; }

    public int Degree { get; set; }
}
