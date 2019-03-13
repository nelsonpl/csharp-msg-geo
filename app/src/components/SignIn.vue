<template>
  <div class="signin">
    <v-container grid-list-xl text-xs-center>
      <v-layout row wrap>
        <v-flex xs10 offset-xs1>
          <v-card class="elevation-12">
            <v-toolbar dark color="primary">
              <v-toolbar-title>Sign In</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-form>
                <v-text-field label="email" v-model="model.email" required></v-text-field>
                <v-text-field label="password" type="password" v-model="model.password" required></v-text-field>
                <v-checkbox label="Remember me"></v-checkbox>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn @click="signin()">Sign in</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import apiMaster from "@/App";
import Vue from "vue";
import firebase from "firebase";

export default {
  name: "signin",
  data() {
    return {
      model: { email: "", password: "" }
    };
  },
  created: function() {
    apiMaster.reload();
  },
  methods: {
    async signin() {
      var router = this.$router;

      firebase
        .auth()
        .signInWithEmailAndPassword(this.model.email, this.model.password)
        .then(
          function(user) {
            apiMaster.reload();
            router.push("Messages");
          },
          function(err) {
            alert("Oops. " + err.message);
          }
        );
    }
  }
};
</script>

<style>
</style>
