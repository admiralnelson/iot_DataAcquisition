# how to register your device?

**POST** JSON request to the  
following url: http://35.240.207.36/Device/NewDevice  
with header: **Content-Type: application/json; charset=utf-8** (important!)  
and following JSON in the request body:  
```
{
    "DeviceName": "Your Device Name", 
    "Lat":-6.97302, 
    "Long" :107.629167 
}

```
Lat: global latitude position, Long: global longitude position
Use google map to find out your desired lat and long.
**Don't leave your DeviceName empty!**

You'll receive this response indicating that your request is successfully processed

```
{
    "deviceId":3,
    "deviceName":"testing2",
    "lat":-6.97302,
    "long":107.629166,
    "deviceLogs":null
}
```
Please note your **deviceId**, it will be used as identification when updating your IoT sensors to the database!

# how to update your sensor data 

**POST** JSON request to the  
following url: http://35.240.207.36/Device/NewDevice   
with header: **Content-Type: application/json; charset=utf-8** (important!)  
and following JSON in the request body:  
```
{ 
    "DeviceId": YOUR_DEVICE_ID,
    "Temperature": 25,
    "Humidity": 0.5,
    "Sound": 12
 }
```
Temperature: your temperature sensor  
Humidity: your humidity sensor, keep in mind that this field is capped between **0.0-1.0**   
Sound: your sound sensor, keep in mind that this field is capped between **0.0-150.0**     


You'll receive this response indicating that your request is successfully processed   

```
{
    "no":5,
    "deviceId":1,
    "temperature":25.8,
    "humidity":0.5,
    "sound":12.0,
    "time":"2019-04-22T22:25:08.2616721+08:00"
}
```


