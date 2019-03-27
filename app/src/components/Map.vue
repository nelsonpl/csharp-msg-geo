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

<script src="./Map.js"></script>
<style src="./Map.css"></style>
