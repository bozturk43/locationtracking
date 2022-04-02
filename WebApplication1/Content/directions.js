var position;
var marker;
var result;
var dirroad;

//40.981820000000006', '29.057740000000003'
  function initMap() {
    /*-Map Oluşturuluyor Özellikleri Tanımlanıyor-*/
    const map = new google.maps.Map(document.getElementById("map"), {
    zoom: 0,
    center: { lat: -24.345, lng: 134.46 }, // Australia.
  });


  const directionsService = new google.maps.DirectionsService();
  geocoder = new google.maps.Geocoder();
  
  function renderDirections(result){
    const directionsRenderer = new google.maps.DirectionsRenderer();
    directionsRenderer.setMap(map);
    directionsRenderer.setDirections(result);
  }

  function requestDirections(start,end){
      directionsService.route({
          origin:start,
          destination:end,
          travelMode:google.maps.DirectionsTravelMode.DRIVING,
      },
        function(result){
            renderDirections(result);
        })
  }
  
  var routes=[{start:"İzmir",end:"Bursa",sevkno:"Sevkiyat No:AsQ4689",aracno:"43ABC067"},{start:"Eskişehir",end:"Antalya",sevkno:"Sevkiyat No:YtU7665",aracno:"34AFG456"},{start:"Samsun",end:"Hatay",sevkno:"Sevkiyat No:SK4568",aracno:"55AFG789"}];
  var markers=[]; 
  routes.forEach(item=>{
        requestDirections(item.start,item.end);
        geocoder.geocode({'address':item.start},
        function(result){
            lat=String(result[0].geometry.location.lat());
            console.log(lat);
            lng=String(result[0].geometry.location.lng());
            console.log(lng);
            addMarker({coords:{lat:Number(lat),lng:Number(lng)},iconImage:'https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png',content:"<h5>"+item.sevkno+"</h5>,<h6>AraçNo:"+item.aracno+"<h6>",map:map});
            markers.push({coords:{lat:Number(lat),lng:Number(lng)},iconImage:'https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png',content:"<h5>"+item.sevkno+"</h5>,<h6>AraçNo:"+item.aracno+"<h6>",map:map});
            console.log(markers.length);
        });
    });
    
   
    

    function addMarker(props){
        var marker=new google.maps.Marker({
        position:props.coords,
        map:map,
        icon:props.iconImage,
        zIndex:100
    });
    if(props.iconImage){
        marker.setIcon(props.iconImage);
    }
    if(props.content){
        
        const infoWindow = new google.maps.InfoWindow({
        content:"",
        disableAutoPan: true,
        });
        marker.addListener("click",function(){
            infoWindow.setContent(props.content)
            infoWindow.open(map, marker);
        });
    }

    }


}