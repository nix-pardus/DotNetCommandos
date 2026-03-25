using System.ComponentModel.DataAnnotations;

namespace UI.Models.Schedule;

public class ScheduleFormModel : IValidatableObject
{
    [Required(ErrorMessage = "График обязателен")]
    [RegularExpression(@"^\d+(-|\/)\d+$", ErrorMessage = "Недопустимый формат. Используйте форматы: 5/2, 2/2 или подобный.")]
    public string Pattern { get; set; } = "5/2";

    [Required(ErrorMessage = "Дата начала обязательна")]
    public DateTime? EffectiveFromDate { get; set; }

    public DateTime? EffectiveToDate { get; set; }

    [Required(ErrorMessage = "Время начала обязательно")]
    public TimeSpan? StartTime { get; set; }

    [Required(ErrorMessage = "Время окончания обязательно")]
    public TimeSpan? EndTime { get; set; }

    public Guid EmployeeId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(StartTime.HasValue && EndTime.HasValue && StartTime >= EndTime)
        {
            yield return new ValidationResult(
                "Время начала должно быть меньше времени окончания.",
                new[] {nameof(StartTime),  nameof(EndTime)});
        }

        if(EffectiveFromDate.HasValue && EffectiveToDate.HasValue && EffectiveFromDate > EffectiveToDate)
        {
            yield return new ValidationResult(
                "Дата начала не может быть позже даты окончания.",
                new[] { nameof(EffectiveFromDate), nameof(EffectiveToDate) });
        }
    }
}
