<script setup lang="js">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { login } from '@/services/authService'

const userLogin = ref('')
const password = ref('')
const router = useRouter()

const handleLogin = async () => {
  try {
    const res = await login(userLogin.value, password.value)
    localStorage.setItem('token', res.data.token)
    router.push('/')
  } catch (err) {
    alert('Логин или пароль неверные')
  }
}
</script>

<template>
  <div>Это страница авторизации</div>
  <div>
    <input class="border" type="text" v-model="userLogin" />
    <input class="border" type="password" v-model="password" />
  </div>
  <div>
    <div>{{ userLogin }}</div>
    <div>{{ password }}</div>
  </div>

  <button
    class="border rounded-3xl m-1 p-2 text-2xl font-bold bg-green-700 text-white border-black"
    @click="handleLogin"
  >
    Кнопка
  </button>
</template>
