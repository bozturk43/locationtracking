

function initMap() {
    var latlng={lat:39.925533,lng:32.866287};

    var mapOptions={
        center:latlng,
        zoom:7,
        mapTypeId:google.maps.MapTypeId.ROADMAP
    };
    
    var map=new google.maps.Map(document.getElementById("map"),mapOptions);

    var directionsService=new google.maps.DirectionsService();
    var directionsDisplay=new google.maps.DirectionsRenderer();
    directionsDisplay.setMap(map);

    function calcRoute(){
        var request={
            origin:document.getElementById("cikisnoktasi").value,
            destination:document.getElementById("varisnoktasi").value,
            travelMode:google.maps.TravelMode.DRIVING,
            unitSystem:google.maps.UnitSystem.IMPRERIAL
        }

        directionsService.route(request,(result,status)=>{
            if(status==google.maps.DirectionsStatus.OK){
                const output=document.querySelector('#output');
                output.innerHTML="<div class='alert-info'>Cikis Noktası:"+document.getElementById("cikisnoktasi").value+".<br/>Varış Noktası:"+document.getElementById("varisnoktasi").value+".<br/>Mesafe:"+result.routes[0].legs[0].distance.text+".<br/>Tahmini Varış Süresi:"+result.routes[0].legs[0].duration.text+".</div>";
                directionsDisplay.setDirections(result);
                var coordinates=google.maps.geometry.encoding.decodePath(result.routes[0].overview_polyline);
                console.log(String(coordinates));
            }
            else{
                directionsDisplay.setDirections({routes:[]});
                map.center(latlng);
            }
        });
    }

    document.getElementById("donebtn").addEventListener('click',calcRoute,true);

    var options={
        types:['(cities)']
    }
    
    var input1=document.getElementById("cikisnoktasi");
    var autocomplete1=new google.maps.places.Autocomplete(input1,options);
    
    var input2=document.getElementById("varisnoktasi");
    var autocomplete2=new google.maps.places.Autocomplete(input2,options);
}