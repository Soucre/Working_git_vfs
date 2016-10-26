using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwipeJob.Model
{
    public class Industry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}