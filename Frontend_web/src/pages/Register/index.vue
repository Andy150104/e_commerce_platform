<template>
  <BaseScreenAuth>
    <template #body>
      <div v-if="store.createFlgAccountInfo">
        <h2 class="text-2xl font-bold text-gray-900 dark:text-white text-center mb-8 pt-4">Register</h2>
        <div class="mb-10 animate-jump-in animate-once animate-ease-linear">
          <ProgressStepper :items="stepperStore.steppList" />
        </div>
        <div :class="'animate-fade-right grid grid-cols-1 gap-8 ' + className.COLS_2">
          <div class="space-y-4">
            <div>
              <LabelItem :xml-column="xmlColumns.userName" />
              <BaseControlTextField
                :xml-column="xmlColumns.userName"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.userName"
                :placeholder="'abcxyz'"
              />
            </div>
            <div>
              <LabelItem :xml-column="xmlColumns.password" />
              <BaseControlTextField
                :xml-column="xmlColumns.password"
                :maxlength="50"
                :disabled="false"
                :type="'password'"
                :err-msg="fieldErrors.password"
                :placeholder="'***********'"
              />
            </div>
            <div>
              <LabelItem :xml-column="xmlColumns.confirmPassword" />
              <BaseControlTextField
                :xml-column="xmlColumns.confirmPassword"
                :maxlength="50"
                :disabled="false"
                :type="'password'"
                :err-msg="fieldErrors.confirmPassword"
                :placeholder="'***********'"
              />
            </div>
          </div>
          <div class="space-y-4">
            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.firstName"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.firstName"
                :placeholder="'John'"
              />
            </div>
            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.email"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.email"
                :placeholder="'abc@gmail.com'"
              />
            </div>
            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.lastName"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.lastName"
                :placeholder="'John'"
              />
            </div>
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
  import BaseControlTextField from '@PKG_SRC/components/Basecontrol/BaseControlTextField.vue'
  import LabelItem from '@PKG_SRC/components/Basecontrol/LabelItem.vue'
  import ProgressStepper from '@PKG_SRC/components/Stepper/ProgressStepper.vue'
  import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
  import BaseScreenAuth from '@PKG_SRC/layouts/Basecreen/BaseScreenAuth.vue'
  import { useRegisterStore } from '@PKG_SRC/stores/Modules/Register/RegisterStore'
  import { useStepperStore } from '@PKG_SRC/stores/Modules/Register/StepperStore'
  import type { StepItem } from '@PKG_SRC/types'
  import { StepStatus } from '@PKG_SRC/types/enums/constantFrontend'
  import { className } from '@PKG_SRC/utils/class/className'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'

  const steppList = ref<StepItem[]>([
    {
      nameItem: 'Account Info',
      status: StepStatus.currentStep,
    },
    {
      nameItem: 'Confirm',
      status: StepStatus.finishStep,
    },
  ])
  const stepperStore = useStepperStore()
  const store = useRegisterStore()
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const xmlColumns = {
    userName: XmlLoadColumn({
      id: 'userName',
      name: 'Username',
      rules: 'required',
      visible: true,
      option: '',
    }),
    password: XmlLoadColumn({
      id: 'password',
      name: 'Password',
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
    email: XmlLoadColumn({
      id: 'email',
      name: 'Email',
      rules: 'required',
      visible: true,
      option: '',
    }),
    firstName: XmlLoadColumn({
      id: 'firstName',
      name: 'First Name',
      rules: 'required',
      visible: true,
      option: '',
    }),
    lastName: XmlLoadColumn({
      id: 'lastName',
      name: 'Last Name',
      rules: 'required',
      visible: true,
      option: '',
    }),
  }

  const onMoveToNextStep = async () => {
    if (store.createFlgAccountInfo) {
      if (!(await store.RegisterUser())) return
      stepperStore.moveToNextStep()
      store.createFlgAccountInfo = false
    }
  }

  const onBackStep = () => {
    if (store.createFlgAccountInfo) {
      window.location.href = document.referrer
    }
  }

  onMounted(() => {
    store.createFlgAccountInfo = true
    stepperStore.SetValues(steppList)
  })
</script>
