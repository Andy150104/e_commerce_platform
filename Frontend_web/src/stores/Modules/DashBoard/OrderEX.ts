import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type {
  AbstractApiResponseOfString,
  UDSSelectUserProfileEntity,
  VEXSGetOrderExchangeResponseEntity,
} from '@PKG_SRC/composables/Client/api/@types'

export const fieldsInitialize = {
  exchangeId: '',
  exchangeName: '',
  created_at: '',
  created_by: '',
  updated_at: '',
  updated_by: '',
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = createErrorFields(fieldsInitialize)

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type OrderState = {
  fields: typeof fields
  OrderHistoryList: VEXSGetOrderExchangeResponseEntity[]
}

export const useOrderEXHSStore = defineStore('OrderEXHS', {
  state: (): OrderState => ({
    fields,
    OrderHistoryList: [] as VEXSGetOrderExchangeResponseEntity[],
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
    async GetOrderHistory() {
      // const validation: any = await this.fields.validate()
      // if (validation.valid === false) return false
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const apiClient = useApiClient()
      const res = await apiClient.api.v1.VEXSGetOrderExchange.$get({})
      if (res.response) {
        this.OrderHistoryList = res.response // Bây giờ là mảng, khớp kiểu
      }
      loadingStore.LoadingChange(false)
    },
  },
  persist: true,
})
