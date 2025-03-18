import { defineStore } from 'pinia'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type {
  AbstractApiResponseOfString,
  AEPSGetExchangeRecheckRequestAccessoryEntity,
} from '@PKG_SRC/composables/Client/api/@types'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'

export const fieldsInitialize = {
  requestId: '',
  exchangeId: '',
  description: '',
  status: 1,
  isActive: true,
  createdAt: '',
  createdBy: '',
  updatedBy: '',
  updatedAt: '',
  isAccepted: true,
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = createErrorFields(fieldsInitialize)

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type ExchangeState = {
  fields: typeof fields
  uAEPSGetExchangeRecheckRequestAccessoryEntity: AEPSGetExchangeRecheckRequestAccessoryEntity[]
}

export const useDBExchangeStore = defineStore('DBExchange', {
  state: (): ExchangeState => ({
    fields,
    uAEPSGetExchangeRecheckRequestAccessoryEntity: [] as AEPSGetExchangeRecheckRequestAccessoryEntity[],
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
    async GetExchange() {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.AEPSGetExchangeRecheckRequestAccessory.$get({})
      loadingStore.LoadingChange(false)
      if (res.response) {
        this.uAEPSGetExchangeRecheckRequestAccessoryEntity = res.response
      }
    },
    async ManageExchange(requestId: string, isAccepted: boolean) {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.AEPSCheckExchangeRecheckRequestAccessory.$post({
        body: {
          requestId: requestId,
          isAccepted: isAccepted,
        },
      })
      loadingStore.LoadingChange(false)
      if (!res.response) {
        
      const formMessage = useFormMessageStore()
        formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
        return false
      }
      
      const formMessage = useFormMessageStore()
      formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
      return true
    },
  },
})
