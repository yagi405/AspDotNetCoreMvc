using System;

namespace ChatApp2021.Infrastructure.Entities
{
    public record ChatLog(
        int Id,
        DateTime PostAt,
        string Message,
        string UserId
    );
}
