﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Core.ViewModels
{
    public class ValidateTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isvalid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:mm",
                CultureInfo.CurrentCulture,
                 DateTimeStyles.None,
                 out dateTime);
            return (isvalid);
        }
    }

}