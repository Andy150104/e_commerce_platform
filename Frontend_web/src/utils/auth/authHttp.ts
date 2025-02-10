import axios from 'axios'
import aspida from '@aspida/axios'
import { useAuthStore } from '@PKG_SRC/stores/master/authStore'
import { useRuntimeConfig } from 'nuxt/app'
import api from '@PKG_SRC/utils/auth/define/$api'

export const useTokenClientAxios = () => {
  const runtimeConfig = useRuntimeConfig()
  const baseURL = runtimeConfig.public.identityBaseUrl

  const apiClientAxios = axios.create({
    baseURL,
  })
  return apiClientAxios
}
export const useLogoutClient = () => {
  const runtimeConfig = useRuntimeConfig()
  const baseURL = runtimeConfig.public.identityBaseUrl
  const authStore = useAuthStore()
  const apiClientAxios = axios.create({
    baseURL,
  })
  apiClientAxios.interceptors.request.use((request) => {
    request.headers.Authorization = 'Bearer ' + authStore.accessToken
    request.headers['Content-Type'] = 'application/json'
    return request
  })

  const apiClient = api(aspida(apiClientAxios))

  return apiClient
}
export const useApiServer = () => {
  const runtimeConfig = useRuntimeConfig()
  const baseURL = runtimeConfig.public.identityBaseUrl
  const authStore = useAuthStore()
  const apiClientAxios = axios.create({
    baseURL,
  })
  const apiServer = api(aspida(apiClientAxios))

  return apiServer
}
