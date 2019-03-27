<template>
  <div class="mylocation">
    <v-container grid-list-xl>
      <v-layout row wrap>
        <v-flex xs10 offset-xs1>
          <v-card class="elevation-12">
            <v-toolbar dark color="primary">
              <v-toolbar-title>My Locations</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-form>
                <v-text-field v-model="model.title" label="Type title..."></v-text-field>
                <label>{{model.location}}</label>
                <v-btn @click="getLocation()">Get Location</v-btn>
                <v-btn @click="add()">Add</v-btn>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>

    <v-container grid-list-md>
      <v-layout v-for="item in list" :key="item.id" row wrap>
        <v-flex xs10 offset-xs1>
          <v-card>
            <v-container grid-list-md>
              <v-layout row wrap>
                <v-flex xs10>
                  <v-icon>home</v-icon>
                  <span>{{item.title}}</span>
                  <br/>
                  <span>{{item.latitude}}</span>
                  <span>{{item.longitude}}</span>
                </v-flex>
                <v-flex xs2>
                  <v-btn>
                    <v-icon>delete</v-icon>
                  </v-btn>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import Vue from "vue";
import api from "@/api/MyLocationService";

var myLocation = {
  latitude: "",
  longitude: "",
  location: "getting..."
};

export default {
  name: "mylocation",
  data() {
    return {
      model: myLocation,
      list: []
    };
  },
  async created() {
    this.get();
  },
  methods: {
    async get() {
      var that = this;
      api.get(entity => {
        that.list.push(entity);
      });
    },
    getLocation() {
      myLocation.location = "getting...";
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          function(position) {
            myLocation.latitude = position.coords.latitude;
            myLocation.longitude = position.coords.longitude;
            myLocation.location =
              "Latitude: " +
              myLocation.latitude +
              " - Longitude: " +
              myLocation.longitude;
          },
          function(error) {}
        );
      } else {
      }
    },
    async add() {
      try {
        api.save(this.model).then(function(e) {
          this.list = [];
          this.get();
          alert("sucesso!");
        });
      } catch (e) {
        alert("error!");
      } finally {
      }
    }
  }
};
</script>

<style>
</style>
