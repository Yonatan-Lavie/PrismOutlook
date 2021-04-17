using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismOutlook.Business;
using PrismOutlook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MessageReadOnlyViewModel : BindableBase, IDialogAware
    {
        #region Fileds
        private MailMessage _message;
        private readonly IMailService _mailService;
        #endregion

        #region Properties
        public event Action<IDialogResult> RequestClose;
        
        public string Title => "";
        
        public MailMessage Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        #endregion

        #region Constractor
        public MessageReadOnlyViewModel(IMailService mailService)
        {
            _mailService = mailService;
        } 
        #endregion

        #region Dialog Interface
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var messageId = parameters.GetValue<int>(MailParameters.MessageId);
            if (messageId != 0)
                Message = _mailService.GetMessage(messageId);
        }
        #endregion

    }
}
