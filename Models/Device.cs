using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
public class Device
{
    private readonly ILazyLoader _lazyLoader;
    public Device()
    {
    }
    public Device(ILazyLoader lazyLoader)
    {
        _lazyLoader = lazyLoader;
    }

    private List<DeviceLog> _DeviceLogs;

    [Key]
    public int DeviceId { get; set; }
    [Required]
    public string DeviceName { get; set; }
    [Required]
    public float Lat { get; set; }
    [Required]
    public float Long { get; set; }
    public virtual List<DeviceLog> DeviceLogs
    {
        get => _lazyLoader.Load(this, ref _DeviceLogs);
        set => _DeviceLogs = value;
    }
}