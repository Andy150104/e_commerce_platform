import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import { useCartStore } from '../Blind_Box/CartStore'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import type { HSShowPlanEntity } from '@PKG_SRC/composables/Client/api/@types'

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

export type PlanState = {
  fields: typeof fields
  planItems: HSShowPlanEntity[],
  paymentUrl: string,
}

export const usePlanStore = defineStore('PlanStore', {
  state: (): PlanState => ({
    fields,
    planItems: [],
    paymentUrl: ''
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
      this.planItems = []
      this.paymentUrl = ''
    },
    async BuyPlan(planId: string) {
      const apiClient = useApiClient()
      const cartStore = useCartStore()
      const loadingStore = useLoadingStore()
      const formMessageStore = useFormMessageStore()
      const res = await apiClient.api.v1.OPSBuyingPlan.$post({
        body: {
          planId: planId,
        },
      })
      if (!res.success) return false
      formMessageStore.SetFormMessage(res, true)
      this.paymentUrl = res.response ??  ''
      return true
    },
    async GetAllPlan() {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.HSShowPlan.$get()
      if (res.response) this.planItems = res.response
      loadingStore.LoadingChange(false)
      if (!res.success) return false
      return true
    },
  },
})
