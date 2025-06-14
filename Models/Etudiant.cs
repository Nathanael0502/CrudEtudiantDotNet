using System.ComponentModel.DataAnnotations;

namespace MonProjetMVC.Models
{
    public class Etudiant
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est requis.")]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "Adresse email invalide.")]
        public string Email { get; set; }

        // Chemin de la photo enregistrée
        public string? PhotoPath { get; set; }
    }
}
