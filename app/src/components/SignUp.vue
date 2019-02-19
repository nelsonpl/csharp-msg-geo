<template>
  <div class="signup">
    <v-container grid-list-xl text-xs-center>
      <v-layout row wrap>
        <v-flex xs10 offset-xs1>
          <v-card class="elevation-12">
            <v-toolbar dark color="primary">
              <v-toolbar-title>Sign Up</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-form>
                <v-text-field
                  label="Name"
                  type="text"
                  v-model="model.name"
                  :rules="[rules.required]"
                ></v-text-field>
                <v-text-field label="e-mail" v-model="model.email" :rules="[rules.required]"></v-text-field>
                <v-text-field
                  label="Password"
                  type="password"
                  v-model="model.password"
                  :rules="[rules.required, rules.min]"
                ></v-text-field>
                <v-text-field
                  label="Confirm Password"
                  type="password"
                  v-model="model.confirmPassword"
                  :rules="[rules.required, rules.confirmPassword]"
                ></v-text-field>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn @click="signup()">Sign up</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import api from "@/api/AccountService";

export default {
  name: "signup",
  data() {
    return {
      model: { email: "", password: "", name: "", confirmPassword: "" },
      show1: false,
      show2: true,
      show3: false,
      show4: false,
      password: "Password",
      rules: {
        required: value => !!value || "required",
        min: v => v.length >= 8 || "Min 8 characters.",
        confirmPassword: () => {
          if (this.model.password != this.model.confirmPassword)
            return "The password you entered don't match.";
          return "";
        }
      }
    };
  },
  async created() {},
  methods: {
    async signup() {

      if(!this.model.email || !this.model.name || !this.model.password || !this.model.confirmPassword){
        alert("Enter the required fields.");
        return;
      }

      if(this.model.password != this.model.confirmPassword){
        alert("The password you entered don't match.");
        return;
      }

      await api.create(this.model);
      this.$router.push("SignIn");
    }
  }
};
</script>

<style>
</style>
