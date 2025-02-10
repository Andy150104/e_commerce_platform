import type { AbstractApiResponseOfString, DetailError } from '@PKG_API/@types'
import { defineStore } from 'pinia'

/** 認証 */
export const useFormMessageStore = defineStore('formMessage', {
  state: () => ({
    messageId: '',
    formInfo: '',
    formWarm: '',
    formError: '',
    errorList: [] as string[],
    warmListTitle: [] as string[],
    warmList: [] as string[],
    isAction: false
  }),
  // stateと同じgetterは宣言不要
  getters: {},
  actions: {
    ResetStore() {
      this.messageId = ''
      this.formInfo = ''
      ;(this.formWarm = ''), (this.formError = ''), (this.errorList = []), (this.warmListTitle = []), (this.warmList = [])
    },
    SetMessageId(message: string) {
      this.messageId = message
    },
    SetFormInfo(message: string) {
      this.formInfo = message
    },
    SetFormWarm(message: string) {
      this.formWarm = message
    },
    SetFormError(message: string) {
      this.formError = message
    },
    SetErrorList(message: string) {
      this.errorList.push(message)
    },
    SetWarmListTitle(message: string) {
      this.warmListTitle.push(message)
    },
    SetWarmList(message: string) {
      this.warmList.push(message)
    },
    SetTableError(detail: { detailError: DetailError; name: string }) {
    },
    SetFormMessage(result: AbstractApiResponseOfString | undefined) {
      this.ResetStore()
      if (result === undefined) return
      if (result.messageId == null) {
        return
      }
      this.messageId = result.messageId
      if (result.messageId === 'I99999') {
        return
      }
      switch (result.messageId.charAt(0)) {
        case 'I':
          this.SetFormInfo(result.message ?? '')
          break
        case 'W':
          this.SetFormWarm(result.message ?? '')
          break
        case 'E':
          this.SetFormError(result.message ?? '')
          break
      }
    },
    SetTableMessage(detail: { detailError: DetailError; name: string }) {
      this.SetTableError(detail)
    },
    SetTableMessageNoLabel(detail: { detailError: DetailError; name: string }) {
      const value = detail.detailError.errorMessage ?? ''
      this.errorList.push(value)
    },
  },
})
