// Vue imports
import Vue from 'vue'
import Router from 'vue-router'

// our own imports
import Map from '@/components/Map'
import Messages from '@/components/Messages'
import SignUp from '@/components/SignUp'
import SignIn from '@/components/SignIn'

Vue.use(Router)

let router = new Router({
  mode: 'history',
  routes: [{
      path: '/SignUp',
      name: 'SignUp',
      component: SignUp
    },
    {
      path: '/SignIn',
      name: 'SignIn',
      component: SignIn
    },
    {
      path: '/',
      name: 'Map',
      component: Map,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/Map',
      name: 'Map',
      component: Map,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/Messages',
      name: 'Messages',
      component: Messages,
      meta: {
        requiresAuth: true
      }
    },
  ]
})

export default router

const onAuthRequired = async (from, to, next) => {
  if (from.matched.some(record => record.meta.requiresAuth) && !(await Vue.prototype.$auth.isAuthenticated())) {
    next({
      path: '/SignIn'
    })
  } else {
    next()
  }
}

router.beforeEach(onAuthRequired)
