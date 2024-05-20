using System.Collections.Generic;

namespace Layout.Models
{
    public interface IFactory
    {
        IDictionary<IDockable, IDockableControl> VisibleDockableControls { get; }
        void SetActiveDockable(IDockable dockable);

        void InitActiveDockable(IDockable? dockable, IDock owner);
    }
}
