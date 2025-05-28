<script setup lang="js">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { login } from '@/services/authService'
import BaseButton from '@/components/BaseButton.vue'

const userLogin = ref('')
const password = ref('')
const router = useRouter()

const handleLogin = async () => {
  try {
    console.log(userLogin.value)
    console.log(password.value)
    const res = await login(userLogin.value, password.value)
    localStorage.setItem('token', res.data.token)
    localStorage.setItem('user', JSON.stringify(res.data.user))
    router.push('/')
  } catch (err) {
    alert('Логин или пароль неверные')
  }
}

const goRegister = () => {
  router.push('/register')
}
</script>

<template>
  <div class="h-[80vh] flex justify-center items-center select-none">
    <div class="bg-red-500 p-3 rounded-2xl">
      <div>
        <div class="text-xl font-bold">Логин</div>
        <div>
          <input
            class="border border-black rounded-md w-full text-white focus:outline-none pl-1"
            type="text"
            v-model="userLogin"
          />
        </div>
      </div>
      <div class="mt-2">
        <div class="text-xl font-bold">Пароль</div>
        <div>
          <input
            class="border border-black rounded-md w-full text-white focus:outline-none pl-1"
            type="password"
            v-model="password"
          />
        </div>
      </div>
      <div class="grid grid-cols-2 gap-2">
        <BaseButton name="Войти" class="mt-2" @click="handleLogin" />
        <BaseButton name="Регистрация" @click="goRegister" class="mt-2" />
      </div>
    </div>
  </div>
</template>
