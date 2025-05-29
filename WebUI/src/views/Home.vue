<script setup lang="js">
import BaseButton from '@/components/BaseButton.vue'
import { useRouter } from 'vue-router'
import { onMounted, reactive } from 'vue'

const router = useRouter()
const user = reactive({})

const logout = async () => {
  localStorage.removeItem('token')
  localStorage.removeItem('user')
  router.push('/login')
}

const goTo = (route) => {
  router.push(route)
}

onMounted(() => {
  const userStorage = JSON.parse(localStorage.getItem('user'))
  Object.assign(user, userStorage)
  console.log(user)
})
</script>

<template>
  <div class="text-center text-3xl">Добро пожаловать, {{ user.username }}!</div>
  <div class="text-center text-2xl">Начальный остаток, {{ user.startBalance }}!</div>
  <div class="grid grid-cols-1 gap-2 m-2">
    <BaseButton name="Категории" @click="goTo('/categories')" />
    <BaseButton name="Финансовые операции" @click="goTo('/financial')" />
    <BaseButton name="Отчёты" />
    <BaseButton name="Выход" @click="logout" />
  </div>
</template>
