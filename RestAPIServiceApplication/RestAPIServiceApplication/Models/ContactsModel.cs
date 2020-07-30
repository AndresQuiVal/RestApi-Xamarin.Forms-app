using RestAPIServiceApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPIServiceApplication.Models
{
    public class ContactsModel
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string ContactAlias { get; set; }
        public string ContactCompanyName { get; set; }
        public long ContactNumber { get; set; }
        public byte[] ContactImage { get; set; }
        public string UserId { get; set; }
    }
}
