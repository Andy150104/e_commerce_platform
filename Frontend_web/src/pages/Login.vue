<template>
  <BaseScreenAuth>
    <template #body>
      <div class="py-20 mx-14">
        <h2 class="text-2xl font-bold text-gray-900 dark:text-white text-center">Login</h2>

        <div class="mt-6">
          <!-- Email -->
          <div class="mb-4">
            <UserControlTextFieldFloatLabel
              ref="userIdInputRef"
              :xml-column="xmlColumns.userId"
              :maxlength="50"
              :disabled="false"
              :err-msg="fieldErrors.userId"
            />
          </div>

          <!-- Password -->
          <div class="mb-4">
            <UserControlTextFieldFloatLabel
              :xml-column="xmlColumns.password"
              :maxlength="50"
              :disabled="false"
              :err-msg="fieldErrors.password"
              :is-password-visibility="true"
              :type="'password'"
              :placeholder="'**********'"
            />
          </div>

          <!-- Terms and Conditions -->
          <div class="mb-4 flex items-center">
            <input id="terms" type="checkbox" class="h-4 w-4 border rounded bg-gray-50 dark:bg-gray-700 dark:border-gray-600 focus:ring-blue-500" />
            <label for="terms" class="ml-2 text-sm text-gray-600 dark:text-gray-400"
              >I accept the <a href="#" class="text-blue-600 dark:text-blue-400 underline">Terms and Conditions</a></label
            >
          </div>

          <!-- Submit Button -->
          <button
            type="submit"
            class="w-full bg-blue-600 text-white py-2.5 rounded-lg text-center font-medium hover:bg-blue-700 dark:bg-blue-500 dark:hover:bg-blue-600"
            @click="Login"
          >
            Login
          </button>
        </div>

        <!-- Login Link -->
        <p class="text-sm text-center text-gray-600 dark:text-gray-400 mt-4">
          You don't have account?
          <a href="/Register" class="text-blue-600 dark:text-blue-400 underline">Sign up</a>
        </p>
      </div>
    </template>
  </BaseScreenAuth>
</template>
<script lang="ts" setup>
  import UserControlTextFieldFloatLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldFloatLabel.vue'
  import BaseScreenAuth from '@PKG_SRC/layouts/Basecreen/BaseScreenAuth.vue'
  import { useAuthStore } from '@PKG_SRC/stores/master/authStore'
  import { useLoginStore } from '@PKG_SRC/stores/Modules/loginStore'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'

  const store = useLoginStore()
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const router = useRouter()
  const userIdInputRef = ref()

  const xmlColumns = {
    userId: XmlLoadColumn({
      id: 'userId',
      name: 'User name',
      rules: '',
      visible: true,
      option: '',
    }),
    password: XmlLoadColumn({
      id: 'password',
      name: 'Password',
      rules: '',
      visible: true,
      option: '',
    }),
  }

  const Login = async () => {
    if (await store.Validate()) {
      const authStore = useAuthStore()
      await authStore.Login(store.fieldValues.userId, store.fieldValues.password, store.fieldValues.userId, true, router)
    }
  }

  onMounted(() => {
    userIdInputRef.value.onFocus()
  })
</script>
