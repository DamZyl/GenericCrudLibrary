using System;

namespace GenericCrud.Models
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}