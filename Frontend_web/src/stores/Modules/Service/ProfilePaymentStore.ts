import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { DPSSelectCartItem, DPSSelectCartItemEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useCartStore } from '../Blind_Box/CartStore'

export type FormSchema = Record<string, string>

export const fieldsInitialize: FormSchema = {
  quantity: '1',
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
  paymentUrl: string,
}

export const useProfilePaymentStore = defineStore('Payment', {
  state: (): CartState => ({
    fields,
    address: {},
    paymentUrl: ''
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
    async InsertOrder() {
        const apiClient = useApiClient()
        const cartStore = useCartStore()
        const loadingStore = useLoadingStore()
        
        const orderDetails = cartStore.cartList.map((item) => ({
          accessoryId: item.accessoryId ?? '',
          quantity: Number(item.quantity)
        }))
        
        const res = await apiClient.api.v1.InsertOrder.$post({
          body: {
            addressId: this.address.addressId,
            paymentMethod: 1,
            orderDetails: orderDetails,
          },
        })
        if (!res.success) return false
        this.paymentUrl = res.response?.paymentUrl ?? ''
        return true
      },
  },
})
