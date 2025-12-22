using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly AppDbContext _context;
        public ApplicationUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ApplicationUser applicationUser)
        {
            _context.ApplicationUsers.Update(applicationUser);
        }
    }
}
