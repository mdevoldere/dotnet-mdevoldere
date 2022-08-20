using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDevoldere.Domain
{
    public abstract class Model
    {
        private static int uid = 0;

        protected static int NewUid { get => ++uid; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; } = 0;
    }
}
