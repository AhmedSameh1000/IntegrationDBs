namespace AutoRepairPro.Data.Models
{
    public class FromDb 
    {
        public int Id { get; set; }
        public string DbName { get; set; }
        public string ConnectionString { get; set; }
        public bool IsSelected { get; set; }
    }  


}
