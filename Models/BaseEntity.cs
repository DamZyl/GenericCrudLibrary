using System;

namespace GenericCrud.Models
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
    }
}