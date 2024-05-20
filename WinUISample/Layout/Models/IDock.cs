using System.Collections.Generic;

namespace Layout.Models
{
    public interface IDock : IDockable
    {
        IList<IDockable>? VisibleDockables { get; set; }
        IDockable? ActiveDockable { get; set; }
    }
}
