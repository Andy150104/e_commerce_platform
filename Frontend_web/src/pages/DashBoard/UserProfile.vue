<template>
  <BaseScreenDashBoard>
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
      <div
        id="gradient-card"
        class="animate-fade-left animate-ease-out animate-delay-100 p-10 md:max-w-none md:p-12 lg:p-30 rounded-xl mb-16 max-w-screen-lg mx-auto h-auto overflow-hidden"
        ref="gradientCard"
      >
        <div class="absolute inset-0 pointer-events-none z-0 gradient-effect" ref="gradientEffect"></div>
        <div class="max-w-3xl mx-auto text-center relative z-10 mt-0 pt-0">
          <h1 class="text-4xl font-bold text-black dark:text-white mb-4 animate-fade-up animate-duration-1000 animate-delay-500">User Profile</h1>
          <p class="text-lg text-gray-700 mb-8 animate-fade-up animate-duration-1000 animate-delay-500">Edit Your Own Personal Informations!</p>
          <div></div>
          <div class="mt-8 flex justify-center space-x-4"></div>
        </div>
        <div class="animate-fade-right grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-8">
          <!-- Column 1: Profile Picture -->
          <div class="space-y-4 xl:col-span-1">
            <h2 class="text-2xl text-black dark:text-white font-semibold">Profile</h2>
            <p class="text-sm text-black dark:text-white">This information will be displayed publicly so be careful what you share.</p>
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
                :src="`https://static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg`"
                alt="User Profile Image"
              />
            </div>
          </div>

          <!-- Column 2: Input Fields -->
          <div class="space-y-4 xl:col-span-2">
            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.email"
                :maxlength="50"
                :disabled="true"
                :err-msg="fieldErrors.email"
                :placeholder="'abc@gmail.com'"
              />
            </div>
            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.firstName"
                :maxlength="50"
                :disabled="!isActive"
                :err-msg="fieldErrors.firstName"
                :placeholder="'John'"
              />
            </div>

            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.lastName"
                :maxlength="50"
                :disabled="!isActive"
                :err-msg="fieldErrors.lastName"
                :placeholder="'John'"
              />
            </div>
            <div>
              <UserControlDateField
                :xml-column="xmlColumns.birthDate"
                :err-msg="fieldErrors.birthDate"
                :disabled="!isActive"
                :maxlength="50"
                :date-model="'11/1/2002'"
                :is-inline="false"
                :date-picker-position="'bottom right'"
              />
            </div>
            <div>
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.phoneNumber"
                :maxlength="50"
                :disabled="!isActive"
                :err-msg="fieldErrors.phoneNumber"
                :placeholder="'+84XXX'"
              />
            </div>
            <div>
              <UserControlRadioButton
                :xml-column="xmlColumns.gender"
                :model="fieldValues.gender"
                :disabled="!isActive"
                :err-msg="fieldErrors.gender"
                :master-name="'Gender'"
              />
            </div>
            <div>
              <UserControlUploadImage :max-number-image="1" :is-show-popover="false" :label="'Upload avatar image'" />
            </div>
            <div class="flex space-x-4">
              <button v-if="isActive" @click="isActive = false" class="w-full bg-gray-500 text-white p-3 rounded-lg hover:bg-gray-600">Back</button>
              <button v-if="isActive" @click="Update" class="w-full bg-green-500 text-white p-3 rounded-lg hover:bg-green-600">Save Changes</button>
              <button v-if="!isActive" @click="isActive = true" class="w-full bg-blue-500 text-white p-3 rounded-lg hover:bg-blue-600">
                Update Account
              </button>
            </div>
          </div>
        </div>
      </div>
    </template>
  </BaseScreenDashBoard>
</template>
<script lang="ts" setup>
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
  import UserControlUploadImage from '@PKG_SRC/components/UserControl/UserControlUploadImage.vue'
  import { useUploadImageStore } from '@PKG_SRC/stores/Modules/usercontrol/uploadImageStore'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'
  import { ref } from 'vue'
  import { useProfileStore } from '@PKG_SRC/stores/Modules/DashBoard/profileStore'
  import UserControlDateField from '@PKG_SRC/components/UserControl/UserControlDateField.vue'
  import { storeToRefs } from 'pinia'
  import UserControlRadioButton from '@PKG_SRC/components/UserControl/UserControlRadioButton.vue'
  const uploadImageStore = useUploadImageStore()
  const store = useProfileStore()
  const isActive = ref(false)
  const successMessage = ref(false)
  const { fieldValues, fieldErrors } = storeToRefs(store)
  // const { fieldValuesU, fieldErrors } = storeToRefs(storeU)
  const formContext = useForm({ initialValues: fieldValues.value })
  // const formContextU = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  // store.SetFields(formContextU)

  const xmlColumns = {
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
    birthDate: XmlLoadColumn({
      id: 'birthDate',
      name: 'birth Date',
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
  }
  const selectUserProfile = async () => {
    store.GetProfile()
  }

  const Update = async () => {
    store.UpdateUser()
    isActive.value = false
    successMessage.value = true
    setTimeout(() => {
      successMessage.value = false
    }, 3000)
  }

  onMounted(async () => {
    await store.GetProfile()
  })
</script>
