using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace PrismOutlook.Core.Regions
{
    public class DependentViewRegionBehavior : RegionBehavior
    {
        public const string BehaviorKey = "DependentViewRegionBehavior";
        private readonly IContainerExtension _containerExtension;
        private Dictionary<object, List<DependentViewInfo>> _dependentViewCache = new Dictionary<object, List<DependentViewInfo>>();

        public DependentViewRegionBehavior(IContainerExtension containerExtension)
        {
            _containerExtension = containerExtension;
        }

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        //get the attributes
                        foreach (var newView in e.NewItems)
                        {
                            var dependentViews = new List<DependentViewInfo>();
                            
                            //check if the view already has dependents
                            if (_dependentViewCache.ContainsKey(newView))
                            {
                                //reuse
                                dependentViews = _dependentViewCache[newView];
                            }
                            else
                            {
                                var atts = GetCustomAttributes<DependentViewAttribute>(newView.GetType());

                                foreach (var att in atts)
                                {
                                    // create the view
                                    var info = CreateDependentViewInfo(att);
                                    //check if need to share DataContext bettwen Views
                                    if(info.View is ISupportDataContext infoDC && newView is ISupportDataContext newViewDC)
                                    {
                                        infoDC.DataContext = newViewDC.DataContext;
                                    }

                                    //add to list
                                    dependentViews.Add(info);
                                }
                                _dependentViewCache.Add(newView, dependentViews);
                            }
                            //inject
                            dependentViews.ForEach(item => Region.RegionManager.Regions[item.Region].Add(item.View));

                        }
                        break;
                    }
                case NotifyCollectionChangedAction.Remove:
                    {
                        foreach (var oldView in e.OldItems)
                        {
                            if (_dependentViewCache.ContainsKey(oldView))
                            {
                                var dependentViews = _dependentViewCache[oldView];
                                dependentViews.ForEach(item => Region.RegionManager.Regions[item.Region].Remove(item.View));

                                if (!ShouldKeepAlive(oldView))
                                {
                                    _dependentViewCache.Remove(oldView);
                                }
                            }
                        }
                        break;
                    }
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }

        private bool ShouldKeepAlive(object oldView)
        {
            var regionLifeTime = GetViewOrDataContextLifeTime(oldView);
            //check if oldView have IRegionMemberLifetime
            if (regionLifeTime != null)
            {
                //check if need to keep alive
                return regionLifeTime.KeepAlive;
            }
            //else dont keep alvie
            return true;
        }

        IRegionMemberLifetime GetViewOrDataContextLifeTime(object view)
        {
            if(view is IRegionMemberLifetime regionLifeTime)
            {
                return regionLifeTime;
            }
            if(view is FrameworkElement fe)
            {
                return fe.DataContext as IRegionMemberLifetime;
            }
            return null;
        }

        DependentViewInfo CreateDependentViewInfo(DependentViewAttribute attribute)
        {
            var info = new DependentViewInfo();
            info.Region = attribute.Region;
            info.View = _containerExtension.Resolve(attribute.Type);

            return info;
        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }
}
