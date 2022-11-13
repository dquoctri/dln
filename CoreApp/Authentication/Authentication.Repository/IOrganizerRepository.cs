using Authentication.Model;
using Repository.Common;

namespace Authentication.Repository
{
    public interface IOrganizerRepository : ICrudRepository<Organizer>
    {
        bool IsExistedName(int partnerId, string name);
    }
}
