using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BO;
[Serializable]
public class BlEmptyException : Exception
{
    public BlEmptyException(string? label, int id)
       : base($"{label ?? ""} with id number {id} does not exist.\n")
    {
    }
    public BlEmptyException(string? label)
      : base($"{label ?? ""}.\n")
    {
    }

    public BlEmptyException(string? label, int id, Exception? innerException)
        : base($"{label} with id number {id} does not exist.\n", innerException)
    {
    }
}
//[Serializable]
//public class BlIdNotExistException : Exception//if not exist
//{
//    public BlIdNotExistException(string message, Exception innerException) : base(message, innerException) { }
//    public BlIdNotExistException(string message) : base(message) { }

//    public override string ToString() => base.ToString() + $" does not exist";
//}

[Serializable]
public class BlIdNotExistException : Exception
{
    public BlIdNotExistException(string? label, int id)
       : base($"{label} id number {id} does not exist.\n")
    {
    }
    public BlIdNotExistException(string? label)
      : base($"{label}.\n")
    {
    }

    public BlIdNotExistException(string? label, int id, Exception? innerException)
        : base($" {label} id number {id} does not exist.\n"/*, innerException*/)
    {
    }
}



[Serializable]
public class BlInCorrectIntException : Exception
{
    public BlInCorrectIntException(string? label, int id)
       : base($"{label}  id number {id} is incorrect input.\n")
    {
    }
    public BlInCorrectIntException(string? label)
      : base($"{label} .\n")
    {
    }

    public BlInCorrectIntException(string? label, int id, Exception? innerException)
        : base($"{label} id number {id} is incorrect input.\n", innerException)
    {
    }
}

[Serializable]
public class BlIdAlreadyExistException : Exception
{
   
    public BlIdAlreadyExistException(string? label, int id)
       : base($"{label}  id number {id} alraedy exist.\n")
    {
    }

    public BlIdAlreadyExistException(string? label, int id, Exception? innerException)
        : base($"{label}  id number {id} is already exist.\n", innerException)
    {
    }

}

[Serializable]
public class BlInCorrectStringException : Exception
{
    public BlInCorrectStringException(string? label, int id)
       : base($"{label} with id number {id} is incorrect input.\n")
    {
    }

    public BlInCorrectStringException(string? label)
      : base($"{label} .\n")
    {
    }

    public BlInCorrectStringException(string? label, int id, Exception? innerException)
        : base($"{label} with id number {id} is incorrect input.\n", innerException)
    {
    }
}

[Serializable]
public class BlInCorrectDoubleException : Exception
{
    public BlInCorrectDoubleException(string? label, double id)
       : base($"{label} with id number {id} is incorrect input.\n")
    {
    }

    public BlInCorrectDoubleException(string? label, double id, Exception? innerException)
        : base($"{label} with id number {id} is incorrect input.\n", innerException)
    {
    }
}

[Serializable]
//public class BlInCorrectDetailsException : Exception
//{
//    public BlInCorrectDetailsException(string? label, int id)
//       : base($" There is no {label} in the List.\n")
//    {
//    }

//    public BlInCorrectDetailsException(string? label, int id, Exception? innerException)
//        : base($" There is no {label} in the List.\n", innerException)
//    {
//    }
//}


//[Serializable]
//public class BlMissingDataException : Exception
//{
//    public BlMissingDataException(string? label, int id)
//       : base($" {label}.\n")
//    {
//    }

//    public BlMissingDataException(string? label, int id, Exception? innerException)
//        : base($"{label}.\n", innerException)
//    {
//    }
//}



//[Serializable]
//public class BlDatesException : Exception
//{
//    public BlDatesException(String label, DateTime? d1, DateTime d2)
//       : base($" {label} Can not compare between {d1} to {d2}.\n")
//    {
//    }

//    public BlDatesException(String label, DateTime? d1, DateTime d2, Exception? innerException)
//        : base($" {label} Can not compare between {d1} to {d2}.\n", innerException)
//    {
//    }

public class BlNullPropertyException : Exception
{
    public string? message;

    public BlNullPropertyException(string mess) : base() { message = mess; }
    public BlNullPropertyException(string message, Exception innerException) : base(message, innerException) { }
    public override string ToString() => $"{message}";

}
[Serializable]
public class BlWrongCategoryException : Exception
{
    public string? message;
    public BlWrongCategoryException(string mess) : base() { message = mess; }
    public BlWrongCategoryException(string message, Exception innerException) : base(message, innerException) { }
    public override string ToString() => $"{message}";
}
[Serializable]
public class BlIncorrectDateException : Exception
{
    public string? message;
    public BlIncorrectDateException(string mess) : base() { message = mess; }
    public BlIncorrectDateException(string message, Exception innerException) : base(message, innerException) { }
    public override string ToString() => $"{message}";
}
public class BlInvalidInputException : Exception
{
    public string? Entity;
    public BlInvalidInputException(string ent) : base() { Entity = ent; }
    public BlInvalidInputException(string Entity, Exception innerException) : base(Entity, innerException) { }
    public override string ToString() => $"invalid {Entity}";
}




