using System.ComponentModel.DataAnnotations;

namespace Domain.Advertisement;
public enum AdEventType
{
    [Display(Name = "Hiển thị")]
    Impression,
    [Display(Name = "Nhấn vào")]
    Click
}
