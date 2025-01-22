import aspida from '@aspida/axios'
import api from '@PKG_API/$api'
import { useAuthStore } from '@PKG_SRC/stores/master/authStore'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { useLoadingStore } from '@PKG_SRC/stores/Modules/usercontrol/loadingStore'
import axios from 'axios'
import { useRuntimeConfig } from 'nuxt/app'
import { useRouter } from 'vue-router'

export const useApiClient = () => {
  const router = useRouter()
  const runtimeConfig = useRuntimeConfig()
  const loadingStore = useLoadingStore()
  const authStore = useAuthStore()
  const baseURL = runtimeConfig.public.apiBaseUrl

  const apiClientAxios = axios.create({
    baseURL,
  })
  const apiRetryAxios = axios.create({
    baseURL,
  })
  apiClientAxios.interceptors.request.use((request) => {
    request.headers.Authorization = 'Bearer ' + authStore.accessToken
    request.headers['Content-Type'] = 'application/json'

    loadingStore.LoadingChange(true)
    return request
  })

  apiClientAxios.interceptors.response.use(
    (response) => {
      setTimeout(function () {
        loadingStore.LoadingChange(false)
      }, 250)
      return response
    },
    async (error) => {
      setTimeout(function () {
        loadingStore.LoadingChange(false)
      }, 250)
      if (error.response.status === 401){
        if (authStore.isAuthorization){
          let accessToken = ''
          await authStore.GetRereshToken().then(() =>{
            accessToken = authStore.accessToken
          })
          error.config.headers.Authorization = `Bearer ${accessToken}`
          
          const response = apiRetryAxios.request(error.config)
          return response
        } else {
          router.push('/?error=timeout')
          return null
        }
      } else {
        const message = SYSTEM_ERROR_BACKEND.message + ' ' + (error.response?.status ?? '') + ' ' + (error.message ?? '')

        const formMessageStore = useFormMessageStore()
        formMessageStore.SetFormMessage({messageId: SYSTEM_ERROR_BACKEND.messageId, message: message, success: false})

        return null
      }
    }
  )
  apiRetryAxios.interceptors.response.use(
    (response) => response,
    (error) => {
      if (error.response && error.response.status === 401) {
        router.push('/?error=timeout') 
      } else {
        router.push('/?error=systemError') 
      }
    }
  )

  const apiClient = api(aspida(apiClientAxios))

  return apiClient
}
export type ApiClient = ReturnType<typeof useApiClient>
