﻿using System.ComponentModel.DataAnnotations;

namespace APICondSecurity.DTOs
{
    public class CamerasDTO
    {
        public int IdCamera { get; set; }

        [Required]
        [StringLength(20)]
        public string Posicao { get; set; }

        [Required]
        [StringLength(20)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(30)]
        public string IpCamera { get; set; }
    }
}
