import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'http://localhost:56967/api/map',
  json: true
})

export default {
  async execute(method, resource, data, params) {
    const accessToken = await Vue.prototype.$auth.getSessionToken();
    return client({
      method,
      url: resource,
      data,
      params,
      headers: {
        Authorization: `${accessToken}`
      }
    }).then(req => {
      return req.data
    })
  },
  get(w, e, s, n) {
    return this.execute('get', '/', null, {
      west: w,
      east: e,
      south: s,
      north: n
    }).catch(function (error) {
      alert(error.message);
    });
  },
}
