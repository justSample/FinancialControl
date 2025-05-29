import api from './api'

export async function getAllCategories(idUser) {
  const res = await api.get(`/categories/${idUser}`)
  return res.data
}

export async function addNewCategory(idUser, name, operationType, idParent = null) {
  const res = await api.post('/categories', { idUser, name, operationType, idParent })
  return res.data
}

export async function changeCategory(idUser, idCategory, name, operationType, idParent = null) {
  try {
    const res = await api.put(`/categories/${idCategory}`, {
      idUser,
      name,
      operationType,
      idParent,
    })
    return res.status === 204
  } catch (err) {
    console.log('Ошибка при изменении категории')
    return false
  }
}

export async function deleteCategory(idUser, idCategory) {
  try {
    const res = await api.delete(`/categories/${idUser}/${idCategory}`)
    return res.status === 204
  } catch (err) {
    console.log('Ошибка при удалении категории')
    return false
  }
}

export default {
  getAllCategories,
  addNewCategory,
  changeCategory,
  deleteCategory,
}
