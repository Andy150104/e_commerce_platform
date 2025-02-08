import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'

export const fieldsInitialize = {
  userName: '',
  password: '',
  email: '',
  phoneNumber: '',
  birthday: '',
  firstName: '',
  lastName: '',
  gender: '1',
  city: '',
  country: '',
  ward: '',
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = createErrorFields(fieldsInitialize)

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type RegisterState = {
  fields: typeof fields
} & {
  createFlgAccountInfo: boolean
  createFlgPersonalInfo: boolean
  createFlgPlan: boolean
  createFlgComplete: boolean
}

export const useRegisterStore = defineStore('Register', {
  state: (): RegisterState => ({
    fields,
    createFlgAccountInfo: false,
    createFlgPlan: false,
    createFlgPersonalInfo: false,
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
    async RegisterUser() {
      const validation: any = await this.fields.validate()
      if (validation.valid === false) return false

      const apiClient = useApiClient()
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)

      // const res = apiClient.api.v1.URSUserRegister.$post({
      //   body: {

      //   },
      // })
      // res.
    },
  },
})
