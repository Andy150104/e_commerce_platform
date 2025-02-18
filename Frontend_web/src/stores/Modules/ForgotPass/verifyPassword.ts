import type { AbstractApiResponseOfString, FPSVerifyTokenRequest } from '@PKG_API/@types'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useApiServer } from '@PKG_SRC/utils/auth/authHttp'

export const fieldsInitialize = {
  password: '',
  otp : '',
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = createErrorFields(fieldsInitialize)

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type VerifyPassState = {
  fields: typeof fields
  createFlgVerifyPass: boolean
  createFlgPersonalInfo: boolean
  createFlgPlan: boolean
  createFlgComplete: boolean
  uFPSVerifyTokenRequest: FPSVerifyTokenRequest
}

export const useVerifyPasswordStore = defineStore('VerifyPass', {
  state: (): VerifyPassState => ({
    fields,
    createFlgVerifyPass: false,
    createFlgPlan: false,
    createFlgPersonalInfo: false,
    createFlgComplete: false,
    uFPSVerifyTokenRequest: {} as FPSVerifyTokenRequest,
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
    async verifyPassword() {
      // const validation: any = await this.fields.validate()
      // if (validation.valid === false) return false
      const validation: any = await this.fields.validate()
      if (validation.valid === false) return false
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const apiClient = useApiServer()
      const formMessage = useFormMessageStore()
      const loadingStore = useLoadingStore()
      const res = await apiClient.api.v1.FPSVerifyToken.$post({
        body: {
          isOnlyValidation: false,
          otp: apiFieldValues.otp,
          newPassWord: apiFieldValues.password,
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
  },
})
