
import type {AbstractApiResponseOfString, UDSSelectUserAddressEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'

export const fieldsInitialize = {
  addressId: '',
  firstName: '',
  lastName: '',
  addressLine: '1',
  ward: '',
  city: '',
  province: '',
  district: ''
};

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
      const apiClient = useApiClient()
      
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.UDSSelectUserAddress.$get({
      })
      loadingStore.LoadingChange(false)
      if (res.response){
        this.uUDSSelectUserAddressEntity = res.response 
      }
    },
   async UpdateAddress() {
        const validation: any = await this.fields.validate()
        if (validation.valid === false) return false
        const apiServer = useApiClient()
        const formMessage = useFormMessageStore()
        const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
        const loadingStore = useLoadingStore()
        loadingStore.LoadingChange(true)
        const res = await apiServer.api.v1.UDSUpdateUserAddress.$put({
          body: {
            isOnlyValidation: false,
            addressId: apiFieldValues.addressId,
            addressLine: apiFieldValues.addressLine,
            ward: apiFieldValues.ward,
            city: apiFieldValues.city ,
            province: apiFieldValues.province,
            district:apiFieldValues.district,
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
        if (!this.fields || typeof this.fields.validate !== 'function') {
          console.error('fields.validate is not a function:', this.fields);
          return false;
        }
      
        const validation: any = await this.fields.validate();
        if (!validation.valid) return false;
      
        const apiServer = useApiClient();
        const formMessage = useFormMessageStore();
        const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize);
        const loadingStore = useLoadingStore();
        loadingStore.LoadingChange(true);
      
        const res = await apiServer.api.v1.UDSInsertUserAddress.$post({
          body: {
            isOnlyValidation: false,
            addressLine: apiFieldValues.addressLine,
            ward: apiFieldValues.ward,
            city: apiFieldValues.city,
            province: apiFieldValues.province,
            district: apiFieldValues.district,
          },
        });
      
        loadingStore.LoadingChange(false);
      
        if (!res.success) {
          formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
          return false;
        }
      
        formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
        return true;
      },
      
      async RemoveAddress(){
        const validation: any = await this.fields.validate()
        if (validation.valid === false) return false
        const apiServer = useApiClient()
        const formMessage = useFormMessageStore()
        const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
        const loadingStore = useLoadingStore()
        loadingStore.LoadingChange(true)
        const res = await apiServer.api.v1.UDSDeleteUserAddress.$patch({
          body: {
            isOnlyValidation: false,
            addressId: apiFieldValues.addressId,
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
