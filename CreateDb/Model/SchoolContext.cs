using Microsoft.EntityFrameworkCore;

namespace CreateDb.Model
{
    public class SchoolContext :DbContext
    {   
        //definindo uma table no DB
        public DbSet<Student> Students { get; set; }
        
        public SchoolContext(DbContextOptions options): base(options)
        {
         
        }
    }
}
