using System;
using System.ComponentModel.DataAnnotations;

namespace Dislinkt.Notifications.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
