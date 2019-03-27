<template>
  <div id="app">
    <v-app id="inspire">
      <v-navigation-drawer v-show="isAuthenticated" fixed :clipped="$vuetify.breakpoint.mdAndUp" app v-model="drawer">
        <v-list dense>
          <template v-for="item in items">
            <v-list-tile :to="item.href" :key="item.text">
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
        <v-text-field :append-icon="'search'" @click:append="search()" v-model="searchValue" flat solo-inverted hide-details prepend-inner-icon="search" label="Search" class="hidden-sm-and-down" v-show="isSearch"></v-text-field>
        <v-spacer></v-spacer>
        <v-btn to="signup" v-show="!isAuthenticated" color="success">Sign up</v-btn>
        <v-btn to="signin" v-show="!isAuthenticated" color="info">Sign in</v-btn>
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
import firebase from "firebase";
import router from './router';

function isUserSignedIn() {
  return !!firebase.auth().currentUser;
}

var fncSearch = function(value) {};

var config = {
  isAuthenticated: false,
  isSearch: false,
  searchValue: "",
  drawer: false,
  items: [
    { icon: "messages", text: "Messages", href: "/Messages" },
    { icon: "map", text: "Map", href: "/Map" }
  ]
};

export default {
  name: "app",
  data() {
    config.isAuthenticated = isUserSignedIn();
    config.drawer = isUserSignedIn();
    config.isSearch = false;
    return config;
  },
  created: function() {
    var authStateObserver = function(user) {
      if (user) {
        var profilePicUrl = firebase.auth().currentUser.photoURL || '/images/profile_placeholder.png';
        var userName = firebase.auth().currentUser.displayName;

        config.isAuthenticated = isUserSignedIn();
        config.drawer = isUserSignedIn();
        config.isSearch = false;

        router.push("Messages");

        // Set the user's profile pic and name.
        // userPicElement.style.backgroundImage = "url(" + addSizeToGoogleProfilePic(profilePicUrl) + ")";
        // userNameElement.textContent = userName;

        // Show user's profile and sign-out button.
        // userNameElement.removeAttribute("hidden");
        // userPicElement.removeAttribute("hidden");
        // signOutButtonElement.removeAttribute("hidden");

        // Hide sign-in button.
        // signInButtonElement.setAttribute("hidden", "true");

        // We save the Firebase Messaging Device token and enable notifications.
        // saveMessagingDeviceToken();
      } else {
        // User is signed out!
        // Hide user's profile and sign-out button.
        // userNameElement.setAttribute("hidden", "true");
        // userPicElement.setAttribute("hidden", "true");
        // signOutButtonElement.setAttribute("hidden", "true");

        // Show sign-in button.
        // signInButtonElement.removeAttribute("hidden");
      }
    };

    firebase.auth().onAuthStateChanged(authStateObserver);
  },
  configSearch(fnc) {
    config.isSearch = true;
    fncSearch = fnc;
  },
  methods: {
    async logout() {
      var router = this.$router;
      firebase
        .auth()
        .signOut()
        .then(() => {
          config.isAuthenticated = false;
          config.drawer = false;
          config.isSearch = false;
          router.push("SignIn");
        });
    },
    search() {
      fncSearch(config.searchValue);
    }
  }
};
</script>

<style>
</style>