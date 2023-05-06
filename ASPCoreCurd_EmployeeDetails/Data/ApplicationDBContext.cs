using ASPCoreCurd_EmployeeDetails.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreCurd_EmployeeDetails.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<EmpModel> EmpDetails { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }
    }
}
