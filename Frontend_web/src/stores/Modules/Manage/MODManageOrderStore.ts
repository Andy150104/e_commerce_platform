import { defineStore } from 'pinia'
import { veeValidateStateInitialize } from '@PKG_SRC/utils/StoreFunction'
import { useLoadingStore } from '../usercontrol/loadingStore'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { DashboardEntity, DPSSelectCartItemEntity } from '@PKG_SRC/composables/Client/api/@types'
import { useFormMessageStore } from '@PKG_SRC/stores/master/formMessageStore'
import type { AbstractApiResponseOfString } from '@PKG_SRC/composables/auth/define/@types'

export type FormSchema = Record<string, string>

export const fieldsInitialize: FormSchema = {
  Date: getCurrentDateDDMMYYYY(),
  sortBy: '0'
}

const errorFieldsInitialize: FormSchema = { ...fieldsInitialize }

const fields = {
  values: fieldsInitialize,
  errors: errorFieldsInitialize,
  ...veeValidateStateInitialize,
}

export type CartState = {
  fields: typeof fields
  DashBoardEntity: DashboardEntity
}

export const useMODManageOrderStore = defineStore('MOD', {
  state: (): CartState => ({
    fields,
    DashBoardEntity: {},
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
      this.DashBoardEntity = {}
    },
    async GetDashBoardManageOrder() {
      const apiClient = useApiClient()
      const loadingStore = useLoadingStore()
      const formMessage = useFormMessageStore()
      const apiFieldValues = ConvertCastValue(this.fields.values, fieldsInitialize)
      const [dd, mm, yyyy] = apiFieldValues.Date.split('-')
      const monthNum = parseInt(mm, 10)
      const yearNum = parseInt(yyyy, 10)
      const dateYYYYMMDD = `${yyyy}-${mm}-${dd}`
      loadingStore.LoadingChange(true)
      const res = await apiClient.api.v1.Dashboard.$get({
        query: {
          Date: dateYYYYMMDD,
          Month: monthNum,
          Year: yearNum,
        },
      })
      loadingStore.LoadingChange(false)
      if (!res.success){
        formMessage.SetFormMessageNotApiRes('E00001', true, res.message ?? '')
        return false
      }
      this.DashBoardEntity = res.response ?? {}
      return true
    },
  },
})
