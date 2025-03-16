import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { useUploadImageStore } from '../usercontrol/uploadImageStore'
import type { AbstractApiResponseOfString, MPSSelectAccessoriesEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useCatogryStore } from '@PKG_SRC/stores/Components/CategoryStore'
export type FormSchema = Record<string, string>

export const fieldsInitialize: FormSchema = {
  quantity: '1',
  description: '',
  name: '',
  price: '',
  discount: '',
  shortDescription: '',
  parentCategory: '',
  childCategory: '',
  quantityUpdate: '1',
  descriptionUpdate: '',
  nameUpdate: '',
  priceUpdate: '',
  discountUpdate: '',
  shortDescriptionUpdate: '',
  parentCategoryUpdate: '',
  childCategoryUpdate: '',
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

export type MPSProductState = {
  fields: typeof fields
  accessoriesList: MPSSelectAccessoriesEntity[]
}

export const useMPSProductStore = defineStore('MPS', {
  state: (): MPSProductState => ({
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
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.MPSSelectAccessories.$get({})
      if (!res.success) return false
      loadingStore.LoadingChange(false)
      this.accessoriesList = res.response ?? []
      return true
    },
    async DeleteOneProduct(id: string) {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const formMessage = useFormMessageStore()
      const res = await apiClient.api.v1.MPSDeleteAccessory.$patch({
        body: {
          codeAccessory: [id],
        },
      })
      loadingStore.LoadingChange(false)
      if (!res.success) {
        formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
        return false
      }
      formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
      return true
    },
    async AddProduct(description: string) {
      const categoryStore = useCatogryStore()
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore()
      const uploadStore = useUploadImageStore()
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const images: File[] = uploadStore.uploadImage.map((image, index) =>
        base64ToFile(image.imagePreview, `image_${index + 1}.png`)
      )
      console.log('Discount', apiFieldValues.discount)
      console.log('Name', apiFieldValues.name)
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.MPSInsertAccessory.$post({
        body: {
          Images: images,
        },
        query: {
          CategoryId: await categoryStore.CheckCategory() ? categoryStore.chooseCategoryId : '',
          Description: description,
          Discount: Number(apiFieldValues.discount),
          Name: apiFieldValues.name,
          Price: Number(apiFieldValues.price),
          Quantity: Number(apiFieldValues.quantity),
          ShortDescription: apiFieldValues.shortDescription,
        },
      })
      categoryStore.ResetCategory()
      loadingStore.LoadingChange(false)
      if (!res.success) {
        formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
        return false
      }
      uploadStore.ResetStore()
      formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
      return true
    },
    async UpdateProduct(description: string, id: string, imageIds: string[]) {
      const categoryStore = useCatogryStore()
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const formMessage = useFormMessageStore()
      const uploadStore = useUploadImageStore()
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const images: File[] = uploadStore.uploadImage.map((image, index) =>
        base64ToFile(image.imagePreview, `image_${index + 1}.png`)
      )
    
      const res = await apiClient.api.v1.MPSUpdateAccessory.$put({
        body:{
          Images: images
        },
        query: {
          CodeAccessory: id,
          CategoryId: await categoryStore.CheckCategory() ? categoryStore.chooseCategoryId : '',
          Description: description,
          Discount: Number(apiFieldValues.discount),
          Name: apiFieldValues.name,
          Price: Number(apiFieldValues.price),
          Quantity: Number(apiFieldValues.quantity),
          ShortDescription: apiFieldValues.shortDescription,
          ImageId: imageIds
        },
      })
      loadingStore.LoadingChange(false)
      if (!res.success) {
        formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
        return false
      }
      uploadStore.ResetStore()
      formMessage.SetFormMessage(res as AbstractApiResponseOfString, true)
      return true
    },
  },
})
