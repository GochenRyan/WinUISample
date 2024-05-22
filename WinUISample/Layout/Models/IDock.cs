using System.Collections.Generic;

namespace Layout.Models
{
    public interface IDock : IDockable
    {
        IDockable? ActiveDockable { get; set; }

        IList<IDockable>? GetVisibleDockables();
    }
}
