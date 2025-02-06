<template>
  <BaseScreen>
    <template #body>
      <div class="min-h-screen">
        <FormInput>
          <template #body>
            <FormColumns :cols="className.COLS_4">
              <UserControlTextFieldFloatLabel
                :xml-column="xmlColumns.mailAddress"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.mailAddress"
              />
              <UserControlTextFieldFloatLabel
                :xml-column="xmlColumns.mailAddressConfirm"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.mailAddressConfirm"
              />
              <UserControlTextFieldFloatLabel
                :xml-column="xmlColumns.mailAddressConfirm"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.mailAddressConfirm"
              />
              <UserControlTextFieldFloatLabel
                :xml-column="xmlColumns.mailAddressConfirm"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.mailAddressConfirm"
              />
              <UserControlTextFieldLabel
                :xml-column="xmlColumns.mailAddressConfirm"
                :maxlength="50"
                :disabled="false"
                :err-msg="fieldErrors.mailAddressConfirm"
              />
              <UserControlDateField
                :xml-column="xmlColumns.seinengappi"
                :err-msg="fieldErrors.seinengappi"
                :disabled="false"
                :maxlength="10"
                :date-model="'11/1/2002'"
              />
            </FormColumns>
          </template>
        </FormInput>
        <div>
          <div class="flex space-x-3">
            <UIcon name="i-heroicons-desktop-computer" class="w-5 h-5 text-primary-500" />
            <UIcon name="i-heroicons-sun" class="w-5 h-5 text-primary-500" />
            <UIcon name="i-heroicons-moon" class="w-5 h-5 text-primary-500" />
            <UIcon name="i-heroicons-adjustments" class="w-5 h-5 text-primary-500" />
          </div>
        </div>
        <button
          class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
          @click="onSend"
        >
          Test
        </button>
        <BModal />
      </div>
    </template>
  </BaseScreen>
</template>
<script setup lang="ts">
  import UserControlTextFieldFloatLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldFloatLabel.vue'
  import FormColumns from '@PKG_SRC/components/Form/FormColumns.vue'
  import { useMyppStore } from '@PKG_SRC/stores/Modules/Mypp/Mypp'
  import { className } from '@PKG_SRC/utils/class/className'
  import { XmlLoadColumn } from '@PKG_SRC/utils/xml'
  import { storeToRefs } from 'pinia'
  import { useForm } from 'vee-validate'
  import UserControlTextFieldLabel from '@PKG_SRC/components/UserControl/UserControlTextFieldLabel.vue'
  import BModal from '@PKG_SRC/components/Modal/BModal.vue'
  import UserControlDateField from '@PKG_SRC/components/UserControl/UserControlDateField.vue'
  import FormInput from '@PKG_SRC/components/Form/FormInput.vue'
  import BaseScreen from '@PKG_SRC/layouts/Basecreen/BaseScreen.vue'
  const store = useMyppStore()

  const { fieldValues, fieldErrors } = storeToRefs(store)
  const formContext = useForm({ initialValues: fieldValues.value })
  store.SetFields(formContext)
  const xmlColumns = {
    mailAddress: XmlLoadColumn({
      id: 'mailAddress',
      name: 'Email',
      rules: 'required',
      visible: true,
      option: '',
    }),
    mailAddressConfirm: XmlLoadColumn({
      id: 'mailAddressConfirm',
      name: 'Email (confirm)',
      rules: 'required',
      visible: true,
      option: '',
    }),
    seinengappi: XmlLoadColumn({
      id: 'seinengappi',
      name: 'Birthday',
      rules: '',
      visible: true,
      option: '',
    }),
  }
  const onSend = () => {
    store.Test()
  }
</script>
<style setup></style>
