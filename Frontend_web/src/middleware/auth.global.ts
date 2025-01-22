import { useAuthStore } from "@PKG_SRC/stores/master/authStore"
import { useUserStore } from "@PKG_SRC/stores/master/userStore"
import { defineNuxtRouteMiddleware } from "nuxt/app"

export default defineNuxtRouteMiddleware(async (to) => {
  const authStore = useAuthStore()
  const userStore = useUserStore()
  const relativePath = to.path.split('/').filter(Boolean).join('/')

  const loginNotRequiredRoutes = [
    '',
    'Mypp',
    'Test',
    'test2',
    'mode',
    'Home',
  ]
  if (!authStore.isAuthorization && !loginNotRequiredRoutes.includes(relativePath)) {
    return { path: '/' }
  }
  if (!authStore.isAuthorization) return
})
