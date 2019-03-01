import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'http://localhost:56967/api/password',
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
  put(model) {
    return this.execute('put', '/', model).catch(function (error) {
      alert(error.message);
    });
  }
}
