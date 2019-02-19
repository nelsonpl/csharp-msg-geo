import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'http://localhost:56967/api/account',
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
  create(data) {
    return this.execute('post', '/', data).catch(function (error) {
      alert(error.message);
    });
  }
}
