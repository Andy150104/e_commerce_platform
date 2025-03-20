import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { AEPSGetByIdExchangeAccessoryEntity } from '@PKG_SRC/composables/Client/api/@types'

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

export type ExchangeState = {
  fields: typeof fields;
  exchangeDetails: AEPSGetByIdExchangeAccessoryEntity | null;
  exchangeDetailsMap: Record<string, AEPSGetByIdExchangeAccessoryEntity>;
  totalCartPrice: number;
  isPaymentFlag: boolean;
};

export const useExchangeStore = defineStore("Exchange", {
  state: (): ExchangeState => ({
    fields,
    exchangeDetails: null,
    exchangeDetailsMap: {},
    totalCartPrice: 0,
    isPaymentFlag: false,
  }),

  getters: {
    fieldValues: (state) => state.fields.values,
    fieldErrors: (state) => state.fields.errors,
    imageList: (state) => {
      return state.exchangeDetails?.imageBlindBoxList?.map(image => image.imageUrls) || [];
    },
  },

  actions: {
    SetFields(value: any) {
      this.fields = value;
    },
    ResetStore() {
      this.fields.resetForm();
    },
    async GetByExchangeID(exchangeId: string) {
      if (this.exchangeDetails?.exchangeId === exchangeId) {
        console.log(`Exchange ${exchangeId} đã có trong store, không cần gọi API.`);
        return true;
      }

      const apiClient = useApiClient();
      const loadingStore = useLoadingStore();
      loadingStore.LoadingChange(true);

      const res = await apiClient.api.v1.AEPSGetByIdExchangeAccessory.$get({
        query: { ExchangeId: exchangeId },
      });

      if (res.success && res.response) {
        this.exchangeDetails = res.response; 
        this.exchangeDetailsMap[exchangeId] = res.response;
      } else {
        console.error(`Không thể lấy dữ liệu cho ExchangeId: ${exchangeId}`);
      }

      loadingStore.LoadingChange(false);
      return res.success;
    },
    async FetchAllExchangeDetails(exchangeIds: string[]) {
      for (const id of exchangeIds) {
        await this.GetByExchangeID(id);
      }
    },
    
  },
});
