using Microsoft.EntityFrameworkCore;

namespace BigonWebUI.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options) { }
        
    }
}
