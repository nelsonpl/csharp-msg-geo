<template>
  <div id="mapview">
    <div class="divmap">
      <l-map
        :ref="config.map"
        :zoom="config.zoom"
        @moveend="config.mapMoved"
        @click="config.addMsg"
        :bounds="config.bounds"
        :center="config.center"
      >
        <l-tile-layer :url="config.url" :attribution="config.attribution"></l-tile-layer>
        <l-geo-json
          :geojson="config.messagesMarket.geojson"
          :options="config.messagesMarket.options"
        />
      </l-map>
    </div>

    <v-btn @click="refresh()" fab bottom center round color="info" dark fixed>
      <v-icon>refresh</v-icon>
    </v-btn>

    <v-dialog v-model="config.dialog" width="800px">
      <v-card>
        <v-card-title class="grey lighten-4 py-4 title">Add Message</v-card-title>
        <v-container grid-list-sm class="pa-4">
          <v-layout row wrap>
            <v-flex xs12>
              <v-textarea
                prepend-icon="message"
                v-model="config.model.msg"
                box
                placeholder="Your messsage here..."
              ></v-textarea>
            </v-flex>
            <v-flex xs12>
              <v-select
                v-model="config.model.type"
                prepend-icon="list"
                :items="['Information', 'Help', 'Question', 'Sales']"
                label="Message Type"
              ></v-select>
            </v-flex>
            <v-flex xs12>
              <label>{{config.model.location}}</label>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn flat @click="config.dialog = false">Cancel</v-btn>
          <v-btn flat color="primary" @click="config.create()">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import Vue from "vue";
import MapPopupContent from "./MapPopupContent";
import api from "@/api/MapService";
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
    await apiMsg.create(config.model);
    config.dialog = false;

    config.messagesMarket.geojson = await api.get(
      config.bounds.getWest(),
      config.bounds.getEast(),
      config.bounds.getSouth(),
      config.bounds.getNorth()
    );
  },
  addMsg: function(e) {
    config.model.location =
      "Latitude: " + e.latlng.lat + " - Longitude: " + e.latlng.lng;
    config.model.latitude = e.latlng.lat;
    config.model.longitude = e.latlng.lng;
    config.dialog = true;
  },
  mapMoved: function(e) {
    config.bounds = e.target.getBounds();
  },

  messagesMarket: {
    geojson: null,
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
  config.messagesMarket.geojson = await api.get(
    config.bounds.getWest(),
    config.bounds.getEast(),
    config.bounds.getSouth(),
    config.bounds.getNorth()
  );
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
    refresh: async function() {
      config.messagesMarket.geojson = await api.get(
        config.bounds.getWest(),
        config.bounds.getEast(),
        config.bounds.getSouth(),
        config.bounds.getNorth()
      );
    }
  }
};
</script>

<style>
.leaflet-fake-icon-image-2x {
  background-image: url(../../node_modules/leaflet/dist/images/marker-icon-2x.png);
}
.leaflet-fake-icon-shadow {
  background-image: url(../../node_modules/leaflet/dist/images/marker-shadow.png);
}
@import "../../node_modules/leaflet/dist/leaflet.css";

.divmap {
  position: absolute;
  overflow-x: auto;
  top: 0px;
  right: 0px;
  left: 0px;
  bottom: 0px;
  z-index: 1;
}
</style>
