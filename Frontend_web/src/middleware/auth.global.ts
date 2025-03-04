import { useAuthStore } from '@PKG_SRC/stores/master/authStore'
import { useUserStore } from '@PKG_SRC/stores/master/userStore'
import { defineNuxtRouteMiddleware } from 'nuxt/app'

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
    'sample',
    'Login',
    'Product2',
    'Register',
    'Register/Verify',
    'ForgotPass',
    'ForgotPass/verify',
    'Blind_Box',
    'Blind_Box/Cart',
    'DashBoard',
    'DashBoard/Account',
    'Service/Buying',
    'Service/Blind_Box',
  ]

  // Kiểm tra nếu không đăng nhập và không thuộc danh sách route cho phép
  if (!authStore.isAuthorization && !loginNotRequiredRoutes.includes(relativePath) && !relativePath.startsWith('Service/Buying/Product')) {
    return { path: '/' }
  }
})
