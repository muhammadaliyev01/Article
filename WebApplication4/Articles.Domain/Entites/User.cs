using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.Domain.Entites
{
    public class User
    {
        public long Id { get; set; }

        public string FullName { get; set; }=string.Empty;

        public string Email { get; set; }=string.Empty;

        public string PasswordHash { get; set; }=string.Empty;

        public string Salt { get; set; }=string.Empty;

        public string ImagePath { get; set; }=string.Empty;

        public DateTime Created { get; set; }
    }
}
