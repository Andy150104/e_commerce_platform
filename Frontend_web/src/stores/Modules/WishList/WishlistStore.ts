import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { DPSSelectCartItem, DPSSelectCartItemEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useCartStore } from '../Blind_Box/CartStore'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'

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
}

export const useWishListStore = defineStore('Wishlist', {
  state: (): CartState => ({
    fields,
    address: {},
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
    async AddToWishList(codeAccessory: string) {
      const apiClient = useApiClient()
      const cartStore = useCartStore()
      const loadingStore = useLoadingStore()
      const formMessageStore = useFormMessageStore()
      const res = await apiClient.api.v1.DPSInsertWishList.$post({
        body: {
          codeAccessory: codeAccessory,
        },
      })
      if (!res.success) return false
      formMessageStore.SetFormMessage(res, true)
      return true
    },
    async RemoveItemOutWishlist(codeAccessory: string) {
      const apiClient = useApiClient()
      const cartStore = useCartStore()
      const loadingStore = useLoadingStore()
      const formMessageStore = useFormMessageStore()
      const res = await apiClient.api.v1.DPSDeleteWishList.$patch({
        body: {
          codeAccessory: codeAccessory,
        },
      })
      if (!res.success) return false
      formMessageStore.SetFormMessage(res, true)
      return true
    },
  },
})
