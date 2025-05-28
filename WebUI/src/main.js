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

  // Если маршрут требует авторизации и токена нет
  if (to.meta.reqAuth && !token) {
    next('/login')
  }
  // Если маршрут не требует авторизации и пользователь авторизован
  else if (!to.meta.reqAuth && token) {
    next('/') // например, не пускать на /login, если уже вошёл
  } else {
    next() // всё ок
  }
})

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.mount('#app')
