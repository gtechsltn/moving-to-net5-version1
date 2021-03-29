﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ValidationLibrary.CommonRules
{
    /// <summary>
    /// Is a valid SSN
    /// </summary>
    /// <returns>True if valid, false if invalid SSN</returns>
    /// <remarks>
    ///
    /// Handle some commonly encountered “fake” values or ones that might easily be forged in
    /// an area that might require a user to enter one.
    /// 
    /// ^                                       #Start of expression
    /// (?!\b(\d)\1+-(\d)\1+-(\d)\1+\b)         #Don't allow all matching digits for every field
    /// (?!123-45-6789|219-09-9999|078-05-1120) #Don't allow "123-45-6789", "219-09-9999" or "078-05-1120"
    /// (?!666|000|9\d{2})\d{3}                 #Don't allow the SSN to begin with 666, 000 or anything between 900-999
    /// -                                       #A dash (separating Area and Group numbers)
    /// (?!00)\d{2}                             #Don't allow the Group Number to be "00"
    /// -                                       #Another dash (separating Group and Serial numbers)
    /// (?!0{4})\d{4}                           #Don't allow last four digits to be "0000"
    /// $                                       #End of expression
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SocialSecurityAttribute : ValidationAttribute
    {
        public string SocialValue { get; set; }

        public override bool IsValid(object value)
        {
            return value is string && (!(value is null) && Regex.IsMatch(value.ToString().Replace("-", ""),
                @"^(?!\b(\d)\1+\b)(?!123456789|219099999|078051120)(?!666|000|9\d{2})\d{3}(?!00)\d{2}(?!0{4})\d{4}$"));
        }
    }
}
