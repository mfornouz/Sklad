using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DataEntity;

public abstract class BaseModel : IDataErrorInfo, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
        if (propertyName != nameof(IsValid))
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(IsValid)));
    }

    public IReadOnlyList<ValidationResult> GetValidationResults()
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(this, new ValidationContext(this),
            results, validateAllProperties: true);
        return results;
    }

    [NotMapped]
    public bool IsValid => GetValidationResults().Count == 0;

    string IDataErrorInfo.Error => string.Join(Environment.NewLine,
        GetValidationResults().Select(result => result.ErrorMessage));

    string IDataErrorInfo.this[string propertyName] =>
        string.Join(Environment.NewLine, GetValidationResults()
            .Where(result => result.MemberNames.Contains(propertyName))
            .Select(result => result.ErrorMessage));

    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}


