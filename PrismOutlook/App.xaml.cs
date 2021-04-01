using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PrismOutlook.Modules.Mail;
using PrismOutlook.Core.Regions;
using PrismOutlook.Views;
using System.Windows;
using PrismOutlook.Modules.Contacts;
using PrismOutlook.Core;
using Infragistics.Themes;
using PrismOutlook.Services.Interfaces;
using PrismOutlook.Core.Dialogs;

namespace PrismOutlook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(Window shell)
        {
            Infragistics.Themes.ThemeManager.ApplicationTheme = new Office2013Theme();
            base.InitializeShell(shell);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<IMyDialogService, MyDialogService>();

            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();

            //containerRegistry.RegisterDialogWindow<RibbonWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MailModule>();
            moduleCatalog.AddModule<ContactsModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(XamOutlookBar), Container.Resolve<XamOutlookBarRegionAddapter>());
            regionAdapterMappings.RegisterMapping(typeof(XamRibbon), Container.Resolve<XamRibbonRegionAddapter>());
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            regionBehaviors.AddIfMissing(DependentViewRegionBehavior.BehaviorKey, typeof(DependentViewRegionBehavior));
        }
    }
}
