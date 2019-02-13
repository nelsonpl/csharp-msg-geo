<template>
  <div class="signup">
    <v-flex xs12>
      <v-card class="elevation-12">
        <v-toolbar dark color="primary">
          <v-toolbar-title>Sign Up</v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-form>
            <v-text-field label="Name" v-model="model.name" required></v-text-field>
            <v-text-field label="e-mail" v-model="model.email" required></v-text-field>
            <v-text-field label="Password" type="password" v-model="model.password" required></v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn @click="signup()">Sign up</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </div>
</template>

<script>
import api from "@/api/AccountService";

export default {
  name: "signup",
  data() {
    return {
      model: { email: "", password: "", name: "" }
    };
  },
  async created() {},
  methods: {
    async signup() {
      await api.create(this.model);
      this.$router.go("/Messages");
    }
  }
};
</script>

<style>
</style>
