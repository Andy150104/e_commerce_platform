import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import type { DPSSelectItemEntity, ItemEntity } from '@PKG_API/@types'

export const fieldsInitialize = {
  dantaiBangou: '',
  ryokouBangou: '',
  ninsyoKey: '',
  mailAddress: '',
  mailAddressConfirm: '',
  seinengappi: '',
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


export type Mypg020MailState = {
  fields: typeof fields
  produtList: ItemEntity[]
  imageList: ImageListResponse
}

export const useDisplayProductStore = defineStore('Product', {
  state: (): Mypg020MailState => ({
    fields,
    produtList: [],
    imageList: {} as ImageListResponse,
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
    async GetProductList(searchServiceNum: number, maxPrice: number, minPrice: number, sortBy: number, pageSize: number) {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.DPSSelectItem.$post({
        body: {
          isOnlyValidation: false,
          currentPage: 1,
          pageSize: pageSize,
          searchBy: searchServiceNum,
          childCategoryName: "",
          maximumPrice: maxPrice,
          minimumPrice: minPrice,
          parentCategoryName: "",
          sortBy: sortBy
        },
      })
      loadingStore.LoadingChange(false)
      if (!res.success) return false
      this.produtList = res.response.items
      return true
    },
  },
})
