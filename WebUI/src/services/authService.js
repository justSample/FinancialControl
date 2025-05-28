import api from './api'

export function login(login, password) {
  return api.post('/auth/login', { login, password, username: '' })
}

export function register(login, password, username) {
  return api.post('/auth/register', { login, password, username })
}
