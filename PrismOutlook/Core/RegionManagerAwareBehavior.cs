using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows;

namespace PrismOutlook.Core
{
    public class RegionManagerAwareBehavior : RegionBehavior
    {
        protected override void OnAttach()
        {
            if(Region.Name == RegionNames.ContentRegion)
            {
                Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
            }
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = Region.RegionManager);
                }
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    InvokeOnRegionManagerAwareElement(item, x => x.RegionManager = null);
                }
            }
        }

        private void InvokeOnRegionManagerAwareElement(object item, Action<IRegionManagerAware> invocation)
        {
            if(item is IRegionManagerAware regionManagerAwareItem)
            {
                invocation(regionManagerAwareItem);
            }
            if(item is FrameworkElement frameworkElement)
            {
                if(frameworkElement.DataContext is IRegionManagerAware regionManagerAwareDataContext)
                {
                    invocation(regionManagerAwareDataContext);
                }
            }
        }
    }
}
