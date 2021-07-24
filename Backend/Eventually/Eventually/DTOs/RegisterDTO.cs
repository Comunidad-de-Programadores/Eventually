using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventually.DTOs
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        public string UserName { get; set; }

        public List<int> Areas { get; set; }

    }
}
