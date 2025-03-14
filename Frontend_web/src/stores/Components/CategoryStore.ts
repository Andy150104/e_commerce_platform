import { defineStore } from 'pinia'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { Category } from '@PKG_SRC/types/models/frontendType'
import { useFormMessageStore } from '../master/formMessageStore'

export type CatogryState = {
  parentId: string
  childId: string
  chooseCategoryId: string
  ParentCategoryList: Category[]
  ChildCategoryList: Category[]
}

export const useCatogryStore = defineStore('category', {
  state: (): CatogryState => ({
    parentId: '',
    childId: '',
    chooseCategoryId: '',
    ParentCategoryList: [],
    ChildCategoryList: [],
  }),
  // state

  getters: {},
  actions: {
    ResetCategory() {
      this.parentId = ''
      this.childId = ''
      this.chooseCategoryId = ''
      this.ParentCategoryList = []
      this.ChildCategoryList = []
    },
    async GetParentCategory() {
      const apiClient = useApiClient()
      const res = await apiClient.api.v1.SelectCategories.$get()
      if (!res.success) return
      return (this.ParentCategoryList =
        res.response
          ?.filter((parent) => parent.parentId === null)
          .map((parent) => ({
            name: parent.categoryName || '',
            code: parent.categoryId || '',
          })) ?? [])
    },
    async GetChildCategory() {
      const apiClient = useApiClient()
      const res = await apiClient.api.v1.SelectSubCategories.$get({
        query: {
          ParentCategoryId: this.parentId,
        },
      })
      if (!res.success) return
      if (res.response) {
        return (this.ChildCategoryList = res.response.map((child) => ({
          name: child.categoryName || '',
          code: child.categoryId || '',
        })))
      }
    },
    async CheckCategory() {
      const apiClient = useApiClient()
      const formMessageStore = useFormMessageStore()
      const res = await apiClient.api.v1.SelectSubCategories.$get({
        query: {
          ParentCategoryId: this.parentId,
        },
      })
      if (!res.success && res.messageId === 'E10000'){
        formMessageStore.SetFormMessageNotApiRes('E10000', true, res.message ?? '')
        return false
      }
      if (res.response && this.childId === '') {
          formMessageStore.SetFormMessageNotApiRes('E0000', true, 'Please choose child category')
          return false
      }
      if (!res.response && !res.success) {
        this.chooseCategoryId = this.parentId
        return true
      }
      this.chooseCategoryId = this.childId
      return true
    },
  },
})
