<template>
  <BaseScreenAuth>
    <template #body>
      <div><ProgressStepper :items="stepperStore.steppList" class="pt-5" /></div>
      <div v-if="store.createFlgVerifyPass">
        <div class="py-15 mx-14 text-center">
          <div class="space-y-4">
            <h2 class="text-2xl font-semibold">Reset Password</h2>
            <p class="text-sm text-gray-600 dark:text-gray-400">Please Enter your new password.</p>
          </div>
        </div>
        <div>
            <LabelItem :xml-column="xmlColumns.password" />
            <BaseControlTextField
              :xml-column="xmlColumns.password"
              :maxlength="50"
              :disabled="false"
              :type="'password'"
              :err-msg="fieldErrors.password"
              :placeholder="'Create Password'"
            />
          </div>
          <div>
            <LabelItem :xml-column="xmlColumns.confirmPassword" />
            <BaseControlTextField
              :xml-column="xmlColumns.confirmPassword"
              :maxlength="50"
              :disabled="false"
              :type="'password'"
              :err-msg="fieldErrors.password"
              :placeholder="'Confirm Password'"
            />
          </div>
        <div class="flex justify-between mt-8 pb-5">
          <button :class="className.BUTTON_DEFAULT_GRAY_1" @click="onBackStep">Back</button>
          <button :class="className.BUTTON_DEFAULT_BLUE_2" @click="onMoveToNextStep">Next</button>
        </div>
      </div>
      <div v-else>
        <section class="bg-white dark:bg-gray-900">
          <div class="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 grid gap-8 lg:gap-16">
            <div class="flex flex-col justify-center">
              <h1 class="mb-4 text-4xl font-extrabold tracking-tight leading-none text-gray-900 md:text-5xl lg:text-6xl dark:text-white">
                Password changed successfully!!!!
              </h1>
              <p class="mb-8 text-lg font-normal text-gray-500 lg:text-xl dark:text-gray-400">Continue to login</p>
              <div class="flex flex-col space-y-4 sm:flex-row sm:space-y-0">
                <a
                  href="http://localhost:3000/Login"
                  class="inline-flex justify-center items-center py-3 px-5 text-base font-medium text-center text-white rounded-lg bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 dark:focus:ring-blue-900"
                >
                  To Login
                  <svg class="w-3.5 h-3.5 ms-2 rtl:rotate-180" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 10">
                    <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M1 5h12m0 0L9 1m4 4L9 9" />
                  </svg>
                </a>
              </div>
            </div>
          </div>
        </section>
      </div>
    </template>
  </BaseScreenAuth>
</template>

<script setup lang="ts">
  import BaseScreenAuth from '@PKG_SRC/layouts/Basecreen/BaseScreenAuth.vue'
  import { useStepperStore } from '@PKG_SRC/stores/Modules/Register/StepperStore'
  import ProgressStepper from '@PKG_SRC/components/Stepper/ProgressStepper.vue'
  import { StepStatus } from '@PKG_SRC/types/enums/constantFrontend'
  import type { StepItem } from '@PKG_SRC/types'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { className } from '@PKG_SRC/utils/class/className'
  import LabelItem from '@PKG_SRC/components/Basecontrol/LabelItem.vue'
  import BaseControlTextField from '@PKG_SRC/components/Basecontrol/BaseControlTextField.vue'
  import { useVerifyPasswordStore } from '@PKG_SRC/stores/Modules/ForgotPass/verifyPassword'
  import { useForm } from 'vee-validate'
  import { useRoute } from 'vue-router'

  const route = useRoute()
  const stepperStore = useStepperStore()
  const successMessage = ref(false)
  const store = useVerifyPasswordStore()
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  
  store.SetFields(formContext)
  const steppList = ref<StepItem[]>([
    {
      nameItem: 'Create new password',
      status: StepStatus.currentStep,
    },
    {
      nameItem: 'Complete',
      status: StepStatus.finishStep,
    },
  ])
  const xmlColumns = {
    password: XmlLoadColumn({
      id: 'password',
      name: 'password',
      rules: 'required',
      visible: true,
      option: '',
    }),
    confirmPassword: XmlLoadColumn({
      id: 'confirmPassword',
      name: 'Confirm Password',
      rules: 'required|confirm_password:password',
      visible: true,
      option: '',
    }),
    key: XmlLoadColumn({
      id: 'key',
      name: 'key',
      rules: '',
      visible: true,
      option: '',
    }),
  }
  const onMoveToNextStep = async () => {
    if (store.createFlgVerifyPass) {
      if (!(await store.verifyPassword())) return
      stepperStore.moveToNextStep()
      sessionStorage.removeItem("key");
      store.createFlgVerifyPass = false
    }
  }

  const onBackStep = () => {
    if (store.createFlgVerifyPass) {
      window.location.href = document.referrer
    }
  }

  onMounted(() => {
    
    let key = route.query.key ?? sessionStorage.getItem("key") ?? "";
  if (Array.isArray(key)) key = key[0] ?? "";
  
  if (key) {
    sessionStorage.setItem("key", key);
    formContext.setFieldValue("key", key);
  }
  console.log("Key GOT: "+ key);
    store.createFlgVerifyPass = true
    stepperStore.SetValues(steppList)
  })
</script>
