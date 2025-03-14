import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { DPSSelectCartItem, DPSSelectCartItemEntity } from '@PKG_SRC/composables/Client/api/@types'

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

export type ImageList = {
  response: ImageListResponse
}

export type ImageListResponse = {
  imagesList: string[]
}

export type CartState = {
  fields: typeof fields
  cartList: DPSSelectCartItem[]
  totalCartPrice: Number
}

export const useCartStore = defineStore('Cart', {
  state: (): CartState => ({
    fields,
    cartList: [],
    totalCartPrice: 0,
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
    async GetAllCart() {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const res = await apiClient.api.v1.DPSSelectCartItem.$get()
      if (!res.success) return false
      this.cartList = res.response?.items ?? []
      this.totalCartPrice = res.response?.totalPrice ?? 0
      return true
    },
    async UpdateCart(index: number, accessoryId: string) {
      const key = `quantity_${index}`
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const res = await apiClient.api.v1.DPSUpdateCartItem.$patch({
        body: {
          accessoryId: accessoryId,
          quantity: Number(this.fieldValues[key]),
        },
      })
      if (!res.success) return false
      return true
    },
    async DeleteCart(index: number, productId: string) {
      const key = `quantity_${index}`
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const res = await apiClient.api.v1.DPSDeleteCartItem.$patch({
        body: {
          codeAccessory: productId,
        },
      })
      if (!res.success) return false
      await this.GetAllCart()
      return true
    },
  },
})
