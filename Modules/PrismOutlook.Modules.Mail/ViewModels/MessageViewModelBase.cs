using Prism.Commands;
using Prism.Services.Dialogs;
using PrismOutlook.Business;
using PrismOutlook.Core;
using PrismOutlook.Services.Interfaces;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MessageViewModelBase : ViewModelBase
    {

        #region Services
        protected IMailService MailService { get; private set; }
        protected IRegionDialogService RregionDialogService { get; private set; }
        #endregion

        #region Fileds
        private MailMessage _message;
        #endregion

        #region Properties
        public MailMessage Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        #endregion

        #region constractor
        public MessageViewModelBase(IMailService mailService, IRegionDialogService regionDialogService)
        {
            MailService = mailService;
            RregionDialogService = regionDialogService;
        }
        #endregion

        #region Commands

        #region Filed
        private DelegateCommand<string> _messageCommand;
        private DelegateCommand _deleteMessageCommand;
        
        #endregion

        #region Properties
        public DelegateCommand<string> MessageCommand =>
                _messageCommand ?? (_messageCommand = new DelegateCommand<string>(ExecuteMessageCommand));
        public DelegateCommand DeleteMessageCommand =>
                _deleteMessageCommand ?? (_deleteMessageCommand = new DelegateCommand(ExecuteDeleteMessageCommand));
        #endregion

        #region Headnles
        protected virtual void ExecuteDeleteMessageCommand()
        {
            if (Message == null)
                return;

            MailService.DeleteMessage(Message.Id);

        }
        void ExecuteMessageCommand(string parameter)
        {
            if (Message == null)
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


            parameters.Add(MailParameters.MessageId, Message.Id);
            parameters.Add(MailParameters.MessageMode, replyType);

            RregionDialogService.Show(viewName, parameters, (result) =>
            {
                HandleMessageCallBack(result);
            });
        }
        #endregion

        #endregion

        protected virtual void HandleMessageCallBack(IDialogResult result)
        {

        }
    }
}
