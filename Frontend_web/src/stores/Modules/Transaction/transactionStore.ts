import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { DPSSelectCartItem, DPSSelectCartItemEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useCartStore } from '../Blind_Box/CartStore'

export type FormSchema = Record<string, string>

export const fieldsInitialize: FormSchema = {
}

const errorFieldsInitialize: FormSchema = { ...fieldsInitialize }

// VeeValidate fields
const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type CartState = {
  fields: typeof fields
  address: any
  orderId: string,
  GhnOrderCode: string,
}

export const useTransactionStore = defineStore('Transaction', {
  state: (): CartState => ({
    fields,
    address: {},
    orderId: '',
    GhnOrderCode: ''
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
    async updateOrder() {
        const apiClient = useApiClient()
        const res = await apiClient.api.v1.MomoOrderLogicReturn.$patch({
          body: {
            ghnOrderCode: this.GhnOrderCode,
            orderId: this.orderId
          },
        })
        if (!res.success) return false
        return true
      },
  },
})
