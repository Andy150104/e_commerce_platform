<template>
  <BaseScreenAuth>
    <template #body>
      <h2 class="text-2xl font-bold text-gray-900 dark:text-white text-center mb-8 pt-4">Register</h2>
      <div class="mb-10 animate-jump-in animate-once animate-ease-linear">
        <ProgressStepper :items="stepperStore.steppList" />
      </div>
      <div v-show="store.createFlgInput" :class="'animate-fade-right grid grid-cols-1 gap-8 ' + className.COLS_2">
        <div class="space-y-4">
          <h2 class="text-2xl font-semibold">Profile</h2>
          <p class="text-sm text-gray-600 dark:text-gray-400">This information will be displayed publicly so be careful what you share.</p>
          <div class="h-full w-full">
            <img
              v-if="uploadImageStore.uploadImage.length > 0"
              class="rounded-full object-cover mx-auto aspect-square w-40 h-40 md:w-64 md:h-64"
              :src="uploadImageStore.uploadImage[0].imagePreview"
              alt="image description"
            />
            <img
              v-else
              class="rounded-full object-cover mx-auto aspect-square w-40 h-40 md:w-64 md:h-64"
              src="https://static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg"
              alt="image description"
            />
          </div>
        </div>
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
            <UserControlUploadImage :max-number-image="1" :is-show-popover="false" :label="'Upload avatar image'" />
          </div>
        </div>
      </div>
      <div v-show="store.createFlgNextInputInfo" class="space-y-6">
        <h2 class="text-2xl font-semibold">Personal Information</h2>
        <p class="text-sm text-gray-600 dark:text-gray-400">Use a permanent address where you can receive mail.</p>
        <div class="grid grid-cols-1 md:grid-cols-2 gap-6 animate-fade-right">
          <div class="space-y-4">
            <div>
              <label for="first-name" class="block text-sm font-medium">First name</label>
              <input type="text" id="first-name" class="mt-1 block w-full px-4 py-2 rounded-md border border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white focus:ring-2 focus:ring-blue-500">
            </div>
            <div>
              <label for="email-address" class="block text-sm font-medium">Email address</label>
              <input type="email" id="email-address" class="mt-1 block w-full px-4 py-2 rounded-md border border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white focus:ring-2 focus:ring-blue-500">
            </div>
            <div>
              <label for="country" class="block text-sm font-medium">Country</label>
              <select id="country" class="mt-1 block w-full px-4 py-2 rounded-md border border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white focus:ring-2 focus:ring-blue-500">
                <option>United States</option>
                <option>Canada</option>
                <option>United Kingdom</option>
              </select>
            </div>
            <div>
              <label for="street-address" class="block text-sm font-medium">Street address</label>
              <input type="text" id="street-address" class="mt-1 block w-full px-4 py-2 rounded-md border border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white focus:ring-2 focus:ring-blue-500">
            </div>
          </div>
          <div class="space-y-4">
            <div>
              <label for="last-name" class="block text-sm font-medium">Last name</label>
              <input type="text" id="last-name" class="mt-1 block w-full px-4 py-2 rounded-md border border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white focus:ring-2 focus:ring-blue-500">
            </div>
            <div>
              <label for="city" class="block text-sm font-medium">City</label>
              <input type="text" id="city" class="mt-1 block w-full px-4 py-2 rounded-md border border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white focus:ring-2 focus:ring-blue-500">
            </div>
            <div>
              <label for="state-province" class="block text-sm font-medium">State / Province</label>
              <input type="text" id="state-province" class="mt-1 block w-full px-4 py-2 rounded-md border border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white focus:ring-2 focus:ring-blue-500">
            </div>
            <div>
              <label for="zip-postal-code" class="block text-sm font-medium">ZIP / Postal code</label>
              <input type="text" id="zip-postal-code" class="mt-1 block w-full px-4 py-2 rounded-md border border-gray-300 dark:border-gray-700 dark:bg-gray-800 dark:text-white focus:ring-2 focus:ring-blue-500">
            </div>
          </div>
        </div>
      </div>
      <div class="flex justify-between mt-8">
        <button :class="className.BUTTON_DEFAULT_GRAY_1" @click="onBackStep">Back</button>
        <button :class="className.BUTTON_DEFAULT_BLUE_2" @click="moveToNextStep">Next</button>
      </div>
    </template>
  </BaseScreenAuth>
</template>
<script setup lang="ts">
  import BaseControlTextField from '@PKG_SRC/components/Basecontrol/BaseControlTextField.vue'
  import LabelItem from '@PKG_SRC/components/Basecontrol/LabelItem.vue'
  import ProgressStepper from '@PKG_SRC/components/Stepper/ProgressStepper.vue'
  import UserControlUploadImage from '@PKG_SRC/components/UserControl/UserControlUploadImage.vue'
  import BaseScreenAuth from '@PKG_SRC/layouts/Basecreen/BaseScreenAuth.vue'
  import { useRegisterStore } from '@PKG_SRC/stores/Modules/Register/RegisterStore'
  import { useStepperStore } from '@PKG_SRC/stores/Modules/Register/StepperStore'
  import { useUploadImageStore } from '@PKG_SRC/stores/Modules/usercontrol/uploadImageStore'
  import type { StepItem } from '@PKG_SRC/types'
import { StepStatus } from '@PKG_SRC/types/enums/constantFrontend'
  import { className } from '@PKG_SRC/utils/class/className'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'

  const steppList = ref<StepItem[]>([
    {
      nameItem: 'Personal Info',
      status: 0,
    },
    {
      nameItem: 'Plan Scription',
      status: 1,
    },
    {
      nameItem: 'Confirm',
      status: 2,
    },
  ])
  const uploadImageStore = useUploadImageStore()
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
  }
  const updateFlags = (isInput: boolean, isInputInfor:boolean, isPlan: boolean, isComplete: boolean) => {
    store.createFlgInput = isInput
    store.createFlgNextInputInfo = isInputInfor
    store.createFlgPlan = isPlan
    store.createFlgComplete = isComplete
  }

  const moveToNextStep = () => {
    stepperStore.updateStepStatus(1, StepStatus.currentStep)
    updateFlags(false, true, false, false)
  }

  const onBackStep = () => {
    stepperStore.updateStepStatus(0, StepStatus.currentStep)
    stepperStore.updateStepStatus(1, StepStatus.pendingStep)
    updateFlags(true, false, false, false)
  }

  onMounted(() => {
    updateFlags(true, false, false, false)
    stepperStore.SetValues(steppList)
  })
</script>
