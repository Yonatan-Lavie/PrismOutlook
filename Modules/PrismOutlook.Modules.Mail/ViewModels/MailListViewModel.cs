using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismOutlook.Business;
using PrismOutlook.Core;
using PrismOutlook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailListViewModel : ViewModelBase
    {

        #region Fileds
        private ObservableCollection<MailMessage> _messages;
        private readonly IMailService _mailService;
        private MailMessage _selectedMessage;
        #endregion

        #region Properties
        public ObservableCollection<MailMessage> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }
        public MailMessage SelectedMessage
        {
            get { return _selectedMessage; }
            set { SetProperty(ref _selectedMessage, value); }
        }
        #endregion


        



        public MailListViewModel(IMailService mailService)
        {
            _mailService = mailService;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var folder = navigationContext.Parameters.GetValue<string>(FolderParameters.FolderKey);
            
            // TODO: seggest to move this code to the service.
            switch (folder)
            {
                case FolderParameters.Inbox:
                    {
                        Messages = new ObservableCollection<MailMessage>(_mailService.GetInboxItems());
                        break;
                    }
                case FolderParameters.Sent:
                    {
                        Messages = new ObservableCollection<MailMessage>(_mailService.GetSentItems());
                        break;
                    }
                case FolderParameters.Deleted:
                    {
                        Messages = new ObservableCollection<MailMessage>(_mailService.GetDeletedItems());
                        break;
                    }
                default:
                    break;
            }
            
        }
    }
}
