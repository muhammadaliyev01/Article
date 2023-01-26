using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.Domain.Entites
{
    public class Article
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty; 

        public DateTime Created { get; set; }

        public bool IsUpdated { get; set; }

        public string UpdatedAt { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;
    }
}
