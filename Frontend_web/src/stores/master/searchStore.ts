import { defineStore } from 'pinia'

export const useSearchStore = defineStore('search', {
  state: () => ({
    searchParams: '',
    searchService: 1,
  }),
  getters: {},
  actions: {
    ResetStore() {},
  },
})
