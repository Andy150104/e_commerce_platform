import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import type { DPSSelectAccessoryEntity, DPSSelectItemEntity, ItemEntity } from '@PKG_API/@types'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import type { ImageItemGallery } from '@PKG_SRC/types'

export const fieldsInitialize = {
  quantity: '1',
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

export type DetailProductState = {
  fields: typeof fields
  productDetail: DPSSelectAccessoryEntity
}

export const useDetailProductStore = defineStore('Detail', {
  state: (): DetailProductState => ({
    fields,
    productDetail: {} as DPSSelectAccessoryEntity,
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
    convertImageList(imageUrls: any[]): ImageItemGallery[] {
      const minImages = 5
      const defaultImage =
        'https://media.istockphoto.com/id/1409329028/vector/no-picture-available-placeholder-thumbnail-icon-illustration-design.jpg?s=612x612&w=0&k=20&c=_zOuJu755g2eEUioiOUdz_mHKJQJn-tDgIAhQzyeKUQ='

      // Kiểm tra nếu imageUrls không tồn tại hoặc không phải là mảng, gán giá trị mặc định
      if (!Array.isArray(imageUrls) || imageUrls.length === 0) {
        imageUrls = []
      }

      // Chuyển đổi danh sách ảnh
      let imageList: ImageItemGallery[] = imageUrls.map((image, index) => ({
        src: image.imageUrl,
        alt: `Ảnh ${index + 1}`,
      }))

      // Nếu số lượng ảnh ít hơn 6, thêm ảnh mặc định
      while (imageList.length < minImages) {
        imageList.push({
          src: defaultImage,
          alt: `Ảnh bổ sung ${imageList.length + 1}`,
        })
      }

      return imageList
    },

    async AddProductToCart(codeProduct: string) {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const formMessageStore = useFormMessageStore()
      const res = await apiClient.api.v1.DPSInsertCart.$post({
        body: {
          isOnlyValidation: false,
          codeAccessory: codeProduct,
          quantity: 1,
        },
      })
      loadingStore.LoadingChange(false)
      if (!res.success) return false
      formMessageStore.SetFormMessage(res, true)
      return true
    },
    async GetProductById(codeProduct: string) {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.DPSSelectAccessory.$post({
        body: {
          isOnlyValidation: false,
          codeAccessory: codeProduct,
        },
      })
      loadingStore.LoadingChange(false)
      this.productDetail = res.response ?? {}
      if (!res.success) return false
      return true
    },
  },
})
