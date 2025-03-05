import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import type { DPSSelectCartItemEntity, DPSSelectItemEntity, ItemEntity, MPSSelectAccessoriesEntity } from '@PKG_API/@types'

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
  accessoriesList: MPSSelectAccessoriesEntity[]
}

export const useMPSProductStore = defineStore('MPS', {
  state: (): CartState => ({
    fields,
    accessoriesList: [],
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
    async GetAllProduct() {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const res = await apiClient.api.v1.MPSSelectAccessories.$post({
        body: {
          isOnlyValidation: false,
        },
      })
      if (!res.success) return false
      this.accessoriesList = res.response ?? []
      return true
    },
  },
})
