import Vue from 'vue';
import App from './App';
import router from './router';
import Vuetify from 'vuetify';
import firebase from 'firebase';

import 'vuetify/dist/vuetify.min.css';

// 
const config = {
  apiKey: "AIzaSyDRoJEKEFGwuX7nKQ9EX4Tm5lE4IEES9m4",
  authDomain: "npx-mwg.firebaseapp.com",
  databaseURL: "https://npx-mwg.firebaseio.com",
  projectId: "npx-mwg",
  storageBucket: "npx-mwg.appspot.com",
  messagingSenderId: "422307222319"
};

firebase.initializeApp(config);

Vue.use(Vuetify)

import {
  LMap,
  LTileLayer,
  LMarker,
  LGeoJson
} from 'vue2-leaflet';


Vue.component('l-map', LMap);
Vue.component('l-tile-layer', LTileLayer);
Vue.component('l-marker', LMarker);
Vue.component('l-geo-json', LGeoJson);

// eslint-disable-next-line
delete L.Icon.Default.prototype._getIconUrl;
// eslint-disable-next-line
L.Icon.Default.mergeOptions({
  iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
  iconUrl: require('leaflet/dist/images/marker-icon.png'),
  shadowUrl: require('leaflet/dist/images/marker-shadow.png')
});

Vue.config.productionTip = false;

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  template: '<App/>',
  components: {
    App
  }
});