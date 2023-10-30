using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    [Serializable]
    public class DalIDNotExistException : Exception
    {
        public int EntityID;
        public string EntityName;

        public DalIDNotExistException(int id, string name) : base() { EntityID = id; EntityName = name; }
        public DalIDNotExistException(int id, string name, string message) : base(message) { EntityID = id; EntityName = name; }
        public DalIDNotExistException(int id, string name, string message, Exception inner) : base(message, inner) { EntityID = id; EntityName = name; }
        public override string ToString() => $"ID:{EntityID} of type {EntityName}, do not exist";

    }

    [Serializable]
    public class DalIDAlreadyExistException : Exception
    {
        public int EntityID;
        public string EntityName;
        public DalIDAlreadyExistException(int id, string name) : base() { EntityID = id; EntityName = name; }
        public DalIDAlreadyExistException(int id, string name, string message) : base(message) { EntityID = id; EntityName = name; }
        public DalIDAlreadyExistException(int id, string name, string message, Exception inner) : base(message, inner) { EntityID = id; EntityName = name; }

        public override string ToString() => $"ID:{EntityID} of type {EntityName}, already exist";
    }

    [Serializable]
    public class DatesException : Exception
    {
        DateTime? d1;
        DateTime? d2;
        string label;
        public DateTime? getD1() { return d1; }
        public DateTime? getD2() { return d2; }
        public string getLabel() { return label; }
        public DatesException(string label, DateTime? d1, DateTime d2)
        {
            this.d1 = d1;
            this.d2 = d2;
            this.label = label;
        }
        public override string ToString()
        {
            return $"[Error] {label} Can not compare between {d1} to {d2}.\n";
        }

    }




}
