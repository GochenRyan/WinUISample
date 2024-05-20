using System;
using System.Collections.Generic;

namespace Layout.Models
{
    public class Factory : IFactory
    {
        public Factory()
        {
            VisibleDockableControls = new Dictionary<IDockable, IDockableControl>();
        }

        public IDictionary<IDockable, IDockableControl> VisibleDockableControls { get; }

        public void InitActiveDockable(IDockable dockable, IDock owner)
        {
            throw new NotImplementedException();
        }

        public void SetActiveDockable(IDockable dockable)
        {
            if (dockable.Owner is IDock dock)
            {
                dock.ActiveDockable = dockable;
            }
        }
    }
}
