import './css/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createWebHistory, createRouter } from 'vue-router'
// import router from './router'

import App from './App.vue'

import LoginView from './views/Login.vue'
import HomeView from './views/Home.vue'
import RegisterView from './views/Register.vue'

const routes = [
  { path: '/login', component: LoginView, meta: { reqAuth: false } },
  { path: '/register', component: RegisterView, meta: { reqAuth: false } },
  { path: '/', component: HomeView, meta: { reqAuth: true } },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')

  if (!to.meta.reqAuth) {
    next()
  } else if (token && to.meta.reqAuth) {
    next()
  } else {
    next('/login')
  }

  if (to.meta.reqAuth && !token) {
    next('/')
  } else {
    next()
  }
})

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.mount('#app')
