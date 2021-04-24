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
    public class MailListViewModel : MessageViewModelBase //ViewModelBase
    {
        #region Services

        #endregion

        #region Filed
        private ObservableCollection<MailMessage> _messages = new ObservableCollection<MailMessage>();
        private string _currentFolder = FolderParameters.Inbox;
        #endregion

        #region Properties
        public ObservableCollection<MailMessage> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }
        #endregion

        #region Commands

        #region Filed
        private DelegateCommand _newMessageCommand;
        #endregion

        #region Properties
        public DelegateCommand NewMessageCommand =>
            _newMessageCommand ?? (_newMessageCommand = new DelegateCommand(ExecuteNewMessageCommand));
        
        #endregion

        #region Headnles
        void ExecuteNewMessageCommand()
        {
            var parameters = new DialogParameters();
            parameters.Add("id", 0);

            RregionDialogService.Show("MessageView", parameters, (result) =>
            {
                if (_currentFolder == FolderParameters.Sent)
                    Messages.Add(result.Parameters.GetValue<MailMessage>("messageSent"));
            });
        }

        #endregion

        #endregion

        protected override void ExecuteDeleteMessageCommand()
        {
            base.ExecuteDeleteMessageCommand();
            Messages.Remove(Message);
        }
        protected override void HandleMessageCallBack(IDialogResult result)
        {
            var mode = result.Parameters.GetValue<MessageMode>(MailParameters.MessageMode);

            if (mode == MessageMode.Delete)
            {
                var messageId = result.Parameters.GetValue<int>(MailParameters.MessageId);
                var messagesToDelete = Messages.Where(x => x.Id == messageId).FirstOrDefault();
                if (messagesToDelete != null)
                    Messages.Remove(messagesToDelete);
            }
        }

        #region Constractor
        public MailListViewModel(IMailService mailService, IRegionDialogService regionDialogService):
            base(mailService, regionDialogService)
        {

        } 
        #endregion

        #region Navigation Aware
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            _currentFolder = navigationContext.Parameters.GetValue<string>(FolderParameters.FolderKey);

            LoadMessages(_currentFolder);

            Message = Messages.FirstOrDefault();
        } 
        #endregion

        void LoadMessages(string folder)
        {
            switch (folder)
            {
                case FolderParameters.Inbox:
                    {
                        Messages = new ObservableCollection<MailMessage>(MailService.GetInboxItems());
                        break;
                    }
                case FolderParameters.Sent:
                    {
                        Messages = new ObservableCollection<MailMessage>(MailService.GetSentItems());
                        break;
                    }
                case FolderParameters.Deleted:
                    {
                        Messages = new ObservableCollection<MailMessage>(MailService.GetDeletedItems());
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
