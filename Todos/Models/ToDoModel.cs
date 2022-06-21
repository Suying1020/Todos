using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todos.Models
{
    public class ToDoModel
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "ItemName is required")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "ItemDescription is required")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "ItemStatus is required")]
        public bool ItemStatus { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser IdentityUser { get; set; }
    }
}
