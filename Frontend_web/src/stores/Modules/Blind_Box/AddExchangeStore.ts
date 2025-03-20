import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useApiServer } from '@PKG_SRC/composables/auth/authHttp'
import type { AbstractApiResponseOfString } from '@PKG_SRC/composables/auth/define/@types'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import { useUploadImageStore } from '../usercontrol/uploadImageStore'

export const fieldsInitialize = {
  name: '',
  description:'',
  price: 1,

}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = createErrorFields(fieldsInitialize)

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type AddExchangeState = {
  fields: typeof fields
  createFlgAddPass: boolean
}

export const useAddExchangeStore = defineStore('AddExchange', {
  state: (): AddExchangeState => ({
    fields,
    createFlgAddPass: false,
  }),
  getters: {
    fieldValues: (state) => {
      return state.fields.values
    },
    fieldValid: (state) => {
      return state.fields.meta.valid
    },
    fieldErrors: (state) => {
      return state.fields.errors
    },
  },
  actions: {
    ResetStore() {
      this.fields.resetForm()
    },
    SetFields(value: any) {
      this.fields = value
    },
    async Validate() {
      await this.fields.validate()
      return this.fieldValid
    },
    async addExchangeProduct() {
      const validation: any = await this.fields.validate();
      if (!validation.valid) return false;
    
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize);
    
      // Lấy danh sách ảnh từ store
      const uploadImageStore = useUploadImageStore();

      const apiClient = useApiClient();
      const formMessage = useFormMessageStore();
      const loadingStore = useLoadingStore();
    
      loadingStore.LoadingChange(true);
      const images: File[] = uploadImageStore.uploadImage.map((image, index) =>
        base64ToFile(image.imagePreview, `image_${index + 1}.png`)
      )
      const res = await apiClient.api.v1.AEPSAddExchangeAccessory.$get({
        query: {
          Name: apiFieldValues.name,
          Description: apiFieldValues.description,
        },
        body:{
          Images:images
        },
      });
    
      loadingStore.LoadingChange(false);
    console.log("api gonna call this:"+ apiFieldValues.name)
    console.log("api gonna call this:"+ apiFieldValues.description)
    console.log("api gonna call this:"+ apiFieldValues.price)
    console.log("api gonna call this:"+ images)
      if (!res.success) {
        formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
        return false;
      }
    
      formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
      return true;
    }    
  },
})
