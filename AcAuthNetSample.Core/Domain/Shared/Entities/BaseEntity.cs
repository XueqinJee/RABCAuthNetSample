using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AcAuthNetSample.Core.Domain.Shared.Entities {
    public class BaseEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("主键")]
        public int Id { get; set; }

        [Comment("创建时间")]
        public DateTimeOffset CreateOn { get; set; } = DateTimeOffset.UtcNow;

        [Comment("修改时间")]
        public DateTimeOffset UpdateOn { get; set; }

        [Comment("建立人")]
        public int CreatedById { get; set; }

        [Comment("修改人")]
        public int UpdatedById { get; set; }
    }
}
