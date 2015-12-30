using System;
using System.ComponentModel.DataAnnotations;

namespace AMF.Core.Model
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? Archived { get; set; }

        protected Entity()
        {
            CreationDate = DateTime.UtcNow;
        }
    }

}