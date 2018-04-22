namespace kms.Models
{
    public class Role : BaseModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool System { get; set; }
    }
}
