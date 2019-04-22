using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class DeviceController : ControllerBase
{

    private readonly MyDbContext _dbcontext;

    public DeviceController(MyDbContext context)
    {
        _dbcontext = context;
    }

    [HttpPost]
    public async Task<IActionResult> NewDevice([FromBody]Device dev)
    {    
        if(ModelState.IsValid)
        {
            await _dbcontext.AddAsync(dev);
            _dbcontext.SaveChanges();
            return new ObjectResult(dev);
        }
        return BadRequest(ModelState);
    }

    [HttpPost]
    public async Task<IActionResult> PostLog([FromBody]DeviceLog log)
    {
        if(ModelState.IsValid)
        {
            var checkIfExist = _dbcontext.Devices.Where(x=>x.DeviceId == log.DeviceId).Count() > 0;
            if(!checkIfExist)
            {
                return BadRequest("Unable to find device id " +  log.DeviceId);
            }            
            await _dbcontext.AddAsync(log);
            _dbcontext.SaveChanges();
            return new ObjectResult(log);
        }
        return BadRequest(ModelState);
    }

    [HttpGet]
    public IActionResult GetLog(int id)
    {
        var checkIfExist = _dbcontext.Devices.Where(x=>x.DeviceId == id).Count() > 0;
        if(!checkIfExist)
        {
            return BadRequest("Unable to find device id" + id);
        }
        var log = _dbcontext.DeviceLogs.Where(x=> x.DeviceId == id).ToList();
        return new ObjectResult(log);        
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var list = _dbcontext.Devices.ToList();
        return new ObjectResult(list);
    }

    [HttpGet]
    public IActionResult Get(int id)
    {
        var isExist = _dbcontext.Devices.Where(x=> x.DeviceId == id).Count() > 0;
        if(isExist)
        {
            var o = _dbcontext.Devices.Where(x=> x.DeviceId == id).FirstOrDefault();
            return new ObjectResult(o);
        }
        return BadRequest("Unable to find device id" + id);
    }

}