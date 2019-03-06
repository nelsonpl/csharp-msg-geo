<template>
  <div id="app">
    <v-app id="inspire">
      <v-navigation-drawer
        v-show="isAuthenticated"
        fixed
        :clipped="$vuetify.breakpoint.mdAndUp"
        app
        v-model="drawer"
      >
        <v-list dense>
          <template v-for="item in items">
            <v-list-tile :href="item.href" :key="item.text">
              <v-list-tile-action>
                <v-icon>{{ item.icon }}</v-icon>
              </v-list-tile-action>
              <v-list-tile-content>
                <v-list-tile-title>{{ item.text }}</v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
          </template>
        </v-list>
      </v-navigation-drawer>
      <v-toolbar color="blue darken-3" dark app :clipped-left="$vuetify.breakpoint.mdAndUp" fixed>
        <v-toolbar-title style="width: 300px" class="ml-0 pl-3">
          <v-toolbar-side-icon v-show="isAuthenticated" @click.stop="drawer = !drawer"></v-toolbar-side-icon>
          <span class="hidden-sm-and-down">GeoMSG</span>
        </v-toolbar-title>
        <v-text-field :append-icon="'search'" @click:append="search()" v-model="searchValue" flat solo-inverted hide-details prepend-inner-icon="search" label="Search" class="hidden-sm-and-down" v-show="isSearch" ></v-text-field>
        <v-spacer></v-spacer>
        <v-btn href="/signup" v-show="!isAuthenticated" color="success">Sign up</v-btn>
        <v-btn href="/signin" v-show="!isAuthenticated" color="info">Sign in</v-btn>
        <div v-show="isAuthenticated">
          <v-menu offset-y>
            <v-btn slot="activator" icon>
              <v-icon>person</v-icon>
            </v-btn>
            <v-list>
              <v-list-tile @click.prevent="logout()">
                <v-list-tile-title>Logout</v-list-tile-title>
              </v-list-tile>
              <v-list-tile href="/PasswordChange">
                <v-list-tile-title>Change Password</v-list-tile-title>
              </v-list-tile>
            </v-list>
          </v-menu>
        </div>
      </v-toolbar>
      <v-content>
        <router-view></router-view>
      </v-content>
    </v-app>
  </div>
</template>

<script>
import Vue from "vue";
import apiSession from "@/api/SessionService";

var fncSearch = function(value){};

var config = {
  isAuthenticated: false,
  isSearch: false,
  searchValue: '',
  drawer: false,
  items: [
    { icon: "messages", text: "Messages", href: "/Messages" },
    { icon: "map", text: "Map", href: "/Map" }
  ]
};

export default {
  name: "app",
  data() {
    config.isAuthenticated = Vue.prototype.$auth.isAuthenticated();
    config.drawer = Vue.prototype.$auth.isAuthenticated();
    return config;
  },
  reload: function() {
    config.isAuthenticated = Vue.prototype.$auth.isAuthenticated();
    config.drawer = Vue.prototype.$auth.isAuthenticated();
  },
  configSearch(fnc) {
    config.isSearch = true;
    fncSearch = fnc;
  },
  methods: {
    async logout() {
      await Vue.prototype.$auth.getSessionToken().then(function(token) {
        apiSession.delete(token);
      });
      Vue.prototype.$auth.endSession();
      this.$router.go("/SignIn");
    },
    search(){
      fncSearch(config.searchValue);
    }
  }
};
</script>

<style>
</style>