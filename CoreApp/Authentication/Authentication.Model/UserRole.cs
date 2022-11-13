using Authentication.Model.Attributes;

namespace Authentication.Model
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
