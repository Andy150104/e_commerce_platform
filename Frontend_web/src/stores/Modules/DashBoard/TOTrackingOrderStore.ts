import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { AbstractApiResponseOfString, DashboardEntity, DPSSelectCartItemEntity, GhnOrderData, RROSelectRefundRequestOrdersEntity, SelectOrderEntity } from '@PKG_SRC/composables/Client/api/@types'
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

export type TOTrackingOrderStoreState = {
  fields: typeof fields
  ghnOrderData: GhnOrderData
}

export const useTOTrackingOrderStore = defineStore('useCRCustomerRefundStore', {
  state: (): TOTrackingOrderStoreState => ({
    fields,
    ghnOrderData: {},
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
      this.ghnOrderData = {}
    },
    async GetTrackingOrder() {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.TrackingGhnOrder.$post({
        body:{
          orderId: '2dcac75b-b243-4c16-b205-cc7bee645317'
        }
      })
      loadingStore.LoadingChange(false)
      if (!res.success){
        formMessage.SetFormMessageNotApiRes('E00001', true, res.message ?? '')
        return false
      }
      this.ghnOrderData = res.response?.data ?? {}
      return true
    },
  },
})
