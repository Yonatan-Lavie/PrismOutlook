using Prism.Regions;
using Infragistics.Windows.Ribbon;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismOutlook.Core.Regions
{
    public class XamRibbonRegionAddapter : RegionAdapterBase<XamRibbon>
    {
        public XamRibbonRegionAddapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, XamRibbon regionTarget)
        {
            region.Views.CollectionChanged += ((s,e) => {
                switch (e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        {
                            foreach(var view in e.NewItems)
                            {
                                AddViewToRegion(view, regionTarget);
                            }
                            break;
                        }
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        {
                            foreach (var view in e.OldItems)
                            {
                                RemoveViewFromRegion(view, regionTarget);
                            }
                            break;
                        }
                    default:
                        break;
                }
            });
        }

        private void RemoveViewFromRegion(object view, XamRibbon regionTarget)
        {
            if (view is RibbonTabItem ribbonTabItem)
            {
                regionTarget.Tabs.Remove(ribbonTabItem);
            }
        }

        private void AddViewToRegion(object view, XamRibbon regionTarget)
        {
            if (view is RibbonTabItem ribbonTabItem)
            {
                regionTarget.Tabs.Add(ribbonTabItem);
            }
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
