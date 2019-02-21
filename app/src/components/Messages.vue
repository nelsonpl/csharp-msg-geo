<template>
  <div class="messages">
    <v-card>
      <v-container fluid grid-list-lg>
        <v-layout v-for="item in list" :key="item.id" row wrap>
          <v-flex xs12>
            <v-card>
              <v-card-title>
                <span class="gray--text">{{item.type}}</span>
                <v-spacer></v-spacer>
                <span class="gray--text">{{item.dateCreate}}</span>
              </v-card-title>

              <v-card-text class="headline font-weight-bold">{{item.msg}}</v-card-text>

              <v-card-actions>
                <span class="gray--text">{{item.User.Name}}</span>
                <v-spacer></v-spacer>
                <v-list-tile>
                  <v-layout>
                    <v-btn :href="linkGoogleMaps(item)" target="_blank" icon>
                      <v-icon>map</v-icon>
                    </v-btn>
                  </v-layout>
                </v-list-tile>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card>

    <v-btn fab bottom right color="pink" dark fixed @click.stop="dialog = !dialog">
      <v-icon>add</v-icon>
    </v-btn>
    <v-dialog v-model="dialog" width="800px">
      <v-card>
        <v-card-title class="grey lighten-4 py-4 title">Add Message</v-card-title>
        <v-container grid-list-sm class="pa-4">
          <v-layout row wrap>
            <v-flex xs12>
              <v-textarea
                prepend-icon="message"
                v-model="model.msg"
                box
                placeholder="Your messsage here..."
              ></v-textarea>
            </v-flex>
            <v-flex xs12>
              <v-select
                v-model="model.type"
                prepend-icon="list"
                :items="['Information', 'Help', 'Question', 'Sales']"
                label="Message Type"
              ></v-select>
            </v-flex>
            <v-flex xs12>
              <v-btn flat @click="getLocation()">Get Location</v-btn>
              <label>{{model.location}}</label>
            </v-flex>
          </v-layout>
        </v-container>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn flat @click="dialog = false">Cancel</v-btn>
          <v-btn flat color="primary" @click="create()">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import api from "@/api/MessageService";

var message = {
  msg: "",
  type: "",
  location: "get your location..."
};

export default {
  name: "messages",
  data() {
    return {
      dialog: false,
      loading: false,
      list: [],
      model: message
    };
  },
  async created() {
    this.get();
  },
  methods: {
    async get() {
      try {
        this.list = await api.get();
      } finally {
      }
    },
    async create() {
      try {
        await api.create(this.model);
        this.get();
        this.dialog = false;
      } finally {
      }
    },
    linkGoogleMaps(item) {
      return (
        "https://www.google.com/maps/@" +
        item.latitude +
        "," +
        item.longitude +
        ",15z"
      );
    },
    getLocation() {
      message.location = "getting...";
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          function(position) {
            message.latitude = position.coords.latitude;
            message.longitude = position.coords.longitude;
            message.location =
              "Latitude: " +
              message.latitude +
              " - Longitude: " +
              message.longitude;
          },
          function(error) {
            message.latitude = -15.79522682;
            message.longitude = -47.8827095;
            message.location =
              "Latitude: " +
              message.latitude +
              " - Longitude: " +
              message.longitude;
          }
        );
      } else {
        message.latitude = -15.79522682;
        message.longitude = -47.8827095;
        message.location =
          "Latitude: " +
          message.latitude +
          " - Longitude: " +
          message.longitude;
      }
    }
  }
};
</script>

<style>
</style>
