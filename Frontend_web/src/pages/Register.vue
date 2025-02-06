<template>
  <BaseScreenAuth>
    <template #body>
      <h2 class="text-2xl font-bold text-gray-900 dark:text-white text-center mb-8 pt-4">Register</h2>
      <div class="mb-10 animate-jump-in animate-once animate-ease-linear">
        <ProgressStepper :items="stepperStore.steppList" />
      </div>
      <div v-show="store.createFlgAccountInfo" :class="'animate-fade-right grid grid-cols-1 gap-8 ' + className.COLS_2">
        <div class="space-y-4">
          <h2 class="text-2xl font-semibold">Profile</h2>
          <p class="text-sm text-gray-600 dark:text-gray-400">This information will be displayed publicly so be careful what you share.</p>
          <div class="w-full">
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
      <div v-show="store.createFlgPersonalInfo" class="space-y-6">
        <h2 class="text-2xl font-semibold">Personal Information</h2>
        <p class="text-sm text-gray-600 dark:text-gray-400">Use a permanent address where you can receive mail.</p>
        <div class="grid grid-cols-1 md:grid-cols-2 gap-6 animate-fade-right">
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
                :xml-column="xmlColumns.phoneNumber"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.phoneNumber"
                :placeholder="'123456789'"
              />
            </div>
            <div>
              <UserControlDateField
                :xml-column="xmlColumns.birthday"
                :err-msg="fieldErrors.birthday"
                :disabled="false"
                :maxlength="10"
                :date-model="'11/1/2002'"
                :is-inline="false"
                :date-picker-position="'top right'"
              />
              <UserControlRadioButton
                :xml-column="xmlColumns.gender"
                :model="fieldValues.gender"
                :disabled="false"
                :err-msg="fieldErrors.gender"
                :master-name="'Gender'"
              />
            </div>
          </div>
          <div class="space-y-4">
            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.lastName"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.lastName"
                :placeholder="'John'"
              />
            </div>
            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.country"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.country"
                :placeholder="'Viet Nam'"
              />
            </div>
            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.city"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.city"
                :placeholder="'Ho Chi Minh'"
              />
            </div>
            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.ward"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.ward"
                :placeholder="'District 0'"
              />
            </div>
          </div>
        </div>
      </div>
      <div v-show="store.createFlgPlan" class="space-y-6">
        <h2 class="text-2xl font-semibold">Plan Supscription</h2>
        <p class="text-sm text-gray-600 dark:text-gray-400">Use a permanent address where you can receive mail.</p>
        <div class="grid grid-cols-1 animate-fade-right">
          <CardPlan/>
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
  import CardPlan from '@PKG_SRC/components/Card/CardPlan.vue'
  import ProgressStepper from '@PKG_SRC/components/Stepper/ProgressStepper.vue'
  import UserControlDateField from '@PKG_SRC/components/UserControl/UserControlDateField.vue'
  import UserControlRadioButton from '@PKG_SRC/components/UserControl/UserControlRadioButton.vue'
  import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
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
      nameItem: 'Account Info',
      status: StepStatus.currentStep,
    },
    {
      nameItem: 'Personal Info',
      status: StepStatus.pendingStep,
    },
    {
      nameItem: 'Plan Supscription',
      status: StepStatus.pendingStep,
    },
    {
      nameItem: 'Confirm',
      status: StepStatus.finishStep,
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
    email: XmlLoadColumn({
      id: 'email',
      name: 'Email',
      rules: 'required',
      visible: true,
      option: '',
    }),
    phoneNumber: XmlLoadColumn({
      id: 'phoneNumber',
      name: 'Phone Number',
      rules: 'required',
      visible: true,
      option: '',
    }),
    birthday: XmlLoadColumn({
      id: 'birthday',
      name: 'Birthday',
      rules: '',
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
    gender: XmlLoadColumn({
      id: 'gender',
      name: 'Gender',
      rules: 'required',
      visible: true,
      option: '',
    }),
    city: XmlLoadColumn({
      id: 'city',
      name: 'City',
      rules: 'required',
      visible: true,
      option: '',
    }),
    country: XmlLoadColumn({
      id: 'country',
      name: 'Country',
      rules: 'required',
      visible: true,
      option: '',
    }),
    ward: XmlLoadColumn({
      id: 'ward',
      name: 'Ward',
      rules: 'required',
      visible: true,
      option: '',
    }),
  }
  const updateFlags = (isAccount: boolean, isPersonal: boolean, isPlan: boolean, isComplete: boolean) => {
    store.createFlgAccountInfo = isAccount
    store.createFlgPersonalInfo = isPersonal
    store.createFlgPlan = isPlan
    store.createFlgComplete = isComplete
  }

  const moveToNextStep = () => {
    stepperStore.moveToNextStep()
    if (store.createFlgAccountInfo) return updateFlags(false, true, false, false)
    if (store.createFlgPersonalInfo) return updateFlags(false, false, true, false)
    if (store.createFlgPlan) return updateFlags(false, false, false, true)
  }

  const onBackStep = () => {
    if (store.createFlgAccountInfo){
      window.location.href = document.referrer
    }
    stepperStore.moveToPreviousStep()
    if (store.createFlgPersonalInfo) return updateFlags(true, false, false, false)
    if (store.createFlgPlan) return updateFlags(false, true, false, false)
    if (store.createFlgComplete) return updateFlags(false, false, true, false)
  }

  onMounted(() => {
    updateFlags(true, false, false, false)
    stepperStore.SetValues(steppList)
  })
</script>
