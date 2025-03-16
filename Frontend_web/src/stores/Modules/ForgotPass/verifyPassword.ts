import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useApiServer } from '@PKG_SRC/composables/auth/authHttp'
import type { AbstractApiResponseOfString } from '@PKG_SRC/composables/auth/define/@types'

export const fieldsInitialize = {
  password: '',
  confirmPassword:'',
  key : '',
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
}

export const useVerifyPasswordStore = defineStore('VerifyPass', {
  state: (): VerifyPassState => ({
    fields,
    createFlgVerifyPass: false,
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
      
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const validation: any = await this.fields.validate()
      if (validation.valid === false) return false
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const apiClient = useApiServer()
      const formMessage = useFormMessageStore()
      const res = await apiClient.api.v1.FPSVerifyKey.$post({
        body: {
          isOnlyValidation: false,
          key: apiFieldValues.key,
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
