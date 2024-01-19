using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.InvitationDto
{
    public class InvitationUpdateRequestDto
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDeclined { get; set; }
    }
}
