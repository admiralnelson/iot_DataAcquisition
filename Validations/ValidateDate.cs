using System;
using System.ComponentModel.DataAnnotations;
public class ValidateDateAttribute : ValidationAttribute
{
    public ValidateDateAttribute()
    {
    }

    public override bool IsValid(object value)
    {
        var dt = (DateTime)value;
        DateTime dateToCompare;
        DateTime.TryParse("1 January 1257", out dateToCompare);
        if(dt >= dateToCompare)
        {
            return true;
        }
        return false;
    }
}