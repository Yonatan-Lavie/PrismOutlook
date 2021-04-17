using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismOutlook.Business;
using PrismOutlook.Core;
using PrismOutlook.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MailListViewModel : ViewModelBase
    {
        #region Services
        private readonly IMailService _mailService;
        private readonly IRegionDialogService _regionDialogService;
        #endregion

        #region Filed
        private ObservableCollection<Business.MailMessage> _messages = new ObservableCollection<Business.MailMessage>();
        private Business.MailMessage _selectedMessage;
        private string _currentFolder = FolderParameters.Inbox;
        #endregion

        #region Properties
        public Business.MailMessage SelectedMessage
        {
            get { return _selectedMessage; }
            set { SetProperty(ref _selectedMessage, value); }
        }
        public ObservableCollection<Business.MailMessage> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }
        #endregion

        #region Commands

        #region Filed
        private DelegateCommand<string> _messageCommand;
        private DelegateCommand _deleteMessageCommand;
        private DelegateCommand _newMessageCommand;
        #endregion

        #region Properties
        public DelegateCommand NewMessageCommand =>
            _newMessageCommand ?? (_newMessageCommand = new DelegateCommand(ExecuteNewMessageCommand));
        
        public DelegateCommand<string> MessageCommand =>
             _messageCommand ?? (_messageCommand = new DelegateCommand<string>(ExecuteMessageCommand));

        public DelegateCommand DeleteMessageCommand =>
            _deleteMessageCommand ?? (_deleteMessageCommand = new DelegateCommand(ExecuteDeleteMessageCommand));


        #endregion

        #region Headnles
        void ExecuteNewMessageCommand()
        {
            var parameters = new DialogParameters();
            parameters.Add("id", 0);

            _regionDialogService.Show("MessageView", parameters, (result) =>
            {
                if (_currentFolder == FolderParameters.Sent)
                    Messages.Add(result.Parameters.GetValue<MailMessage>("messageSent"));
            });
        }
        void ExecuteDeleteMessageCommand()
        {
            if (SelectedMessage == null)
                return;

            _mailService.DeleteMessage(SelectedMessage.Id);

            Messages.Remove(SelectedMessage);
        }

        void ExecuteMessageCommand(string parameter)
        {
            if (SelectedMessage == null)
                return;

            var parameters = new DialogParameters();
            var viewName = "MessageView";
            MessageMode replyType = MessageMode.Read;

            switch (parameter)
            {
                case nameof(MessageMode.Read):
                    {
                        viewName = "MessageReadOnlyView";
                        replyType = MessageMode.Read;
                        break;
                    }
                case nameof(MessageMode.Reply):
                    {
                        replyType = MessageMode.Reply;
                        break;
                    }
                case nameof(MessageMode.ReplyAll):
                    {
                        replyType = MessageMode.ReplyAll;
                        break;
                    }
                case nameof(MessageMode.Forward):
                    {
                        replyType = MessageMode.Forward;
                        break;
                    }

            }


            parameters.Add(MailParameters.MessageId, SelectedMessage.Id);
            parameters.Add(MailParameters.MessageMode, replyType);

            _regionDialogService.Show(viewName, parameters, (result) =>
            {

            });
        }


        #endregion

        #endregion


        #region Constractor
        public MailListViewModel(IMailService mailService, IRegionDialogService regionDialogService)
        {
            _mailService = mailService;
            _regionDialogService = regionDialogService;
        } 
        #endregion

        #region Navigation Aware
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            _currentFolder = navigationContext.Parameters.GetValue<string>(FolderParameters.FolderKey);

            LoadMessages(_currentFolder);

            SelectedMessage = Messages.FirstOrDefault();
        } 
        #endregion

        void LoadMessages(string folder)
        {
            switch (folder)
            {
                case FolderParameters.Inbox:
                    {
                        Messages = new ObservableCollection<Business.MailMessage>(_mailService.GetInboxItems());
                        break;
                    }
                case FolderParameters.Sent:
                    {
                        Messages = new ObservableCollection<Business.MailMessage>(_mailService.GetSentItems());
                        break;
                    }
                case FolderParameters.Deleted:
                    {
                        Messages = new ObservableCollection<Business.MailMessage>(_mailService.GetDeletedItems());
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
