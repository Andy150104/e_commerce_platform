import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import type { DPSSelectCartItemEntity, DPSSelectItemEntity, ItemEntity } from '@PKG_API/@types'

export type FormSchema = Record<string, string>

export const fieldsInitialize: FormSchema = {
  quantity: '1',
}

const errorFieldsInitialize: FormSchema = { ...fieldsInitialize }

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
  cartList: DPSSelectCartItemEntity[]
}

export const useWishListBuyingServiceStore = defineStore('Wishlist', {
  state: (): CartState => ({
    fields,
    cartList: [],
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
    },
    async GetAllCart() {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const res = await apiClient.api.v1.DPSSelectCartItem.$post({
        body: {
          isOnlyValidation: false,
        },
      })
      if (!res.success) return false
      this.cartList = res.response ?? []
      return true
    },
    async AddToWishList(index: number, accessoryId: string) {
      const key = `quantity_${index}`
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const res = await apiClient.api.v1.DPSInsertWishList.$post({
        body: {
          isOnlyValidation: false,
          codeAccessory: accessoryId,
        },
      })
      if (!res.success) return false
      return true
    },
    async DeleteCart(index: number, productId: string) {
      const key = `quantity_${index}`
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const res = await apiClient.api.v1.DPSDeleteCartItem.$post({
        body: {
          isOnlyValidation: false,
          codeAccessory: productId,
        },
      })
      if (!res.success) return false
      await this.GetAllCart()
      return true
    },
  },
})
