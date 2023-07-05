using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// وضعیت نماد بر اساس صفحه شرکت فناوری بورس
/// </summary>
public enum InstrumentStatus
{
    /// <summary>
    /// نامعلوم
    /// </summary>
    [Display(Name = "نامعلوم")]
    [Description("نامعلوم")]
    Unknown = 0,

    /// <summary>
    /// "ممنوع";
    /// </summary>
    [Display(Name = "ممنوع")]
    [Description("I")]
    Forbidden = 1,

    /// <summary>
    /// "A" : "مجاز"
    /// </summary>
    [Display(Name = "مجاز")]
    [Description("A")]
    Allowed = 2,

    /// <summary>
    /// "AG" : "مجاز-مسدود";
    /// </summary>
    [Display(Name = "مجاز-مسدود")]
    [Description("AG")]
    AllowedClosed = 3,

    /// <summary>
    /// "AS" : "مجاز-متوقف";
    /// </summary>
    [Display(Name = "مجاز-متوقف")]
    [Description("AS")]
    AllowedStopped = 4,

    /// <summary>
    /// "AR" : "مجاز-محفوظ";
    /// </summary>
    [Display(Name = "مجاز-محفوظ")]
    [Description("AR")]
    AllowedProtected = 5,

    /// <summary>
    /// "IG" : "ممنوع-مسدود";
    /// </summary>
    [Display(Name = "ممنوع-مسدود")]
    [Description("IG")]
    ForbiddenClosed = 6,

    /// <summary>
    /// "IS" : "ممنوع-متوقف";
    /// </summary>
    [Display(Name = "ممنوع-متوقف")]
    [Description("IS")]
    ForbiddenStopped = 7,

    /// <summary>
    /// "IR" : "ممنوع-محفوظ";
    /// </summary>
    [Display(Name = "ممنوع-محفوظ")]
    [Description("IR")]
    ForrbiddenProtected = 8

}

