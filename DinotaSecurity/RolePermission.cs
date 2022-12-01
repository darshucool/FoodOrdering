namespace Dinota.Security
{
    public class RolePermission
    {
        public RolePermission()
        {

        }

        public RolePermission(bool writable)
        {
            CanDelete = CanEdit = CanAdd = writable;
        }

        public bool CanAdd { get; private set; }

        public bool CanEdit { get; private set; }

        public bool CanDelete { get; private set; }
    }
}
