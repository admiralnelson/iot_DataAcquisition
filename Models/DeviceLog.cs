using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System;


public class DeviceLog
{


    [Key]
    public int No { get; set; }
    [Required]
    public int DeviceId { get; set; }
    [Required]
    public float Temperature { get; set; }
    [Required]
    [Range(0,1, ErrorMessage="Invalid range: 0-1")]
    public float Humidity { get; set; }
    [Required]
    [Range(0,150, ErrorMessage="Invalid range: 0-150db")]
    public float Sound { get; set; }
    [Required]
    [ValidateDate(ErrorMessage="Invalid date. (Or are you a medieval time traveller? )")]
    public DateTime Time { get; set; }
    [JsonIgnore]
    public virtual Device Device { get; set; }

}