<template>
  <div class="signin">
    <v-container grid-list-xl text-xs-center>
      <v-layout row wrap>
        <v-flex xs5 offset-xs4>
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
              <v-btn block color="success"  @click="signin()">Sign in</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>

      <v-layout row wrap>
        <v-flex xs5 offset-xs4>
          <v-card class="elevation-12">
            <v-card-text>
              <v-btn color="primary" round @click="signinGoogle()">Google</v-btn>
            </v-card-text>
          </v-card>
        </v-flex>
      </v-layout>

    </v-container>
  </div>
</template>

<script>
import Vue from "vue";
import firebase from "firebase";

export default {
  name: "signin",
  data() {
    return {
      model: { email: "", password: "" }
    };
  },
  created: function() {},
  methods: {
    signin() {
      var router = this.$router;

      firebase
        .auth()
        .signInWithEmailAndPassword(this.model.email, this.model.password)
        .then(
          function(user) { },
          function(err) {
            alert("Oops. " + err.message);
          }
        );
    },
    signinGoogle() {
      var provider = new firebase.auth.GoogleAuthProvider();
      firebase.auth().signInWithPopup(provider);
    }
  }
};
</script>

<style>
</style>
