<template>
  <BaseScreenAuth>
    <template #body>
      <div v-if="isVerify">
        <h2 class="text-2xl font-bold text-gray-900 dark:text-white text-center mb-8 pt-4">Register</h2>
        <div class="mb-10 animate-jump-in animate-once animate-ease-linear">
          <ProgressStepper :items="stepperStore.steppList" />
        </div>
        <div v-show="store.createFlgPersonalInfo">
          <div v-show="store.createFlgPersonalInfo" class="space-y-6">
            <h2 class="text-2xl font-semibold">Personal Information</h2>
            <p class="text-sm text-gray-600 dark:text-gray-400">Use a permanent address where you can receive mail.</p>
            <div class="grid grid-cols-1 md:grid-cols-2 gap-6 animate-fade-right">
              <div class="space-y-4">
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
                  <UserControlTextFieldLabel
                    :xml-column="xmlColumns.phoneNumber"
                    :maxlength="50"
                    :disabled="true"
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
                    :date-picker-position="'top left'"
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
                <UserControlUploadImage :max-number-image="1" :is-show-popover="false" :label="'Upload avatar image'" />
              </div>
              <div class="space-y-4">
                <div>
                  <LocationPicker
                    :xml-column-province="xmlColumns.province"
                    :maxlength-province="50"
                    :disabled-province="false"
                    :err-msg-province="fieldErrors.province"
                    :placeholder-province="'Province'"
                    :xml-column-district="xmlColumns.district"
                    :maxlength-district="50"
                    :disabled-district="false"
                    :err-msg-district="fieldErrors.district"
                    :placeholder-district="'District'"
                    :xml-column-ward="xmlColumns.ward"
                    :maxlength-ward="50"
                    :disabled-ward="false"
                    :err-msg-ward="fieldErrors.ward"
                    :placeholder-ward="'Ward'"
                  />
                </div>
              </div>
            </div>
          </div>
          <div :class="store.createFlgPersonalInfo ? 'flex justify-end mt-8' : 'flex justify-between mt-8'">
            <button v-if="!store.createFlgPersonalInfo" :class="className.BUTTON_DEFAULT_GRAY_1" @click="onBackStep">Back</button>
            <button :class="className.BUTTON_DEFAULT_BLUE_2" @click="onMoveToNextStep">Next</button>
          </div>
        </div>
        <div v-show="store.createFlgPlan" class="space-y-6">
          <h2 class="text-2xl font-semibold">Plan Supscription</h2>
          <p class="text-sm text-gray-600 dark:text-gray-400">Use a permanent address where you can receive mail.</p>
          <div class="grid grid-cols-1 animate-fade-right">
            <CardPlan />
          </div>
          <div :class="store.createFlgPersonalInfo ? 'flex justify-end mt-8 pb-6' : 'flex justify-between mt-8 pb-6'">
            <button v-if="!store.createFlgPersonalInfo" :class="className.BUTTON_DEFAULT_GRAY_1" @click="onBackStep">Back</button>
            <button v-if="isNext" :class="className.BUTTON_DEFAULT_BLUE_2" @click="onMoveToConfirmStep">Next</button>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenAuth>
</template>
<script setup lang="ts">
  import CardPlan from '@PKG_SRC/components/Card/CardPlan.vue'
  import ProgressStepper from '@PKG_SRC/components/Stepper/ProgressStepper.vue'
  import LocationPicker from '@PKG_SRC/components/UserControl/LocationPicker.vue'
  import UserControlDateField from '@PKG_SRC/components/UserControl/UserControlDateField.vue'
  import UserControlRadioButton from '@PKG_SRC/components/UserControl/UserControlRadioButton.vue'
  import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
  import UserControlUploadImage from '@PKG_SRC/components/UserControl/UserControlUploadImage.vue'
  import BaseScreenAuth from '@PKG_SRC/layouts/Basecreen/BaseScreenAuth.vue'
  import { useStepperStore } from '@PKG_SRC/stores/Modules/Register/StepperStore'
  import { useVerifyStore } from '@PKG_SRC/stores/Modules/Register/VerifyStore'
  import { useUploadImageStore } from '@PKG_SRC/stores/Modules/usercontrol/uploadImageStore'
  import type { StepItem } from '@PKG_SRC/types'
  import { StepStatus } from '@PKG_SRC/types/enums/constantFrontend'
  import { className } from '@PKG_SRC/utils/class/className'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { storeToRefs } from 'pinia'
  import { useForm } from 'vee-validate'

  const steppList = ref<StepItem[]>([
    {
      nameItem: 'Account Info',
      status: StepStatus.currentStep,
    },
    {
      nameItem: 'Verify Account',
      status: StepStatus.currentStep,
    },
    {
      nameItem: 'Personal Info',
      status: StepStatus.currentStep,
    },
    {
      nameItem: 'Plan Supscription',
      status: StepStatus.pendingStep,
    },
    {
      nameItem: 'Ending',
      status: StepStatus.finishStep,
    },
  ])
  const uploadImageStore = useUploadImageStore()
  const stepperStore = useStepperStore()
  const store = useVerifyStore()
  const isVerify = ref(false)
  const isNext = ref(false)
  const route = useRoute()
  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const xmlColumns = {
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
    gender: XmlLoadColumn({
      id: 'gender',
      name: 'Gender',
      rules: 'required',
      visible: true,
      option: '',
    }),
    province: XmlLoadColumn({
      id: 'province',
      name: 'Provice',
      rules: '',
      visible: true,
      option: '',
    }),
    district: XmlLoadColumn({
      id: 'district',
      name: 'District',
      rules: '',
      visible: true,
      option: '',
    }),
    ward: XmlLoadColumn({
      id: 'ward',
      name: 'Ward',
      rules: '',
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

  const onMoveToConfirmStep = async () => {
    isNext.value = true
    if (store.createFlgPlan) {
      store.RegisterUserClient(String(route.query.key))
      updateFlags(false, false, false, true)
      return
    }
  }

  const onMoveToNextStep = async () => {
    isNext.value = true
    stepperStore.moveToNextStep()
    if (store.createFlgPersonalInfo) return updateFlags(false, false, true, false)
  }

  const onBackStep = () => {
    if (store.createFlgAccountInfo) {
      window.location.href = document.referrer
    }
    stepperStore.moveToPreviousStep()
    if (store.createFlgPersonalInfo) return updateFlags(true, false, false, false)
    if (store.createFlgPlan) return updateFlags(false, true, false, false)
    if (store.createFlgComplete) return updateFlags(false, false, true, false)
  }

  onMounted(async () => {
    if (await store.onVerify(String(route.query.key))) {
      isVerify.value = true
      updateFlags(false, true, false, false)
    }
    stepperStore.SetValues(steppList)
    stepperStore.currentIndex = 2
  })
</script>
