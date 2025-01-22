import { defineStore } from 'pinia'

export const useLoadingStore = defineStore('loading', {
  state: () => ({
    showLoading: 0,
    showLoadingBackground: false,
  }),
  getters: {},
  actions: {
    ResetStore() {
      this.showLoading = 0
      this.showLoadingBackground = false
    },
    LoadingChange(value: boolean) {
      let changeValue = this.showLoading + (value ? 1 : -1)
      if (changeValue < 0) changeValue = 0
      this.showLoading = changeValue
    },
    SetShowLoadingBackground(value: boolean) {
      this.showLoadingBackground = value
    },
  },
})
