var position;
var marker;
var result;
var dirroad;

//40.981820000000006', '29.057740000000003'
  function initMap() {
    /*-Map Oluşturuluyor Özellikleri Tanımlanıyor-*/
    const map = new google.maps.Map(document.getElementById("map"), {
    zoom: 8,
    center: { lat: -24.345, lng: 134.46 }, // Australia.
  });
  /*-Map Oluşturuluyor Özellikleri Tanımlanıyor-*/

  /*Rota Oluşturmak İçin Gerekli Servis ve Özellikleri tanımlanıyor*/
  const directionsService = new google.maps.DirectionsService();
  const directionsRenderer = new google.maps.DirectionsRenderer({
    //Map Üzerinde Oluşan Baslangıc ve Bitiş Markerları draggable secenegi veriliyor.
    draggable: true,
    //Rotanın cizileceği map burada belirtiliyor yukarıda belirttiğimiz map oluşturuluyor.
    map,
    //Rota üzerinde izlenecek adımların yazılacagı panel burada değişken icerisine aktarılıyor.
    /*panel: document.getElementById("panel"),*/
  });
  /*Rota Oluşturmak İçin Gerekli Servis ve Özellikleri tanımlanıyor*/

  /*Direction için event listener ekleniyor event türü directions_changed yani direction değiştiğinde bu metod tetiklenecek*/
  directionsRenderer.addListener("directions_changed", () => {
    //Direction rendererdan getDirection metodu cagırılıyor ve sonuc directions sabitinin içine aktarılıyor.
    const directions = directionsRenderer.getDirections();
    if (directions) {
      //Direction üzerindeki toplam mesafe hesaplanıyor.
      computeTotalDistance(directions);  
      //Direction üzerinde çizilen polylinedaki değerler dizi içine dolduruluyor. Markerın hareketlendirilmesi için kullanılacak.
      var coordinates=google.maps.geometry.encoding.decodePath(directions.routes[0].overview_polyline);
      /*polyline her nokta için bir fonksiyon dondurdugunde deger alabilmek için aldıgımız diziyi stringe çevirdim*/
      var cordstr=String(coordinates);
      /*Dizide her bir eleman(43.05445,23.56689) şeklinde oldugu için virgül ve parantezleri temizleyerek yalnızca coordinatları bir arraye koydum*/
      //Daha sonrada parantezlerini temizleyerek start noktası olarak ilk koordinatlar ile birlikte marker oluşturdum.
      var cordarr=cordstr.split(",");
      var lati=cordarr[0].slice(1);
      var long=cordarr[1].slice(1,-1);
      //MoveMarker() metodunda kullanmak için koordinatları global değişkene doldurdum
      dirroad=coordinates;
      console.log(String(dirroad));
      //Directionun baslangıc noktasında bir adet marker oluşturdum.
      addMarker({coords:{lat:Number(lati),lng:Number(long)},iconImage:'https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png',content:"<h1>DENEME BASLIK "+i+"</h1>",map:map});
      /*Canlı Konum Alamayacagım için Belirlenene Yol üzerinde markerı hareket ettirmek için MoveMarker() metodu oluşturdum*/
      moveMarker();
      var i=0;
      /*Şimdilik coordinates dizisi içindeki tüm elemanlar için bir marker harita üzerine ekleniyor*/
      
    }
  });
  /*Direction için event listener ekleniyor event türü directions_changed yani direction değiştiğinde bu metod tetiklenecek*/
  
  /*Display Route metoduna baslangıc ve bitis noktaları ve metod için gereken yukarıda olusturdugumuz servisler gönderiliyor */
  displayRoute(
    "Sancaktepe",
    "Beyoglu",
    directionsService,
    directionsRenderer
  );
  displayRoute(
    "Çatalca",
    "Pınarhisar",
    directionsService,
    directionsRenderer
  );
    /*Display Route metoduna baslangıc ve bitis noktaları ve metod için gereken yukarıda olusturdugumuz servisler gönderiliyor */

}

function displayRoute(origin, destination, service, display) {
  service
    .route({
      //Rotanın baslangıc noktası
      origin: origin,
      //Rotanın bitis noktası
      destination: destination,
      //Oluşturulacak Rotanın yöntemi(Yürüyüş,Toplu Taşıma,Sürüş)
      travelMode: google.maps.TravelMode.DRIVING,
      avoidTolls: true,
    })
    .then((result) => {
      display.setDirections(result);
      
    })
    .catch((e) => {
      alert("Could not display directions due to: " + e);
    });
   
    
}


function computeTotalDistance(result) {
  let total = 0;
  const myroute = result.routes[0];

  if (!myroute) {
    return;
  }

  for (let i = 0; i < myroute.legs.length; i++) {
    total += myroute.legs[i].distance.value;
  }

  total = total / 1000;
 // document.getElementById("total").innerHTML = total + " km";
}


function addMarker(props){
                marker=new google.maps.Marker({
                position:props.coords,
                map:props.map,
                icon:props.iconImage
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



var delay=100;
var deltaLat;
var deltaLang;
var i=0;

function moveMarker(){

  var cordstr=String(dirroad[i]);
  var cordarr=cordstr.split(",");
  var lati=cordarr[0].slice(1);
  var long=cordarr[1].slice(1,-1);
  var latlang=new google.maps.LatLng(Number(lati),Number(long));
  console.log("Latitude= "+lati);
  console.log("Longtitude= "+long);
  marker.setPosition(latlang);

  if(i!=dirroad.length){
    setTimeout(moveMarker,delay);
    i++;
  }
}