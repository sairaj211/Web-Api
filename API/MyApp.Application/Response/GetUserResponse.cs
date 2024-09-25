using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Response
{
    public class GetUserResponse
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; } = null!;
    }
}
