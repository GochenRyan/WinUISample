namespace Layout.Models
{
    public interface IDockable
    {
        IDockable? Owner { get; set; }
    }
}
