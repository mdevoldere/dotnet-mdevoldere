using System.ComponentModel.DataAnnotations;

namespace MDevoldere.Domain.Models
{
    abstract public class Currency : Model
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public float Value { get; set; }

        public Currency(string name, float value = 1)
        {
            Id = NewUid;
            Name = name;
            Value = value;
        }
    }
}
