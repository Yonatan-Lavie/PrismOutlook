using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismOutlook.Business;
using PrismOutlook.Core;
using PrismOutlook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismOutlook.Modules.Mail.ViewModels
{
    public class MessageReadOnlyViewModel : MessageViewModelBase, IDialogAware
    {

        #region Properties
        public event Action<IDialogResult> RequestClose;
        
        public string Title => "";
        #endregion

        #region Constractor
        public MessageReadOnlyViewModel(IMailService mailService, IRegionDialogService regionDialogService):
            base(mailService, regionDialogService)
        {

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
                Message = MailService.GetMessage(messageId);
        }
        #endregion

        protected override void ExecuteDeleteMessageCommand()
        {
            base.ExecuteDeleteMessageCommand();

            var p = new DialogParameters();
            p.Add(MailParameters.MessageMode, MessageMode.Delete);
            p.Add(MailParameters.MessageId, Message.Id);

            var result = new DialogResult(ButtonResult.OK,p);
            RequestClose(result);
        }
    }
}
