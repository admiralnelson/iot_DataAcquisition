const RADIUS = 20;
var
    g_map; 

//Utilitas //
Date.prototype.addHours = function (h)
{
    this.setTime(this.getTime() + (h * 60 * 60 * 1000));
    return this;
}

function Ajax(operation, url, actionCallback, dataToSend)
{
    actionCallback = actionCallback || function(e){ console.log(e.responseText); };
    dataToSend= actionCallback || new Object();
    var 
        xhttp = new XMLHttpRequest();


    xhttp.onreadystatechange = function()
    {
        if (this.readyState == 4 && 
            (this.status == 200 || this.status == 201) ) 
        {            
            actionCallback(this);
            
        }
        else if (this.readyState == 4 && this.status != 200)
        {
            var msg = this.responseText;
            try
            {
                var o = JSON.parse(msg);
                alert("Error: " 	 + o.error + "\n" + 
                        "Status:" 	 + this.status +"\n" + 
                        "Raw message:" + msg
                        );
                parent.loadingMsg = "";
            }
            catch (E)
            {
                alert("Error: " + this.responseText + 
                            "Status:" 	 + this.status +"\n" );
                parent.loadingMsg = "";
            }
        }
        
    }
    
    var
        data = new Object();

    xhttp.open(operation, url, true);
    xhttp.setRequestHeader("Content-Type", "application/json;charset=UTF-8");							
    try 
    {
        data = JSON.stringify(dataToSend);
    } 
    catch (error) 
    {
        throw "Not a valid JSON data!";
    }
    xhttp.send(data);    
        
    
}

//Utilitas End//

//User Interface //
function ShowGraph()
{
    var
        graphWindow = document.getElementById("toolbar");   
    if (graphWindow.style.height == "300px")
    {
        Close();
        return;
    }
    graphWindow.style.height = "300px";            
}

function DropDown()
{
    var
        select = document.getElementById("deviceSelector")

    select.addEventListener('change', function () {
        DrawGraph(this.value);
    });
}

function DrawGraph(deviceId)
{
    function Draw(e)
    {
        var
            selectedData = JSON.parse(e.responseText);

        AmCharts.makeChart("chartdiv1",
        {
            "type": "serial",
            "theme": "none",
            "marginLeft": 20,
            "pathToImages": "https://www.amcharts.com/lib/3/images/",
            "dataProvider": selectedData,
            "valueAxes": [{
                "axisAlpha": 0,
                "inside": true,
                "position": "left",
                "ignoreAxisWidth": true
            }],
            "graphs": [
                {
                    "balloonText": "Temperatur [[category]]<br><b><span style='font-size:14px;'>[[value]]</span>C</b>",
                    "bullet": "round",
                    "bulletSize": 6,
                    "lineColor": "#d1655d",
                    "lineThickness": 2,
                    "type": "smoothedLine",
                    "valueField": "temperature"
                }
            ],
            "chartScrollbar": {},
            "chartCursor": {
                "cursorAlpha": 0,
                "cursorPosition": "mouse"
            },
            "categoryField": "time",
        }),
        chart2 = AmCharts.makeChart("chartdiv2",
        {
            "type": "serial",
            "theme": "none",
            "marginLeft": 20,
            "pathToImages": "https://www.amcharts.com/lib/3/images/",
            "dataProvider": selectedData,
            "valueAxes": [{
                "axisAlpha": 0,
                "inside": true,
                "position": "left",
                "ignoreAxisWidth": true
            }],
            "graphs": [
                {
                    "balloonText": "Humid [[category]]<br><b><span style='font-size:14px;'>[[value]]</span>%</b>",
                    "bullet": "round",
                    "bulletSize": 6,
                    "lineColor": "darkgreen",
                    "lineThickness": 2,
                    "type": "smoothedLine",
                    "valueField": "humidity"
                }
            ],
            "chartScrollbar": {},
            "chartCursor": {
                "cursorAlpha": 0,
                "cursorPosition": "mouse"
            },
            "categoryField": "time",
        }),
        chart3 = AmCharts.makeChart("chartdiv3",
        {
            "type": "serial",
            "theme": "none",
            "marginLeft": 20,
            "pathToImages": "https://www.amcharts.com/lib/3/images/",
            "dataProvider": selectedData,
            "valueAxes": [{
                "axisAlpha": 0,
                "inside": true,
                "position": "left",
                "ignoreAxisWidth": true
            }],
            "graphs": [
                {
                    "balloonText": "Sound [[category]]<br><b><span style='font-size:14px;'>[[value]]</span>dB</b>",
                    "bullet": "round",
                    "bulletSize": 6,
                    "lineColor": "#637bb6",
                    "lineThickness": 2,
                    "type": "smoothedLine",
                    "valueField": "sound"
                }
            ],
            "chartScrollbar": {},
            "chartCursor": {
                "cursorAlpha": 0,
                "cursorPosition": "mouse"
            },
            "categoryField": "time",
        });
    }

    Ajax("GET", "/Device/GetLog/"+deviceId, Draw);   
}

function Close()
{
    var
        graphWindow = document.getElementById("toolbar");

    graphWindow.style.height = "0";
}

//User Interface End//

//Main procedure //
function initMap() {
    g_map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: -6.973020, lng: 107.629167 },
        zoom: 30,
        mapTypeId: 'satellite'

    });
    MakeDropPinAndInterpolation();
    DropDown();
}
//Main procedure End//

//Load semua data ke peta//
function MakeDropPinAndInterpolation() {    

    function RetrieveData(e)
    {
        var
            data = JSON.parse(e.responseText),
            points = new Array();

        for (var i of data) {
            var latlng = new google.maps.LatLng(i.lat, i.long);
            points.push(latlng);
            new google.maps.Marker({
                position: latlng,
                map: g_map,
                title: `${i.deviceName}`
            });
            var sel = document.createElement("option");
            sel.innerHTML = "Perangkat " + i.deviceName;
            sel.value = i.deviceId;
            document.getElementById("deviceSelector").appendChild(sel);  
        }
        g_data = data;
        /*new google.maps.Marker({
            position: new google.maps.LatLng(-6.973020, 107.629167),
            map: map,
            title: "test"
        });*/
    
        new google.maps.visualization.HeatmapLayer({
            data: points,
            map: g_map,
            radius: 30
        });
      
    }

    Ajax("GET", "/Device/GetAll", RetrieveData);    
}
//Load semua data ke peta End//