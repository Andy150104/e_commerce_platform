import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { AbstractApiResponseOfString, UDSSelectUserProfileEntity, VEXSAddToQueueRequest } from '@PKG_SRC/composables/Client/api/@types'
import ExchangeId from '@PKG_SRC/pages/DashBoard/PlannedCustomer/Exchange-[id].vue'

export const fieldsInitialize = {
    ExchangeId:'',
    description:''
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = createErrorFields(fieldsInitialize)

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type RequestState = {
  fields: typeof fields
  uVEXSAddToQueueRequest: VEXSAddToQueueRequest
}

export const useRequestStore = defineStore('RquestQueue', {
  state: (): RequestState => ({
    fields,
    uVEXSAddToQueueRequest: {} as VEXSAddToQueueRequest,
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
    async AddToQueue() {
    
      // Kiểm tra validation
      const validation: any = await this.fields.validate();
    
      if (validation.valid === false) {
        return false;
      }
    
      const apiServer = useApiClient();
      const formMessage = useFormMessageStore();
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize);
    
      // Bắt đầu loading
      const loadingStore = useLoadingStore();
      loadingStore.LoadingChange(true);
    
      try {
        const res = await apiServer.api.v1.VEXSAddToQueue.$post({
          body: {
            exchangeId: apiFieldValues.ExchangeId,
            description: apiFieldValues.description,
          },
        });
    
    
        // Tắt loading
        loadingStore.LoadingChange(false);
    
        if (!res.success) {
          formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
          return false;
        }
    
        formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
        return true;
      } catch (error) {
        loadingStore.LoadingChange(false);
        return false;
      }
    }
    
  },
})
