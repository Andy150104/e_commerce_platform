import type { AbstractApiResponseOfString } from '@PKG_API/@types'
import { SearchService } from '@PKG_SRC/types/enums/constantBackend'
import { defineStore } from 'pinia'

export const useSearchStore = defineStore('search', {
  state: () => ({
    searchParams: '',
    searchService: 1,
  }),
  getters: {},
  actions: {
    ResetStore() {

    },
  },
})
