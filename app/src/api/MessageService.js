import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'http://localhost:56967/api/message',
  json: true
})

export default {
  async execute(method, resource, data) {
    const accessToken = await Vue.prototype.$auth.getSessionToken();
    return client({
      method,
      url: resource,
      data,
      headers: {
        Authorization: `${accessToken}`
      }
    }).then(req => {
      return req.data
    })
  },
  get() {
    return this.execute('get', '/').catch(function (error) {
      alert(error.message);
    });
  },
  create(data) {
    return this.execute('post', '/', data).catch(function (error) {
      alert(error.message);
    });
  },
  update(id, data) {
    return this.execute('put', `/${id}`, data)
  },
  delete(id) {
    return this.execute('delete', `/${id}`)
  }
}
