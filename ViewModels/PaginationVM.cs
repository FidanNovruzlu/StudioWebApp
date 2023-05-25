namespace StudioWebApp.ViewModels;
public class PaginationVM<T>
{
    public List<T> Teams { get; set; } = null!;
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
}
