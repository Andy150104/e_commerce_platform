<template>
  <BaseScreenDashBoard>
    <template #body>
      <div class="space-y-6">
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
    </template>
  </BaseScreenDashBoard>
</template>
<script setup lang="ts">
  import DropDownSearch from '@PKG_SRC/components/DropDown/DropDownSearch.vue'
  import UserControlDateField from '@PKG_SRC/components/UserControl/UserControlDateField.vue'
  import UserControlRadioButton from '@PKG_SRC/components/UserControl/UserControlRadioButton.vue'
  import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
  import BaseScreenDashBoard from '@PKG_SRC/layouts/Basecreen/BaseScreenDashBoard.vue'
  import { useRegisterStore } from '@PKG_SRC/stores/Modules/Register/RegisterStore'
  import { useStepperStore } from '@PKG_SRC/stores/Modules/Register/StepperStore'
  import { useUploadImageStore } from '@PKG_SRC/stores/Modules/usercontrol/uploadImageStore'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { useForm } from 'vee-validate'
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
    confirmPassword: XmlLoadColumn({
      id: 'confirmPassword',
      name: 'Confirm Password',
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
</script>
