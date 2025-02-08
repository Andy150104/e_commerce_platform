import { useRuntimeConfig } from 'nuxt/app'
import { defineStore } from 'pinia'
import { useLoginStore } from '../Modules/loginStore'
import { useLoadingStore } from '../Modules/usercontrol/loadingStore'
import { AuthAPI } from '@PKG_SRC/utils/auth/authClient'
import { useRouter, type Router } from 'vue-router'
import { useUserStore } from './userStore'
import { UserInfoGrpId } from '@PKG_SRC/types/enums/constantFrontend'
import axios from 'axios'
import { useLogoutClient } from '@PKG_SRC/utils/auth/authHttp'
import type { TokenResponse } from '@PKG_SRC/utils/auth/define/@types'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    isAuthorization: false,
    accessToken: '',
    refreshToken: '',
    expiresIn: 0,
    scope: '',
  }),
  getters: {},
  actions: {
    ResetStore() {
      this.isAuthorization = false
      this.accessToken = ''
      this.refreshToken = ''
      this.expiresIn = 0
      this.scope = ''
    },
    SetTokens(tokens: TokenResponse) {
      this.isAuthorization = true
      this.accessToken = tokens.access_token
      this.refreshToken = tokens.refresh_token
      this.expiresIn = tokens.expires_in
      this.scope = tokens.scope
    },

    async Login(userId: string, password: string, userNameOrEmail: string, groupFlg: boolean, router: Router) {
      const runtimeConfig = useRuntimeConfig()
      const loginStore = useLoginStore()
      const loadingStore = useLoadingStore()
      const authParams = new URLSearchParams()
      authParams.append('grant_type', 'password')
      authParams.append('client_id', 'service_client')
      authParams.append('client_secret', runtimeConfig.public.clientSecret)
      authParams.append('username', userId)
      authParams.append('password', password)
      authParams.append('UserNameOrEmail', userNameOrEmail)

      loadingStore.LoadingChange(true)
      try {
        const res = await AuthAPI.Token.post(authParams)
        console.log('Login', res)
        this.SetTokens(res)
        const userStore = useUserStore()
        await userStore.SetMypm730(userStore.UserInfo.grpId, false)
        userStore.UserInfo.usrId = userId

        // if (
        //   (userStore.UserInfo.grpId === UserInfoGrpId.personal && groupFlg === true) ||
        //   (userStore.UserInfo.grpId === UserInfoGrpId.group && groupFlg === false)
        // ) {
        //   return;
        // }

        userStore.SetGroupAuth(groupFlg)

        if (groupFlg) {
          router.push('/Home')
          // window.location.href = 'http://localhost:3000/Home'
        }
      } catch (error) {
        console.error('Login error:', error)
      } finally {
        loadingStore.LoadingChange(false)
      }
    },
    async GetRereshToken() {
      const runtimeConfig = useRuntimeConfig()
      const authParams = new URLSearchParams()
      authParams.append('grant_type', 'refresh_token') // 固定
      authParams.append('client_id', 'default-client') // 固定
      authParams.append('client_secret', runtimeConfig.public.clientSecret)
      authParams.append('refresh_token', this.refreshToken)
      let apiResult = false
      for (let i = 0; i < 3; i++) {
        const res = AuthAPI.Token.post(authParams)
        await res
          .then((result: TokenResponse) => {
            //(status=200）
            this.SetTokens(result)
            apiResult = true
          })
          .catch(() => {})
        if (apiResult) return
        await sleepByPromise(Math.floor(Math.random() * 101))
      }
      if (apiResult === false) this.ResetStore()
    },
    async Logout(groupFlg: boolean) {
      //   const loadingStore = useLoadingStore()
      //   loadingStore.LoadingChange(true)
      //   const logoutClient = useLogoutClient()
      //   const res = logoutClient.api.v1.Logout.$get()
      //   await res.finally(() => {
      //     loadingStore.LoadingChange(false)
      //     this.ResetStore()
      //     const router = useRouter()
      //     if (groupFlg) {
      //       router.push('/Mypg010')
      //     } else {
      //       router.push('/Mypp010')
      //     }
      //   })
    },
  },
  persist: true,
})
