// Vue imports
import Vue from 'vue';
import Router from 'vue-router';
import firebase from 'firebase';

// our own imports
import Map from '@/components/Map';
import Messages from '@/components/Messages';
import SignUp from '@/components/SignUp';
import SignIn from '@/components/SignIn';
import PasswordChange from '@/components/PasswordChange';

Vue.use(Router);

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
    {
      path: '/PasswordChange',
      name: 'PasswordChange',
      component: PasswordChange,
      meta: {
        requiresAuth: true
      }
    },
  ]
});

export default router;

const onAuthRequired = async (to, from, next) => {

  const currentUser = firebase.auth().currentUser;
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth);

  if (requiresAuth && !currentUser) next('SignIn');
  else next();

  next();

};

router.beforeEach(onAuthRequired);