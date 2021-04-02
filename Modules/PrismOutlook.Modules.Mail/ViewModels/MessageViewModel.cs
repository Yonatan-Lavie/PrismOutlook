using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismOutlook.Business;
using PrismOutlook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MessageViewModel : BindableBase, IDialogAware
    {
        int _messageId;

        private DelegateCommand _sendMessageCommand;
        private readonly IMailService _mailService;

        private MailMessage _message;
        public MailMessage Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }


        public DelegateCommand SendMessageCommand =>
            _sendMessageCommand ?? (_sendMessageCommand = new DelegateCommand(ExecuteSendMessageCommand));

        void ExecuteSendMessageCommand()
        {
            _mailService.SendMessage(Message);
            IDialogParameters parameters = new DialogParameters();
            parameters.Add("messageSent", Message);

            RequestClose?.Invoke(new DialogResult(ButtonResult.Yes, parameters));
        }
        public MessageViewModel(IMailService mailService)
        {
            _mailService = mailService;
        }

        public string Title => "Mail Message";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            _messageId = parameters.GetValue<int>("id");
            if (_messageId == 0)
                Message = new MailMessage() { From="yonatanb.lavie89@gmail.com"};
            else
                Message = _mailService.GetMessage(_messageId);
        }
    }
}
