import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'

export const fieldsInitialize = {
  dantaiBangou: '',
  ryokouBangou: '',
  ninsyoKey: '',
  mailAddress: '',
  mailAddressConfirm: '',
  seinengappi: '',
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = fieldsInitialize

// VeeValidate
const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  // vee-validate用の初期化メソッド
  ...veeValidateStateInitialize,
}

export type Mypg020MailState = {
  fields: typeof fields
}

export const useMyppStore = defineStore('useMyppStore', {
  state: (): Mypg020MailState => ({
    fields,
  }),
  // state

  getters: {
    fieldValues: (state) => {
      return state.fields.values
    },
    fieldErrors: (state) => {
      return state.fields.errors
    },
  },
  actions: {
    SetFields(value: any) {
      this.fields = value
    },
    ResetStore() {
      this.fields.resetForm()
    },
    Test(){
      console.log('test seinengappi',this.fieldValues.seinengappi)
      console.log('test mailAddress',this.fieldValues.mailAddress)
    }
  },
})
