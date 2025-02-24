import type { AbstractApiResponseOfString } from '@PKG_API/@types'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { AuthAPI } from '@PKG_SRC/utils/auth/authClient'
import { useApiServer, useLogoutClient } from '@PKG_SRC/utils/auth/authHttp'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'

export const fieldsInitialize = {
  userName: '',
  password: '',
  confirmPassword: '',
  email: '',
  phoneNumber: '',
  birthday: '',
  firstName: '',
  lastName: '',
  gender: '1',
  province: '',
  district: '',
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
      const apiServer = useApiServer()
      const formMessage = useFormMessageStore()
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiServer.api.v1.UserInsert.$post({
        body: {
          isOnlyValidation: false,
          username: apiFieldValues.userName,
          email: apiFieldValues.email,
          password: apiFieldValues.password,
          firstName: apiFieldValues.firstName,
          lastName: apiFieldValues.lastName,
        },
      })
      loadingStore.LoadingChange(false)
      if (!res.success) {
        formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
        return false
      }
      formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
      return true
    },
    async onVerify(key: string) {
      const apiServer = useApiServer()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiServer.api.v1.UserVerifyKey.$post({
        body: {
          isOnlyValidation: true,
          key: key,
        },
      })
      loadingStore.LoadingChange(false)
      if (!res.success) return false
      return true
    },
  },
})
