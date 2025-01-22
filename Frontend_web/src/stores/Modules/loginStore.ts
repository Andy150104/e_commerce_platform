import { defineStore } from 'pinia'

const fieldsInitialize = { userId: '', password: '' }
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = { userId: '', password: '' }

export const useLoginStore = defineStore('login', {
  state: () => ({
    fields: {
      values: fieldsInitialize,
      errors: errorFieldsInitialize,
      ...veeValidateStateInitialize,
    },
  }),
  getters: {
    fieldValues: (state) => {
      return state.fields.values
    },
    fieldValid: (state) => {
      return state.fields.meta.valid
    },
    fieldErrors: (state) => {
      return state.fields.errors
    },
  },
  actions: {
    ResetStore() {
      this.fields.resetForm()
    },
    SetFields(value: any) {
      this.fields = value
    },
    async Validate() {
      await this.fields.validate()
      return this.fieldValid
    },
  },
})
