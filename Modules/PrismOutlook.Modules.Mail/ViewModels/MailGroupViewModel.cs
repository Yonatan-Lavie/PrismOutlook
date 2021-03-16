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

            var root = new NavigationItem() { Caption = "Personal Folder", NavigationPath = "MailList?id=Default" };
            root.Items.Add(new NavigationItem() { Caption = "Inbox", NavigationPath = "MailList?id=Inbox" });
            root.Items.Add(new NavigationItem() { Caption = "Deleted", NavigationPath = "MailList?id=Deleted" });
            root.Items.Add(new NavigationItem() { Caption = "Sent", NavigationPath = "MailList?id=Sent" });

            Items.Add(root);
        }
    }
}
