using System;
using System.ComponentModel.DataAnnotations;

namespace VideoGameManager.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El títol és obligatori")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "El gènere és obligatori")]
        public string Genre { get; set; } = string.Empty;

        [Range(1970, 2030, ErrorMessage = "Any no vàlid")]
        public int Year { get; set; }

        [Range(0, 10, ErrorMessage = "La puntuació ha de ser entre 0 i 10")]
        public double Score { get; set; }

        [Required(ErrorMessage = "La descripció és obligatoria")]
        public string Description { get; set; } = string.Empty;

        // Clau forània cap a Developer
        public int DeveloperId { get; set; }

        //Nova propietat opcional
        public string? Platform { get; set; }

        // Propietat de navegació (No es veu a la taula)
        public Developer? Developer { get; set; }

    }
}
