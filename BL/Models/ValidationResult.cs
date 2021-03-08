using System.Collections.Generic;

namespace BL
{
    public class ValidationResult
    {
        public List<PropertyError> Errors { get; set; } = new List<PropertyError>();
        public bool IsSuccess => Errors.Count == 0;
    }

    public class PropertyError
    {
        public PropertyError(string property, string errorMessage)
        {
            Name = property;
            Message = errorMessage;
        }
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
