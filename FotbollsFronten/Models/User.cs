

namespace FotbollsFronten.Models
{
    public class User
    {
        public int  Id { get; set; }
        public string userId { get; set; } 
        public string Name { get; set; }
        public DateTime RegisterDay { get; set; }
        public string Image { get; set; }
        public bool IsAdmin { get; set; } 

    }
}
