using System;

namespace GenericCrud.Models
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}