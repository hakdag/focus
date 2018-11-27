using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Focus.Common
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        [NotMapped]
        public bool InsertNew { get; set; } = false;
    }
}
