<script setup lang="js">
import { ref, computed, watch } from 'vue'
import { defineProps, defineEmits } from 'vue'

const props = defineProps({
  columns: Array, // [{ key: 'name', label: 'Имя' }]
  rows: Array, // [{ id: 1, name: 'Петя' }]
})

const emit = defineEmits(['rowSelected'])

const selectedRow = ref(null)
const sortKey = ref(null)
const sortOrder = ref('asc')

const sortedRows = computed(() => {
  if (!sortKey.value) return props.rows

  return [...props.rows].sort((a, b) => {
    const aVal = a[sortKey.value]
    const bVal = b[sortKey.value]
    const order = sortOrder.value === 'asc' ? 1 : -1

    return aVal > bVal ? order : aVal < bVal ? -order : 0
  })
})

function toggleSort(columnKey) {
  if (sortKey.value === columnKey) {
    sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc'
  } else {
    sortKey.value = columnKey
    sortOrder.value = 'asc'
  }
}

function selectRow(row) {
  selectedRow.value = row
  emit('rowSelected', row)
}
</script>

<template>
  <table class="w-full border-collapse border border-gray-300">
    <thead class="bg-gray-100">
      <tr>
        <th
          v-for="col in props.columns"
          :key="col.key"
          class="border border-gray-300 p-2 cursor-pointer text-center"
          @click="toggleSort(col.key)"
        >
          {{ col.label }}
          <span v-if="sortKey === col.key">
            {{ sortOrder === 'asc' ? '▲' : '▼' }}
          </span>
        </th>
      </tr>
    </thead>
    <tbody>
      <tr
        v-for="row in sortedRows"
        :key="row.id"
        @click="selectRow(row)"
        :class="['cursor-pointer', selectedRow?.id === row.id ? 'bg-blue-100' : '']"
        class="text-center"
      >
        <td v-for="col in props.columns" :key="col.key" class="border border-gray-300 p-2">
          {{ row[col.key] }}
        </td>
      </tr>
    </tbody>
  </table>
</template>
