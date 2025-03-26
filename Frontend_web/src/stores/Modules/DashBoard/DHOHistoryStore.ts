import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { AbstractApiResponseOfString, DashboardEntity, DPSSelectCartItemEntity, SelectOrderEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { useUploadImageStore } from '../usercontrol/uploadImageStore'

export type FormSchema = Record<string, string>

export const fieldsInitialize: FormSchema = {
  refundReason: ''
}

const errorFieldsInitialize: FormSchema = { ...fieldsInitialize }

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type DHOHistoryStoreState = {
  fields: typeof fields
  DOHSelectHistoryOrder: SelectOrderEntity[]
}

export const useDHOHistoryStore = defineStore('DHOHistoryStore', {
  state: (): DHOHistoryStoreState => ({
    fields,
    DOHSelectHistoryOrder: [],
  }),

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
      this.DOHSelectHistoryOrder = []
    },
    async GetDashHistoryOrder() {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.SelectOrders.$get()
      loadingStore.LoadingChange(false)
      if (!res.success){
        formMessage.SetFormMessageNotApiRes('E00001', true, res.message ?? '')
        return false
      }
      this.DOHSelectHistoryOrder = res.response ?? []
      return true
    },
    async RefundMoney(orderId: string, reason: string) {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore()
      const uploadStore = useUploadImageStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.RROInsertRefundRequestOrder.$post({
        body:{
          orderId: orderId,
          imageUrl: uploadStore.image,
          refundReason: reason
        }
      })
      loadingStore.LoadingChange(false)
      if (!res.success){
        formMessage.SetFormMessageNotApiRes('E00001', true, res.message ?? '')
        return false
      }
      formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
      return true
    },
    async RePayment(orderId: string) {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore()
      const uploadStore = useUploadImageStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.PaymentOrderCallback.$post({
        body:{
          orderId: orderId,
          platform: 1,
        }
      })
      loadingStore.LoadingChange(false)
      if (!res.success){
        formMessage.SetFormMessageNotApiRes('E00001', true, res.message ?? '')
        return false
      }
      formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
      return res.response?.paymentUrl
    },
  },
})
