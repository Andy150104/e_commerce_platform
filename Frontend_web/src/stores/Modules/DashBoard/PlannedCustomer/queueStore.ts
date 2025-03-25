import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import type { AbstractApiResponseOfString, MPSSelectAccessoriesEntity, VEXSGetToQueueResponseEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useCatogryStore } from '@PKG_SRC/stores/Components/CategoryStore'
import { useLoadingStore } from '../../usercontrol/loadingStore'
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

export type QueueItem = {
  queueId: string
  userFullNameQueue: string
  userImage: string
  descriptionQueue: string
  userGender: 'male' | 'female' | 'other'
  userPhone: string
  userBirthday: string
}

export type MPSProductState = {
  fields: typeof fields
  queueListDetail: VEXSGetToQueueResponseEntity[]
}

export const useQueueStore = defineStore('queueStore', {
  state: (): MPSProductState => ({
    fields,
    queueListDetail: [] as VEXSGetToQueueResponseEntity[],
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
      this.queueListDetail = []
    },
    async ApproveQueue(exchangeId: string,queueId:string) {
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore();
      loadingStore.LoadingChange(true)

      const apiClient = useApiClient()
      const res = await apiClient.api.v1.VEXSApproveQueue.$post({
        body: {
          exchangeId: exchangeId,
          queueId:queueId,
        },
      })
      loadingStore.LoadingChange(false)
       if (!res.success) {
             formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
             return false;
           }
         
           formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
           return true;
    },
    async FinaleApproveQueue(exchangeId: string,queueId:string, isAccepted: boolean) {
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore();
      loadingStore.LoadingChange(true)

      const apiClient = useApiClient()
      const res = await apiClient.api.v1.AEPSFinalAcceptExchangeAccessory.$post({
        body: {
          exchangeId: exchangeId,
          queueId:queueId,
          isAccepted:isAccepted
        },
      })
      loadingStore.LoadingChange(false)
       if (!res.success) {
             formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
             return false;
           }
         
           formMessage.SetFormMessage(res as AbstractApiResponseOfString, true);
           return true;
    },
    async GetQueueById(exchangeId: string) {
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)

      const apiClient = useApiClient()
      const res = await apiClient.api.v1.VEXSGetToQueue.$get({
        query: {
          ExchangeId: exchangeId,
        },
      })
      if (res.success) {
        this.queueListDetail = res.response ?? []
      } else {
        console.error(`Không thể lấy dữ liệu cho ExchangeId: ${exchangeId}`)
      }
      loadingStore.LoadingChange(false)
      return res.success
    },
  },
})
