<template>
  <div class="passwordchange">
        <v-container grid-list-xl text-xs-center>
      <v-layout row wrap>
        <v-flex xs10 offset-xs1>
          <v-card class="elevation-12">
            <v-toolbar dark color="primary">
              <v-toolbar-title>Change Password</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
              <v-form>
                <v-text-field label="Old Password" type="password" v-model="model.oldPassword" :rules="[rules.required]" ></v-text-field>
                <v-text-field label="New Password" type="password" v-model="model.newPassword" :rules="[rules.required, rules.min]" ></v-text-field>
                <v-text-field label="Confirm New Password" type="password" v-model="model.confirmPassword" :rules="[rules.required, rules.confirmPassword]" ></v-text-field>
              </v-form>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn @click="change()">Change</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import api from "@/api/PasswordService";
import apiSession from "@/api/SessionService";
import apiMaster from "@/App";
import Vue from "vue";

export default {
  name: "passwordchange",
  data() {
    return {
      model: { newPassword: "", confirmPassword: "", oldPassword: "" },
      show1: false,
      show2: true,
      show3: false,
      show4: false,
      password: "Password",
      rules: {
        required: value => !!value || "required",
        min: v => v.length >= 8 || "Min 8 characters.",
        confirmPassword: () => {
          if (this.model.newPassword != this.model.confirmPassword)
            return "The password you entered don't match.";
          return "";
        }
      }
    };
  },
  async created() {},
  methods: {
    async change() {
      if (
        !this.model.newPassword ||
        !this.model.oldPassword ||
        !this.model.confirmPassword
      ) {
        alert("Enter the required fields.");
        return;
      }

      if (this.model.newPassword != this.model.confirmPassword) {
        alert("The password you entered don't match.");
        return;
      }

      await api.put(this.model);

      await Vue.prototype.$auth.getSessionToken().then(function(token) {
        apiSession.delete(token);
      });
      Vue.prototype.$auth.endSession();
      apiMaster.reload();
      this.$router.push("SignIn");
    }
  }
};
</script>

<style>
</style>
