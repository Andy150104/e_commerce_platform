import type { AbstractApiResponseOfString, UDSSelectUserAddressEntity } from '@PKG_API/@types'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'

export const fieldsInitialize = {
  addressId: '',
  firstName: '',
  lastName: '',
  addressLine: '',
  ward: '',
  city: '',
  province: '',
  district: ''
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = createErrorFields(fieldsInitialize)

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
    async GetAddress() {
      // const validation: any = await this.fields.validate()
      // if (validation.valid === false) return false

      const apiClient = useApiClient()
      const res = await apiClient.api.v1.UDSSelectUserAddress.$post({
        body: {
          isOnlyValidation: false,
        },
      })
      if (res.response){
        this.uUDSSelectUserAddressEntity = res.response 
      }
    },
  
  },
})
