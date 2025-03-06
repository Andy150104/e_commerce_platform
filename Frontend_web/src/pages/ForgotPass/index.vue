<template>
  <BaseScreenAuth>
    <template #body>
      <div
        v-if="successMessage"
        class="fixed top-5 right-5 z-50 w-96 transition-opacity duration-1000 ease-out"
        :class="{ 'opacity-0': !successMessage }"
      >
        <div class="flex items-center p-4 text-sm text-green-800 rounded-lg bg-green-50 shadow-lg" role="alert">
          <svg class="flex-shrink-0 w-4 h-4 mr-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
            <path d="M18 10A8 8 0 1 1 2 10a8 8 0 0 1 16 0ZM9 13h2v2H9v-2Zm0-6h2v4H9V7Z" />
          </svg>
          <span class="font-medium">Success!</span> Profile updated successfully.
        </div>
      </div>
      <ProgressStepper :items="stepperStore.steppList" class="pt-14" />
      <div v-if="store.createFlgUpdatePass">
        <h2 class="text-2xl font-bold text-gray-900 dark:text-white text-center mb-8 pt-4">Forgot Password</h2>
        <div class="mb-10 animate-jump-in animate-once animate-ease-linear"></div>
        <div class="py-20 mx-14 text-center">
          <div class="space-y-4">
            <h2 class="text-2xl font-semibold">Account Confirm</h2>
            <p class="text-sm text-gray-600 dark:text-gray-400">Please Enter your recovery email.</p>
          </div>
          <div>
            <LabelItem :xml-column="xmlColumns.email" />
            <BaseControlTextField
              :xml-column="xmlColumns.email"
              :maxlength="50"
              :disabled="false"
              :err-msg="fieldErrors.email"
              :placeholder="'abc@gmail.com'"
            />
          </div>
        </div>
        <div class="flex justify-between mt-8">
          <button :class="className.BUTTON_DEFAULT_GRAY_1" @click="onBackStep">Back</button>
          <button :class="className.BUTTON_DEFAULT_BLUE_2" @click="onMoveToNextStep">Next</button>
        </div>
      </div>
      <div v-else>
        <section class="bg-white dark:bg-gray-900">
          <div class="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 grid lg:grid-cols-2 gap-8 lg:gap-16">
            <div class="flex flex-col justify-center">
              <h1 class="mb-4 text-4xl font-extrabold tracking-tight leading-none text-gray-900 md:text-5xl lg:text-6xl dark:text-white">
                Email has been sended!!!!
              </h1>
              <p class="mb-8 text-lg font-normal text-gray-500 lg:text-xl dark:text-gray-400">Please check your email</p>
              <div class="flex flex-col space-y-4 sm:flex-row sm:space-y-0">
                <a
                  href="https://mail.google.com/"
                  class="inline-flex justify-center items-center py-3 px-5 text-base font-medium text-center text-white rounded-lg bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 dark:focus:ring-blue-900"
                >
                  Open Email Now
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
  import { useUpdatePassStore } from '@PKG_SRC/stores/Modules/ForgotPass/forgotStore'
  import { useForm } from 'vee-validate'

  const successMessage = ref(false)
  const stepperStore = useStepperStore()
  const store = useUpdatePassStore()
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)

  const steppList = ref<StepItem[]>([
    {
      nameItem: 'Confirm Account',
      status: StepStatus.currentStep,
    },
    {
      nameItem: 'Reset Code',
      status: StepStatus.finishStep,
    },
  ])
  const xmlColumns = {
    email: XmlLoadColumn({
      id: 'email',
      name: 'email',
      rules: '',
      visible: true,
      option: '',
    }),
  }
  const onMoveToNextStep = async () => {
    if (store.createFlgUpdatePass) {
      if (!(await store.updatePassword())) return
      stepperStore.moveToNextStep()
      store.createFlgUpdatePass = false
    }
  }

  const onBackStep = () => {
    if (store.createFlgUpdatePass) {
      window.location.href = document.referrer
    }
  }
  const UpdatePass = async () => {
    if (!(await store.updatePassword())) return
    successMessage.value = true
    setTimeout(() => {
      successMessage.value = false
    }, 3000)
  }

  onMounted(() => {
    store.createFlgUpdatePass = true
    stepperStore.SetValues(steppList)
  })
</script>
