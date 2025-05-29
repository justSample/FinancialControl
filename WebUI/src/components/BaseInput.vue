<script setup lang="js">
defineProps({
  modelValue: {
    type: String,
    required: true,
  },
  inputType: String,
})

defineEmits(['update:modelValue'])
</script>

<template>
  <input
    class="border border-black rounded-md w-full text-white focus:outline-none pl-1"
    :type="inputType ? inputType : 'text'"
    :value="modelValue"
    @input="$emit('update:modelValue', $event.target.value)"
  />
</template>
