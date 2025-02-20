import type { AbstractApiResponseOfString, DetailError } from '@PKG_API/@types'
import { defineStore } from 'pinia'

/** 認証 */
export const useFormMessageStore = defineStore('formMessage', {
  state: () => ({
    messageId: '',
    message: '',
    formInfo: '',
    formWarm: '',
    formError: '',
    errorList: [] as string[],
    warmListTitle: [] as string[],
    warmList: [] as string[],
    isAction: false,
    isNotify: false,
  }),
  getters: {},
  actions: {
    ResetStore() {
      this.messageId = ''
      this.formInfo = ''
      this.isAction = false,
      this.isNotify = false
    },
    SetMessageId(message: string) {
      this.messageId = message
    },
    SetMessageShow(message: string) {
      this.isNotify = true
      this.message = message
    },
    SetFormMessage(result: AbstractApiResponseOfString | undefined, isNotify: boolean) {
      this.ResetStore()
      this.isNotify = isNotify
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
          this.SetMessageShow(result.message ?? '')
          break
        case 'W':
          this.SetMessageShow(result.message ?? '')
          break
        case 'E':
          this.SetMessageShow(result.message ?? '')
          break
      }
    },
  },
})
