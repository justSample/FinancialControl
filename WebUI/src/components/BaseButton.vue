<script setup lang="js">
const emit = defineEmits(['click'])
defineProps({
  name: String,
})

const OnClcik = () => emit('click')
</script>

<template>
  <button
    class="border rounded-2xl p-2 font-bold active:bg-amber-600 active:text-white"
    @click="OnClcik"
  >
    {{ name }}
  </button>
</template>
