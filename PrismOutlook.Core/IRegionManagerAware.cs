using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismOutlook.Core
{
    public interface IRegionManagerAware
    {
        IRegionManager RegionManager { get; set; }
    }
}
