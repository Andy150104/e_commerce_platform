import { defineStore } from 'pinia'
import { useApiClient } from '@PKG_SRC/composables/Client/apiClient'
import type { Category } from '@PKG_SRC/types/models/frontendType'

export type CatogryState = {
  parentId: string
  ParentCategoryList: Category[]
  ChildCategoryList: Category[]
}

export const useCatogryStore = defineStore('category', {
  state: (): CatogryState => ({
    parentId: '',
    ParentCategoryList: [],
    ChildCategoryList: [],
  }),
  // state

  getters: {},
  actions: {
    ResetCategory() {
      this.ChildCategoryList = []
    },
    async GetParentCategory() {
      const apiClient = useApiClient()
      const res = await apiClient.api.v1.SelectCategories.$get()
      if (!res.success) return
        return this.ParentCategoryList = res.response
          ?.filter((parent) => parent.parentId === null)
          .map((parent) => ({
            name: parent.categoryName || '',
            code: parent.categoryId || '',
          })) ?? [] 
      
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
  },
})
