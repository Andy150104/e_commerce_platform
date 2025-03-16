import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { AbstractApiResponseOfString, UDSSelectUserAddressEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
export type FormSchema = Record<string, string>

export const fieldsInitialize: FormSchema = {
  addressId: '',
  addressLine: '1',
  ward: '',
  city: '',
  province: '',
  district: '',
}

const errorFieldsInitialize: FormSchema = { ...fieldsInitialize }

// VeeValidate fields
const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type AddressState = {
  fields: typeof fields
  uUDSSelectUserAddressEntity: UDSSelectUserAddressEntity[]
}

export const useDashAddressStore = defineStore('Address', {
  state: (): AddressState => ({
    fields,
    uUDSSelectUserAddressEntity: [],
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
    async GetAddress() {
      const apiClient = useApiClient()

      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.UDSSelectUserAddress.$get({})
      loadingStore.LoadingChange(false)
      if (res.response) {
        this.uUDSSelectUserAddressEntity = res.response
      }
    },
    async UpdateAddress() {
      const apiServer = useApiClient()
      const formMessage = useFormMessageStore()
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiServer.api.v1.UDSUpdateUserAddress.$put({
        body: {
          addressId: apiFieldValues.addressId,
          addressLine: apiFieldValues.addressLine,
          ward: apiFieldValues.ward,
          city: apiFieldValues.province,
          province: apiFieldValues.province,
          district: apiFieldValues.district,
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

    async InsertAddress() {
      const apiServer = useApiClient()
      const formMessage = useFormMessageStore()
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)

      const res = await apiServer.api.v1.UDSInsertUserAddress.$post({
        body: {
          addressLine: apiFieldValues.addressLine,
          ward: apiFieldValues.ward,
          city: apiFieldValues.province,
          province: apiFieldValues.province,
          district: apiFieldValues.district,
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
    async RemoveAddress(addressId: string){
      console.log('a')
      const apiServer = useApiClient()
      const formMessage = useFormMessageStore()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiServer.api.v1.UDSDeleteUserAddress.$patch({
        body: {
          addressId: addressId,
        },
      })
      loadingStore.LoadingChange(false)
      if (!res.success) {
        formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
        return false
      }
      formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
      return true
    }
  },
})
