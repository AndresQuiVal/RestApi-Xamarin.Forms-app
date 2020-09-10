using RestAPIServiceApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPIServiceApplication.Models
{
    public class ContactsModel
    {
        public int Id { get; set; }
        public virtual string ContactName { get; set; }
        public virtual string ContactAlias { get; set; }
        public virtual string ContactCompanyName { get; set; }
        public virtual long ContactNumber { get; set; }
        public byte[] ContactImage { get; set; }
        public string UserId { get; set; }
    }
}
