import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import { ConvertCastValue, createErrorFields } from '@PKG_SRC/utils/commonFunction'
import { defineStore } from 'pinia'
import { useLoadingStore } from '../../usercontrol/loadingStore'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { AbstractApiResponseOfString, AEPSGetExchangeAccessoryEntity, UDSSelectUserProfileEntity } from '@PKG_SRC/composables/Client/api/@types'

export const fieldsInitialize = {
    exchangeId: '',
    exchangeName: '',
    description: '',
    status: '',
    blindBoxId: '',
}
export type FormSchema = typeof fieldsInitialize

const errorFieldsInitialize = createErrorFields(fieldsInitialize)

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type MyExchangeState = {
  fields: typeof fields
  uAEPSGetExchangeAccessoryEntities: AEPSGetExchangeAccessoryEntity[]
}

export const useMyExchangeStore = defineStore('MyExchange', {
  state: (): MyExchangeState => ({
    fields,
    uAEPSGetExchangeAccessoryEntities: []
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
    async GetMyExchange() {
        const loadingStore = useLoadingStore()
        loadingStore.LoadingChange(true)
      
        const apiClient = useApiClient()
        const res = await apiClient.api.v1.AEPSGetExchangeAccessory.$get({})
      
        // Lưu toàn bộ danh sách từ API
        this.uAEPSGetExchangeAccessoryEntities = res.response ?? []
      
        // Nếu muốn set fields với dữ liệu đầu tiên
        if (this.uAEPSGetExchangeAccessoryEntities.length > 0) {
          const firstItem = this.uAEPSGetExchangeAccessoryEntities[0]
      
          Object.keys(firstItem).forEach((key) => {
            if (this.fields.values.hasOwnProperty(key)) {
              this.fields.setFieldValue(key, firstItem[key as keyof AEPSGetExchangeAccessoryEntity])
            }
          })
        }
      
        loadingStore.LoadingChange(false)
      }
      
  },
})
