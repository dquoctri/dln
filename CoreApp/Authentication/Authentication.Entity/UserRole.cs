using Authentication.Entity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Entity
{
    public enum UserRole : ushort
    {
        [OrganizerTypes(OrganizerType.SYSTEM)]
        PARTNER_MANAGER,
        [OrganizerTypes(OrganizerType.SYSTEM)]
        PARTNER_VIEWER,
        [OrganizerTypes(OrganizerType.SYSTEM, OrganizerType.PARTNER)]
        ORGANIZER_MANAGER,
        [OrganizerTypes(OrganizerType.SYSTEM, OrganizerType.PARTNER)]
        ORGANIZER_VIEWER,
        [OrganizerTypes(OrganizerType.SYSTEM, OrganizerType.PARTNER, OrganizerType.NORMAL)]
        USER_MANAGER,
        [OrganizerTypes(OrganizerType.SYSTEM, OrganizerType.PARTNER, OrganizerType.NORMAL)]
        USER_VIEWER,
        [OrganizerTypes(OrganizerType.SYSTEM, OrganizerType.PARTNER, OrganizerType.NORMAL)]
        PROFILE_MANAGER,
        [OrganizerTypes(OrganizerType.SYSTEM, OrganizerType.PARTNER, OrganizerType.NORMAL)]
        PROFILE_VIEWER
    }
}
