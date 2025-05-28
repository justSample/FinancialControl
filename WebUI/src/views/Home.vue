<script setup lang="js">
import { useRouter } from 'vue-router'

const router = useRouter()

const logout = async () => {
  localStorage.removeItem('token')
  router.push('/login')
}
</script>

<template>
  <div>Поздравляю! Это дом!</div>
  <button @click="logout" class="border p-2 rounded-2xl m-1">Выход</button>
</template>
