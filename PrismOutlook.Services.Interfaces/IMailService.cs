using PrismOutlook.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismOutlook.Services.Interfaces
{
    public interface IMailService
    {
        IList<MailMessage> GetInboxItems();
        
        IList<MailMessage> GetSentItems();
        
        IList<MailMessage> GetDeletedItems();

        MailMessage GetMessage(int id);
    }
}
