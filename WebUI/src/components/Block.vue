<script setup lang="js">
const props = defineProps({
  title: String,
})
</script>

<template>
  <div class="*:p-2">
    <div class="text-center text-2xl bg-green-300 rounded-t-xl">{{ title }}</div>
    <div class="grid grid-cols-2 bg-red-400 rounded-b-xl items-center place-items-center">
      <slot></slot>
    </div>
  </div>
</template>
