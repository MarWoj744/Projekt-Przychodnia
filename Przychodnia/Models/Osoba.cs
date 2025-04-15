﻿using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Models
{

    public enum Rola
    {
        Pacjent,
        Recepcjonistka,
        Lekarz
    }

    public abstract class Osoba
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Imie { get; set; }

        [Required]
        [StringLength(50)]
        public string Nazwisko { get; set; }

        [Required]
        [StringLength(100)]
        public string Adres { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Telefon { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Haslo { get; set; }

        [Required]
        public Rola Rola { get; set; }
    }
}
