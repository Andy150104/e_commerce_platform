import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import type { AbstractApiResponseOfString, AEPSSendExchangeRecheckRequestAccessoryRequest, AEPSGetFailExchangeAccessoryEntity, AEPSGetByIdExchangeAccessoryEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useCatogryStore } from '@PKG_SRC/stores/Components/CategoryStore'
import { useLoadingStore } from '../../usercontrol/loadingStore'
import { useExchangeStore } from '../../Blind_Box/ExchangeStore'
export type FormSchema = Record<string, string>

export const fieldsInitialize: FormSchema = {
    exchangeId: '1',
    exchangeName: '',
    description: '',
    status: '',
    blindBoxId: '',
}

const errorFieldsInitialize: FormSchema = { ...fieldsInitialize }

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type MPSProductState = {
  fields: typeof fields
  failedExchangeList: AEPSGetByIdExchangeAccessoryEntity[]
}

export const useFailedEXStore = defineStore('failedEXStore', {
  state: (): MPSProductState => ({
    fields,
    failedExchangeList: [] as AEPSGetByIdExchangeAccessoryEntity[],
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
    async SendRecheckRequest() {
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore();
      
            const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize);
      loadingStore.LoadingChange(true)

      const apiClient = useApiClient()
      const res = await apiClient.api.v1.AEPSSendExchangeRecheckRequestAccessory.$post({
        body: {
          exchangeId: apiFieldValues.exchangeId,
          description:apiFieldValues.description,
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
    async GetFailedExchangeList() {
        const loadingStore = useLoadingStore();
        loadingStore.LoadingChange(true);
      
        const apiClient = useApiClient();
        const exchangeStore = useExchangeStore(); // Gọi store chứa FetchAllExchangeDetails
      
        const res = await apiClient.api.v1.AEPSGetFailExchangeAccessory.$get({});
      
        if (res.success && res.response) {
          this.failedExchangeList = res.response ?? [];
      
          // Lấy danh sách ExchangeId từ failedExchangeList
          const exchangeIds = this.failedExchangeList
            .map(item => item.exchangeId)
            .filter((id): id is string => !!id);
      
          if (exchangeIds.length > 0) {
            await exchangeStore.FetchAllExchangeDetails(exchangeIds);
            
            // Cập nhật failedExchangeList với thông tin chi tiết từ store Exchange
            this.failedExchangeList = this.failedExchangeList.map(item => ({
                ...item,
                details: item.exchangeId ? exchangeStore.exchangeDetailsMap[item.exchangeId] ?? null : null
              }));              
          }
        } else {
          console.error(`Không thể lấy dữ liệu!`);
        }
      
        loadingStore.LoadingChange(false);
        return res.success;
      }
      
      
  },
})
