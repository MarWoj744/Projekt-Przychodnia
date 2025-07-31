using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        [StringLength(255)]
        public string StareHaslo { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$")]
        public string NoweHaslo { get; set; }

        [Required]
        [Compare("NoweHaslo")]
        public string PotwierdzNoweHaslo { get; set; }
    }
}
