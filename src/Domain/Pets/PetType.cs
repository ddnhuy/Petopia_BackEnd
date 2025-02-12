using System.ComponentModel.DataAnnotations;

namespace Domain.Pets;
public enum PetType
{
    [Display(Name = "Chó")]
    Dog,
    [Display(Name = "Mèo")]
    Cat,
    [Display(Name = "Chim")]
    Bird,
    [Display(Name = "Cá")]
    Fish,
    [Display(Name = "Thỏ")]
    Rabbit,
    [Display(Name = "Hamster")]
    Hamster,
    [Display(Name = "Rùa")]
    Turtle,
    [Display(Name = "Chuột Lang")]
    GuineaPig,
    [Display(Name = "Khác")]
    Other
}
