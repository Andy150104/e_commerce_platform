import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { AuthAPI } from '@PKG_SRC/composables/auth/authClient'
import { useApiServer, useLogoutClient } from '@PKG_SRC/composables/auth/authHttp'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useUploadImageStore } from '../usercontrol/uploadImageStore'
import { DateFormat } from '@PKG_SRC/types/enums/constantFrontend'
import type { AbstractApiResponseOfString } from '@PKG_SRC/composables/Client/api/@types'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'

export const fieldsInitialize = {
  phoneNumber: '',
  birthday: '',
  gender: '1',
  province: '',
  district: '',
  ward: '',
  addressLine: '',
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

export const useVerifyStore = defineStore('Verify', {
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
    async onVerify(key: string) {
      const apiServer = useApiServer()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiServer.api.v1.UserVerifyKey.$post({
        body: {
          isOnlyValidation: false,
          key: key,
        },
      })
      loadingStore.LoadingChange(false)
      if (!res.success) return false
      return true
    },
    async RegisterUserClient(key: string) {
      const apiClient = useApiClient()
      const formMessage = useFormMessageStore()
      const useImageStore = useUploadImageStore()
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.URSUserRegister.$post({
        body: {
          key: key,
          addressLine: apiFieldValues.addressLine,
          birthDay: convertDateFormat(apiFieldValues.birthday, DateFormat.YYYYMMDD),
          city: apiFieldValues.district,
          district: apiFieldValues.district,
          gender: Number(apiFieldValues.gender),
          imageUrl:
            'https://plus.unsplash.com/premium_photo-1664474619075-644dd191935f?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8aW1hZ2V8ZW58MHx8MHx8fDA%3D',
          phoneNumber: formatPhoneNumber(apiFieldValues.phoneNumber)?.toString(),
          province: apiFieldValues.province,
          ward: apiFieldValues.ward,
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
    async activeAccount(key: string) {
      const apiServer = useApiServer()
      const formMessage = useFormMessageStore()
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiServer.api.v1.UserInsertVerify.$post({
        body: {
          isOnlyValidation: false,
          key: key,
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
