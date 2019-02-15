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
        <v-text-field
          flat
          v-show="isAuthenticated"
          solo-inverted
          prepend-icon="search"
          label="Search"
          class="hidden-sm-and-down"
        ></v-text-field>
        <v-spacer></v-spacer>
        <v-btn href="/signup" v-show="!isAuthenticated" color="success">Sign up</v-btn>
        <v-btn href="/signin" v-show="!isAuthenticated" color="info">Sign in</v-btn>
        <div v-show="isAuthenticated">
          <v-btn icon>
            <v-icon>settings</v-icon>
          </v-btn>

          <v-menu offset-y>
            <v-btn slot="activator" icon>
              <v-icon>person</v-icon>
            </v-btn>
            <v-list>
              <v-list-tile @click.prevent="logout()">
                <v-list-tile-title>Logout</v-list-tile-title>
              </v-list-tile>
            </v-list>
          </v-menu>
        </div>
      </v-toolbar>
      <v-content>
        <v-container>
          <v-layout justify-center>
            <router-view></router-view>
          </v-layout>
        </v-container>
      </v-content>
    </v-app>
  </div>
</template>

<script>
import Vue from "vue";
import apiSession from "@/api/SessionService";

export default {
  name: "app",
  data() {
    return {
      isAuthenticated: Vue.prototype.$auth.isAuthenticated(),
      drawer: Vue.prototype.$auth.isAuthenticated(),
      items: [
        { icon: "messages", text: "Messages", href: "/Messages" },
        { icon: "map", text: "Map", href: "/Map" }
      ]
    };
  },
  methods: {
    login() {},
    async logout() {
      await Vue.prototype.$auth.getAccessToken().then(function(token) {
        apiSession.delete(token);
      });
      Vue.prototype.$auth.setAccessToken("");
      this.$router.go("/SignIn");
    }
  }
};
</script>

<style>
</style>