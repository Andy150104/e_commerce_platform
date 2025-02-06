import { defineStore } from 'pinia'

const fieldsInitialize = { userName: '', password: '' }
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = { userName: '', password: '' }

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type RegisterState = {
  fields: typeof fields
} & {
  createFlgInput: boolean
  createFlgNextInputInfo: boolean,
  createFlgPlan: boolean
  createFlgComplete: boolean
}

export const useRegisterStore = defineStore('Register', {
  state: (): RegisterState => ({
    fields: {
      values: fieldsInitialize,
      errors: errorFieldsInitialize,
      ...veeValidateStateInitialize,
    },
    createFlgInput: false,
    createFlgPlan: false,
    createFlgNextInputInfo: false,
    createFlgComplete: false,
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
