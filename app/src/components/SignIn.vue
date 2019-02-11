<template>
  <div class="signup">
    <v-card>
      <v-container>
        <v-layout>
          <form>
            <v-text-field label="email" v-model="model.email" required></v-text-field>
            <v-text-field label="password" type="password" v-model="model.password" required></v-text-field>
            <v-btn @click="signup()">Sign up</v-btn>
          </form>
        </v-layout>
      </v-container>
    </v-card>
  </div>
</template>

<script>
import api from "@/api/AccountService";
import Vue from 'vue';

export default {
  name: "signup",
  data() {
    return {
      model: { email: "", password: "" }
    };
  },
  methods: {
    async signup() {
       var token = await api.get(this.model.email, this.model.password);
       Vue.prototype.$auth.setAccessToken(token);
       this.$router.go("/Messages");
    }
  }
};
</script>

<style>
</style>
