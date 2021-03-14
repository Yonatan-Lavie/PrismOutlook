using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismOutlook.Core
{
    public class ApplicationCommands : IApplicationCommands
    {
        public CompositeCommand NavigateCommand { get; } = new CompositeCommand();
    }
}
