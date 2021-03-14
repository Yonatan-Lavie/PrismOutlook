using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismOutlook.Core
{
    public interface IApplicationCommands
    {
        CompositeCommand NavigateCommand { get; }
    }
}
