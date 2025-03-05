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
import { useProfileStore } from '../Modules/DashBoard/profileStore'
import { useFormMessageStore } from './formMessageStore'

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
      const profileStore = useProfileStore()
      const loadingStore = useLoadingStore()
      const authParams = new URLSearchParams()
      authParams.append('grant_type', 'password')
      authParams.append('client_id', 'service_client')
      authParams.append('client_secret', runtimeConfig.public.clientSecret)
      authParams.append('username', userId)
      authParams.append('password', password)
      authParams.append('UserNameOrEmail', userNameOrEmail)
      const formMessageStore = useFormMessageStore()
      loadingStore.LoadingChange(true)
      try {
        const res = await AuthAPI.Token.post(authParams)
        this.SetTokens(res)
        const userStore = useUserStore()
        await userStore.SetMypm730(userStore.UserInfo.grpId, false)
        profileStore.GetProfile()
        userStore.UserInfo.usrId = userId
        formMessageStore.SetFormMessageNotApiRes('I00001', true, 'Login Successfully')

        // if (
        //   (userStore.UserInfo.grpId === UserInfoGrpId.personal && groupFlg === true) ||
        //   (userStore.UserInfo.grpId === UserInfoGrpId.group && groupFlg === false)
        // ) {
        //   return;
        // }

        userStore.SetGroupAuth(groupFlg)

        router.push('/Home')
        // window.location.href = 'http://localhost:3000/Home'
      } catch (error) {
        console.error('Login error:', error)
        if (axios.isAxiosError(error) && error.response) {
          const errorDescription = error.response.data?.error_description || 'Unknown error'
          formMessageStore.SetFormMessageNotApiRes('E00001', true, errorDescription)
        }
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
    async Logout() {
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const logoutClient = useLogoutClient()
      const formMessageStore = useFormMessageStore()
      this.ResetStore()
      setTimeout(() => {
        loadingStore.LoadingChange(false)
        const router = useRouter()
        window.location.href = "http://localhost:3000/Home"
      }, 1000)
      formMessageStore.SetFormMessageNotApiRes('I000001', true, "Logout successfully!!!")
    },
  },
  persist: true,
})
