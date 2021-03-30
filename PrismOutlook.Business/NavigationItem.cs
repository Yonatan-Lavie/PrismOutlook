﻿using System;
using System.Collections.ObjectModel;

namespace PrismOutlook.Business
{
    public class NavigationItem
    {
        public string Caption { get; set; }
        public string NavigationPath { get; set; }
        // false by default
        public bool IsExpanded { get; set; }

        public ObservableCollection<NavigationItem> Items { get; set; }

        public NavigationItem()
        {
            Items = new ObservableCollection<NavigationItem>();
        }
    }
}
