<template>
  <div  class="signup">
    <v-card>
      <v-container fluid grid-list-lg>
        <v-layout  row wrap>

           <form>
    <v-text-field
      :counter="10"
      label="Name"
      data-vv-name="name"
      required
    ></v-text-field>
    <v-text-field
      label="E-mail"
      data-vv-name="email"
      required
    ></v-text-field>
    <v-select
      label="Select"
      data-vv-name="select"
      required
    ></v-select>
    <v-checkbox
      value="1"
      label="Option"
      data-vv-name="checkbox"
      type="checkbox"
      required
    ></v-checkbox>

    <v-btn>submit</v-btn>
    <v-btn>clear</v-btn>
  </form>

        </v-layout>
      </v-container>
    </v-card>
  </div>
</template>

<script>
import api from "@/api/AccountService";

export default {
  name: "signup",
  data() {
    return {
    };
  },
  async created() {
  },
  methods: {}
};
</script>

<style>
</style>
