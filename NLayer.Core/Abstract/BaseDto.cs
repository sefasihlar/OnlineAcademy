namespace NLayer.Core.Abstract
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Condition { get; set; }
    }
}
