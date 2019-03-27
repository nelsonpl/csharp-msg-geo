import Vue from "vue";
import MapPopupContent from "./MapPopupContent";
import apiMsg from "@/api/MessageService";

var message = {
  msg: "",
  type: "",
  location: "get your location...",
  latitude: "",
  longitude: ""
};

var config = {
  zoom: 0,
  center: null,
  url: "",
  attribution: "",
  dialog: false,
  model: message,
  create: async function() {
    await apiMsg.save(config.model);
    config.dialog = false;
    config.messagesMarket.geojson = [];
    apiMsg.get((message)=>{
      config.messagesMarket.geojson.push(message);
    });
    // config.messagesMarket.geojson = await api.get(
    //   config.bounds.getWest(),
    //   config.bounds.getEast(),
    //   config.bounds.getSouth(),
    //   config.bounds.getNorth()
    // );
  },
  addMsg: function(e) {
    config.model.location = "Latitude: " + e.latlng.lat + " - Longitude: " + e.latlng.lng;
    config.model.latitude = e.latlng.lat;
    config.model.longitude = e.latlng.lng;
    config.dialog = true;
  },
  mapMoved: function(e) {
    config.bounds = e.target.getBounds();
  },

  messagesMarket: {
    geojson: [],
    options: {
      filter: function(feature, layer) {
        if (feature.properties) {
          return feature.properties.underConstruction !== undefined
            ? !feature.properties.underConstruction
            : true;
        }
        return false;
      },
      onEachFeature: onEachFeature
    }
  }
};

var buildMarkers = async function(lat, lng, messagesMarket) {
  var southWest = L.latLng(lat - 0.025, lng - 0.025);
  var northEast = L.latLng(lat + 0.025, lng + 0.025);
  config.center = L.latLng(lat, lng);
  config.bounds = L.latLngBounds(southWest, northEast);
  config.messagesMarket.geojson = [];
  apiMsg.get((message)=>{
    config.messagesMarket.geojson.push(message);
  });
  // config.messagesMarket.geojson = await api.get(
  //   config.bounds.getWest(),
  //   config.bounds.getEast(),
  //   config.bounds.getSouth(),
  //   config.bounds.getNorth()
  // );
};

var buildConfig = function() {
  config.map = null;
  config.zoom = 13;
  config.center = L.latLng(-15.79522682, -47.8827095);
  config.url = "http://{s}.tile.osm.org/{z}/{x}/{y}.png";
  config.attribution =
    '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors';

  return config;
};

function onEachFeature(feature, layer) {
  let PopupCont = Vue.extend(MapPopupContent);
  let popup = new PopupCont({
    propsData: {
      type: feature.geometry.type,
      text: feature.properties.message
    }
  });
  layer.bindPopup(popup.$mount().$el);
}

export default {
  name: "mapview",
  data() {
    return { config: buildConfig() };
  },
  async created() {
    var that = this;

    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        async position => {
          buildMarkers(
            position.coords.latitude,
            position.coords.longitude,
            that.config.messagesMarket
          );
        },
        async error => {
          buildMarkers(-15.79522682, -47.8827095, that.config.messagesMarket);
        },
        {}
      );
    } else {
      buildMarkers(-15.79522682, -47.8827095, that.config.messagesMarket);
    }
  },
  methods: {
    refresh: function() {      
      config.messagesMarket.geojson = [];
      apiMsg.get((message)=>{
        config.messagesMarket.geojson.push(message);
      });
      // config.messagesMarket.geojson = await api.get(
      //   config.bounds.getWest(),
      //   config.bounds.getEast(),
      //   config.bounds.getSouth(),
      //   config.bounds.getNorth()
      // );
    }
  }
};

