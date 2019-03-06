import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'http://localhost:56967/api/message',
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
  get(searchText) {
    return this.execute('get', '/', null, {search: searchText}).catch(function (error) {
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
