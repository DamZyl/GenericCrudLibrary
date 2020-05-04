using System;

namespace GenericCrud.Auth
{
    public interface IJwtHandler
    {
        string CreateToken(Guid userId, string fullName, string role);
    }
}