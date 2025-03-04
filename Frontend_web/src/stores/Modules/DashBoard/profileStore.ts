import type { AbstractApiResponseOfString, UDSSelectUserProfileEntity } from '@PKG_API/@types'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'

export const fieldsInitialize = {
  userName: '',
  imageUrl: '',
  password: '',
  confirmPassword: '',
  email: '',
  phoneNumber: '',
  birthDate: '',
  firstName: '',
  lastName: '',
  gender: 1,
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = createErrorFields(fieldsInitialize)

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type ProfileState = {
  fields: typeof fields
  uDSSelectUserProfileEntity: UDSSelectUserProfileEntity
}

export const useProfileStore = defineStore('Profile', {
  state: (): ProfileState => ({
    fields,
    uDSSelectUserProfileEntity: {} as UDSSelectUserProfileEntity,
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
    async GetProfile() {
      // const validation: any = await this.fields.validate()
      // if (validation.valid === false) return false
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const apiClient = useApiClient()
      const res = await apiClient.api.v1.UDSSelectUserProfile.$post({
        body: {
          isOnlyValidation: false,
        },
      })
      this.uDSSelectUserProfileEntity = res.response
      this.fields.setFieldValue('email', this.uDSSelectUserProfileEntity.email)
      this.fields.setFieldValue('imageUrl', this.uDSSelectUserProfileEntity.imageUrl)
      this.fields.setFieldValue('phoneNumber', this.uDSSelectUserProfileEntity.phoneNumber)
      this.fields.setFieldValue('birthDate', this.uDSSelectUserProfileEntity.birthDate)
      this.fields.setFieldValue('gender', this.uDSSelectUserProfileEntity.gender?.toString())
      this.fields.setFieldValue('firstName', this.uDSSelectUserProfileEntity.firstName)
      this.fields.setFieldValue('lastName', this.uDSSelectUserProfileEntity.lastName)
      loadingStore.LoadingChange(false)
    },
    async UpdateUser() {
      const validation: any = await this.fields.validate()
      if (validation.valid === false) return false
      const apiServer = useApiClient()
      const formMessage = useFormMessageStore()
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiServer.api.v1.UDSUpdateUserProfile.$post({
        body: {
          isOnlyValidation: false,
          firstName: apiFieldValues.firstName,
          lastName: apiFieldValues.lastName,
          phoneNumber: apiFieldValues.phoneNumber,
          birthDay:
            apiFieldValues.birthDate && /^\d{2}-\d{2}-\d{4}$/.test(apiFieldValues.birthDate)
              ? apiFieldValues.birthDate.split('-').reverse().join('-')
              : apiFieldValues.birthDate || '',
          imageUrl: 'https://static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jp',
          gender: apiFieldValues.gender,
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
  persist: true,
})
