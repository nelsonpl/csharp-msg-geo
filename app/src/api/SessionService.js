import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'http://localhost:56967/api/session',
  json: true
})

export default {
  async execute(method, resource, data, params) {
    return client({
      method,
      url: resource,
      data,
      params,
      headers: {}
    }).then(req => {
      return req.data
    })
  },
  delete(token) {
    return this.execute('delete', '/', null, { 'token': token }).catch(function (error) {
      alert(error.message);
    })
  }
}
