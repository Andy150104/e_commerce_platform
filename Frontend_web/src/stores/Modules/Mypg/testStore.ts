import { defineStore } from 'pinia'

import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { useAuthStore } from '@PKG_SRC/stores/master/authStore'

/** 認証 */
export const useTestStore = defineStore('testStore', {
  state: () => ({}),
  // stateと同じgetterは宣言不要
  getters: {},
  actions: {
    TestAuth() {
        const auth = useAuthStore()
        auth.isAuthorization = true
    },
    ResetStore(){
        const auth = useAuthStore()
        auth.ResetStore()
        console.log('auth',auth.isAuthorization)
    },
    persist: true, // 永続化
  },
})
