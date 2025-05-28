<script setup lang="js">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { register } from '@/services/authService'

import BaseButton from '@/components/BaseButton.vue'

const username = ref('')
const userLogin = ref('')
const password = ref('')

const router = useRouter()

const reg = async () => {
  try {
    if (username.value.length <= 3 || username.value.trim() === '') {
      alert('Имя пользователя должно быть больше 3 и не пустым!')
      return
    }

    if (userLogin.value.length <= 3 || userLogin.value.trim() === '') {
      alert('Логин должен быть больше 3 и не пустым!')
      return
    }

    if (password.value.length <= 3 || password.value.trim() === '') {
      alert('Пароль должен быть больше 3 и не пустым!')
      return
    }

    const res = await register(userLogin.value, password.value, username.value)
    console.log(res)
    if (res.status === 201) {
      alert('Зарегестрированы!')
      router.push('/login')
    } else {
      alert('Неудачная регистрация!')
    }
  } catch (err) {
    console.log(err)
    alert('Ошибка регистрации')
  }
}

const back = () => router.push('/login')
</script>

<template>
  <div class="h-[80vh] flex justify-center items-center select-none">
    <div class="bg-red-500 p-3 rounded-2xl">
      <div>
        <div class="text-xl font-bold">Ваше имя</div>
        <div>
          <input
            class="border border-black rounded-md w-full text-white focus:outline-none pl-1"
            type="text"
            v-model="username"
          />
        </div>
      </div>
      <div class="mt-2">
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
        <BaseButton name="Регистрация" class="mt-2" @click="reg" />
        <BaseButton name="Назад" class="mt-2" @click="back" />
      </div>
    </div>
  </div>
</template>
