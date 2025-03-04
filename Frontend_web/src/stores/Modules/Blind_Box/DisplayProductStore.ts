import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import type { DPSSelectItemEntity, ItemEntity } from '@PKG_API/@types'

export const fieldsInitialize = {
  sortBy: 0,
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = fieldsInitialize

// VeeValidate
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

export type DisplayProductState = {
  fields: typeof fields
  produtList: ItemEntity[]
  totalRecords: Number
  imageList: ImageListResponse
  isLoadingSkeletonCard: boolean
}

export const useDisplayProductStore = defineStore('Product', {
  state: (): DisplayProductState => ({
    fields,
    produtList: [],
    totalRecords: 0,
    imageList: {} as ImageListResponse,
    isLoadingSkeletonCard: false,
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
      this.produtList = []
    },
    async GetProductList(searchServiceNum: number, maxPrice: number, minPrice: number, sortBy: number, pageSize: number) {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      this.isLoadingSkeletonCard = true
      const res = await apiClient.api.v1.DPSSelectItem.$post({
        body: {
          isOnlyValidation: false,
          currentPage: 1,
          pageSize: pageSize,
          searchBy: searchServiceNum,
          childCategoryName: '',
          maximumPrice: maxPrice,
          minimumPrice: minPrice,
          parentCategoryName: '',
          sortBy: sortBy,
        },
      })
      loadingStore.LoadingChange(false)
      this.isLoadingSkeletonCard = false
      if (!res.success) return false
      this.totalRecords = res.response?.totalRecords ?? 0
      if (res.response?.items) this.produtList = res.response.items
      return true
    },
  },
})
