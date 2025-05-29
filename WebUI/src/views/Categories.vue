<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import apiCategory from '@/services/categoryService'

import BaseButton from '@/components/BaseButton.vue'
import BaseInput from '@/components/BaseInput.vue'
import GridView from '@/components/GridView.vue'

const idUser = ref('')

const newCategory = reactive({
  name: '',
  operationType: -1,
  idParent: null,
  arrCategories: [],
})

const selectedCategory = reactive({
  id: '',
  name: '',
  operationType: -1,
  idParent: null,
  categories: [],
})

const emptySelectedCategory = (emptyArray: Boolean = false) => {
  selectedCategory.name = ''
  selectedCategory.operationType = -1
  selectedCategory.idParent = null
  if (emptyArray) selectedCategory.categories = []
}

const updateCategories = async () => {
  const allCategories = await apiCategory.getAllCategories(idUser.value)
  emptySelectedCategory(true)

  allCategories.forEach((category) => {
    selectedCategory.categories.push(category)
  })

  console.log(allCategories)
}

const createNewCategory = async () => {
  if (newCategory.name.length <= 0) {
    alert('Название новой категории не должно быть пустым')
    return
  }

  if (newCategory.operationType === -1) {
    alert('Финансовая операция должна быть выбрана!')
    return
  }

  const data = await apiCategory.addNewCategory(
    idUser.value,
    newCategory.name,
    newCategory.operationType,
    newCategory.idParent,
  )

  console.log(data)

  selectedCategory.categories.push(data)

  newCategory.name = ''
  newCategory.operationType = -1
  newCategory.idParent = null
  newCategory.arrCategories = []
}

const onSelectedNewFinanceCategory = () => {
  console.log(newCategory.operationType)

  newCategory.arrCategories = []
  newCategory.arrCategories.push(
    ...selectedCategory.categories.filter((x) => x.operationType === newCategory.operationType),
  )
  newCategory.idParent = null
}

const onSelectedCategory = () => {
  console.log(selectedCategory.id)

  const foundCategory = selectedCategory.categories.filter((x) => x.id === selectedCategory.id)[0]
  selectedCategory.name = foundCategory.name
  selectedCategory.operationType = foundCategory.operationType
  selectedCategory.idParent = foundCategory.idParent
}

const editCategory = async () => {
  const result = await apiCategory.changeCategory(
    idUser.value,
    selectedCategory.id,
    selectedCategory.name,
    selectedCategory.operationType,
    selectedCategory.idParent,
  )

  console.log(`changed?: [${result}]`)

  if (result) {
    const foundIndex = selectedCategory.categories.findIndex((x) => x.id === selectedCategory.id)
    selectedCategory.categories[foundIndex].name = selectedCategory.name
    selectedCategory.categories[foundIndex].operationType = selectedCategory.operationType
    selectedCategory.categories[foundIndex].idParent = selectedCategory.idParent
  }
}

const deleteCategory = async () => {
  const result = await apiCategory.deleteCategory(idUser.value, selectedCategory.id)

  console.log(`deleted?: [${result}]`)
  if (result) {
    emptySelectedCategory(false)
    const foundIndex = selectedCategory.categories.findIndex((x) => x.id === selectedCategory.id)
    selectedCategory.categories.splice(foundIndex, 1)
  }
}

onMounted(async () => {
  idUser.value = JSON.parse(localStorage.getItem('user')).id
  updateCategories()
})

const columns = [
  { key: 'id', label: 'ID' },
  { key: 'name', label: 'Название категории' },
  { key: 'operationType', label: 'Операция' },
  { key: 'idParent', label: 'Подкатегория' },
]

function handleRowSelected(row) {
  console.log('Выбранная строка:', row)
}
</script>

<template>
  <div class="mt-2 text-center text-3xl">Категории</div>

  <div class="*:p-2">
    <div class="text-center text-2xl bg-green-300 rounded-t-xl">Добавление новой категории</div>
    <div class="grid grid-cols-2 bg-red-400 rounded-b-xl items-center place-items-center">
      <div class="text-xl mb-2">Название категории</div>
      <div class="w-2/4">
        <BaseInput v-model="newCategory.name" />
      </div>
      <div class="text-xl mb-2">Финансовая операция</div>
      <div class="w-2/4">
        <select
          class="border border-black rounded-md w-full text-black focus:outline-none pl-1 h-full"
          v-model.number="newCategory.operationType"
          @change="onSelectedNewFinanceCategory"
        >
          <option value="-1" selected hidden>Выберите категорию</option>
          <option value="0">Доход</option>
          <option value="1">Расход</option>
        </select>
      </div>
      <div class="text-xl mb-2">Подкатегория(Необязательно)</div>
      <div class="w-2/4">
        <select
          class="border border-black rounded-md w-full text-black focus:outline-none pl-1 h-full"
          v-model="newCategory.idParent"
        >
          <option :value="null" selected hidden>Выберите подкатегорию</option>
          <option v-for="(item, index) in newCategory.arrCategories" :key="index" :value="item.id">
            {{ item.name }}
          </option>
        </select>
      </div>
      <div class="col-span-2 w-2/4">
        <BaseButton name="Добавить" class="w-full" @click="createNewCategory" />
      </div>
    </div>
  </div>

  <div class="*:p-2 mt-2">
    <div class="text-center text-2xl bg-green-300 rounded-t-xl">Редактор категорий</div>
    <div class="grid grid-cols-2 bg-red-400 rounded-b-xl items-center place-items-center">
      <div class="text-xl mb-2">Выбор категории</div>
      <div class="w-2/4">
        <select
          class="border border-black rounded-md w-full text-black focus:outline-none pl-1 h-full"
          v-model.number="selectedCategory.id"
          @change="onSelectedCategory"
        >
          <option value="-1" selected hidden>Выберите категорию</option>
          <option
            v-for="(item, index) in selectedCategory.categories"
            :key="index"
            :value="item.id"
          >
            {{ item.name }}
          </option>
        </select>
      </div>

      <div class="text-xl mb-2">Наименование</div>
      <div class="w-2/4">
        <BaseInput v-model="selectedCategory.name" />
      </div>
      <div class="text-xl mb-2">Финансовая операция</div>
      <div class="w-2/4">
        <select
          class="border border-black rounded-md w-full text-black focus:outline-none pl-1 h-full"
          v-model.number="selectedCategory.operationType"
          @change="onSelectedNewFinanceCategory"
        >
          <option value="-1" selected hidden>Выберите операцию</option>
          <option value="0">Доход</option>
          <option value="1">Расход</option>
        </select>
      </div>
      <div class="text-xl mb-2">Подкатегория</div>
      <div class="w-2/4">
        <select
          class="border border-black rounded-md w-full text-black focus:outline-none pl-1 h-full"
          v-model="selectedCategory.idParent"
        >
          <option :value="null" selected hidden>Выберите подкатегорию</option>
          <option
            v-for="(item, index) in selectedCategory.categories"
            :key="index"
            :value="item.id"
          >
            {{ item.name }}
          </option>
        </select>
      </div>
      <div class="w-11/12">
        <BaseButton name="Изменить" class="w-full" @click="editCategory" />
      </div>
      <div class="w-11/12">
        <BaseButton name="Удалить" class="w-full" @click="deleteCategory" />
      </div>
    </div>
  </div>

  <!--   <GridView
    class="mt-2"
    :columns="columns"
    :rows="selectedCategory.categories"
    @rowSelected="handleRowSelected"
  /> -->
</template>
