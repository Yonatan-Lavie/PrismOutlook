using Prism.Commands;
using Prism.Mvvm;
using PrismOutlook.Business;
using PrismOutlook.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailGroupViewModel : ViewModelBase
    {

        #region fileds
        private readonly IApplicationCommands _applicationCommands;
        private ObservableCollection<NavigationItem> _items;
        private DelegateCommand<NavigationItem> _selectedCommand;
        #endregion

        #region Properties
        public ObservableCollection<NavigationItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
        public DelegateCommand<NavigationItem> SelectedCommand =>
            _selectedCommand ?? (_selectedCommand = new DelegateCommand<NavigationItem>(ExecuteSelectedCommand));
        #endregion


        #region Constractors
        public MailGroupViewModel(IApplicationCommands applicationCommands)
        {
            GenerateMenu();
            _applicationCommands = applicationCommands;
        } 
        #endregion

        #region Heandlers
        private void ExecuteSelectedCommand(NavigationItem navigationItem)
        {
            if(navigationItem != null)
            {
                _applicationCommands.NavigateCommand.Execute(navigationItem.NavigationPath);
            }
        }
        #endregion

        void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();

            var root = new NavigationItem() { Caption = "Personal Folder", NavigationPath = "MailList?id=Default" , IsExpanded=true};
            root.Items.Add(new NavigationItem() { Caption = FolderParameters.Inbox, NavigationPath = GetNavigationPath(FolderParameters.Inbox) });
            root.Items.Add(new NavigationItem() { Caption = FolderParameters.Deleted, NavigationPath = GetNavigationPath(FolderParameters.Deleted) });
            root.Items.Add(new NavigationItem() { Caption = FolderParameters.Sent, NavigationPath = GetNavigationPath(FolderParameters.Sent) });

            Items.Add(root);
        }
        // TODO: dont like this approche 
        string GetNavigationPath(string folder)
        {
            return $"MailList?{FolderParameters.FolderKey}={folder}";
        }
    }
    // TODO: dont like this approche 
    public class FolderParameters
    {
        public const string FolderKey = "Folder";

        public const string Inbox = "Inbox";
        public const string Deleted = "Deleted";
        public const string Sent = "Sent";
    }
}
