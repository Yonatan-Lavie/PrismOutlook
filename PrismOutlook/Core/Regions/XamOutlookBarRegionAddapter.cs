using System;
using System.Collections.Generic;
using System.Text;
using Prism.Regions;
using Infragistics.Windows.OutlookBar;

namespace PrismOutlook.Core.Regions
{
    public class XamOutlookBarRegionAddapter : RegionAdapterBase<XamOutlookBar>
    {
        public XamOutlookBarRegionAddapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, XamOutlookBar regionTarget)
        {
            region.Views.CollectionChanged += ((x, y) =>
           {
               switch (y.Action)
               {
                   case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                       {
                           foreach (OutlookBarGroup group in y.NewItems)
                           {
                               regionTarget.Groups.Add(group);
                               
                               if(regionTarget.Groups[0] == group)
                               {
                                   regionTarget.SelectedGroup = group;
                               }
                           }
                           break;
                       }
                   case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                       {
                           foreach(OutlookBarGroup group in y.OldItems)
                           {
                               regionTarget.Groups.Remove(group);
                           }
                           break;
                       }

                   default:
                       break;
               }
           });
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
