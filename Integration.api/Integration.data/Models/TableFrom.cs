using AutoRepairPro.Data.Models;
namespace Integration.data.Models
{
    public class TableFrom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FromDbId { get; set; }
        public FromDb FromDb { get; set; }
        public int? TableToId { get; set; }
        public TableTo TableTo { get; set; }   

        public List<ColumnFrom > ColumnFromList { get; set; }
        public List<TableReference> tableReferences { get; set; }
    }  
    public class TableTo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ToDbId { get; set; }
        public ToDb  ToDb{ get; set; }
        public int? TableFromId { get; set; }
        public TableFrom TableFrom { get; set; }
        public List<ColumnTo > ColumnToList { get; set; }
    }

    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TableFromId { get; set; }
        public TableFrom TableFrom { get; set; }
        public int TableToId { get; set; }
        public TableTo TableTo { get; set; }
        public string ToPrimaryKeyName { get; set; }
        public string ToLocalIdName { get; set; }
        public string fromPrimaryKeyName { get; set; } 

        public List<ConditionFrom>  conditionFroms { get; set; }
        public List<ConditionTo>   ConditionTos{ get; set; }
    }

    public class ColumnFrom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int tableFromId { get; set; }
        public string ColumnToName { get; set; }
        public TableFrom tableFrom { get; set; }
        public bool isReference { get; set; }
        public string? TableToName { get; set; }
    }


    public class ColumnTo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int tableToId { get; set; }
        public TableTo TableTo { get; set; }
    }

    public class ConditionFrom
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public int ModuleId {  get; set; }
        public Module Module { get; set; }
    }  

    public class ConditionTo
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public int ModuleId {  get; set; }
        public Module Module { get; set; }
    }

    public class TableReference
    {
        public int Id { get; set; }
        public int TableFromId { get; set; }
        public TableFrom TableFrom { get; set; }
        public int TableToId { get; set; }
        public TableTo TableTo { get; set; }
        public string LocalPrimary { get; set; }
        public string cloudLocalName { get; set; }
        public string CloudPrimaryReferanceName { get; set; }
        public int? ModuleId { get; set; }
        public Module Module { get; set; }
    }


}


