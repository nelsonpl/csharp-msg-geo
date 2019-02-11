// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import Vuetify from 'vuetify'

import 'vuetify/dist/vuetify.min.css'

Vue.use(Vuetify)

import {
  LMap,
  LTileLayer,
  LMarker,
  LGeoJson
} from 'vue2-leaflet'


Vue.component('l-map', LMap);
Vue.component('l-tile-layer', LTileLayer);
Vue.component('l-marker', LMarker);
Vue.component('l-geo-json', LGeoJson);

// eslint-disable-next-line
delete L.Icon.Default.prototype._getIconUrl
// eslint-disable-next-line
L.Icon.Default.mergeOptions({
  iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
  iconUrl: require('leaflet/dist/images/marker-icon.png'),
  shadowUrl: require('leaflet/dist/images/marker-shadow.png')
})

Vue.config.productionTip = false;
Vue.prototype.$auth = {
  setAccessToken: function (token) {
    localStorage.npxgeomsgtoken = token;
  },
  getAccessToken: async function () {
    if (localStorage.npxgeomsgtoken)
      return localStorage.npxgeomsgtoken;
    return "";
  },
  isAuthenticated: function () { 
    var flag = localStorage.npxgeomsgtoken != null && localStorage.npxgeomsgtoken != ""; 
    return flag;
  }
};

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: {
    App
  }
})
