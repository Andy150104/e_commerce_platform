import { defineStore } from 'pinia'
import { useApiServer } from '@PKG_SRC/composables/auth/authHttp'
import { useAuthStore } from './authStore'

export type VerifyTokenState = {
  roleName: string
}

export const useVerifyTokenStore = defineStore('VerifyToken', {
  state: (): VerifyTokenState => ({
    roleName: ''
  }),
  actions: {
    async GetRoleName() {
      const apiClient = useApiServer()
      const authStore = useAuthStore()
      const res = await apiClient.api.v1.VerifyToken.$post({
        body: {
          isOnlyValidation: false,
        },
        config:{
          headers: {
            Authorization: `Bearer ${authStore.accessToken}`
          }
        }
      })
      if (!res.success) return false
      this.roleName = res.response.roleName
      return true
    },
  },
})
