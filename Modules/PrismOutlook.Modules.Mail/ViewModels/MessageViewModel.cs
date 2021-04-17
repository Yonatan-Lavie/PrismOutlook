using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismOutlook.Business;
using PrismOutlook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MessageViewModel : BindableBase, IDialogAware
    {

        private DelegateCommand _sendMessageCommand;
        private readonly IMailService _mailService;

        private Business.MailMessage _message;
        public Business.MailMessage Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }


        public DelegateCommand SendMessageCommand =>
            _sendMessageCommand ?? (_sendMessageCommand = new DelegateCommand(ExecuteSendMessageCommand));

        void ExecuteSendMessageCommand()
        {
            _mailService.SendMessage(Message);
            //TODO: fix magic string
            IDialogParameters parameters = new DialogParameters();
            parameters.Add("messageSent", Message);

            RequestClose?.Invoke(new DialogResult(ButtonResult.Yes, parameters));
        }
        public MessageViewModel(IMailService mailService)
        {
            _mailService = mailService;
        }

        //TODO: use this
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
            Message = new Business.MailMessage() { From = "yonatan.lavie89@gmail.com" };


            var messageMode = parameters.GetValue<MessageMode>(MailParameters.MessageMode);
            if(messageMode != MessageMode.New)
            {
                var messageId = parameters.GetValue<int>(MailParameters.MessageId);
                var originalMessage = _mailService.GetMessage(messageId);
                Message.To = GetToEmails(messageMode, originalMessage);
                
                if(messageMode == MessageMode.ReplyAll || messageMode == MessageMode.Reply)
                    Message.Cc = originalMessage.Cc;

                Message.Subject = GetMessageSubject(messageMode, originalMessage);
                
                //TODO: append RTF with reply header
                Message.Body = originalMessage.Body;

            }

        }
       
        ObservableCollection<string> GetToEmails(MessageMode mode, MailMessage message)
        {
            var toEmails = new List<string>();
            switch (mode)
            {
                case MessageMode.Reply:
                    {
                        toEmails.Add(message.From);
                        break;
                    }

                case MessageMode.ReplyAll:
                    {
                        toEmails.Add(message.From);
                        toEmails.AddRange(message.To.Where(x => x != "yonatan.lavie89@gmail.com"));
                        break;
                    }
                case MessageMode.Forward:
                    {
                        break;
                    }

            }
            return new ObservableCollection<string>(toEmails);
        }
        private string GetMessageSubject(MessageMode messageMode, MailMessage originalMessage)
        {
            var prefix = String.Empty;
            switch (messageMode)
            {
                case MessageMode.Reply:
                case MessageMode.ReplyAll:
                    {
                        prefix = "RE:";
                        break;
                    }
                case MessageMode.Forward:
                    {
                        prefix = "FW:";
                        break;
                    }

            }
            return originalMessage.Subject.ToLower().StartsWith(prefix.ToLower()) ? originalMessage.Subject : $"{prefix} {originalMessage.Subject}";
        }
    }
}
