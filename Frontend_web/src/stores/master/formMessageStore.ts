import type { AbstractApiResponseOfString } from '@PKG_SRC/composables/Client/api/@types'
import { defineStore } from 'pinia'

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
    actionName: 'Retry',
    actionCallBack: null as (() => void) | null,
  }),
  getters: {},
  actions: {
    ResetStore() {
      this.messageId = ''
      this.formInfo = ''
      this.isAction = false
      this.isNotify = false
      this.actionCallBack = null
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
        case 'E':
          this.SetMessageShow(result.message ?? '')
          break
      }
    },
    SetFormMessageNotApiRes(messageId: string | undefined, isNotify: boolean, message: string) {
      this.ResetStore()
      this.isNotify = isNotify
      this.messageId = messageId ?? ''
      if (messageId === 'I99999') {
        return
      }
      switch (this.messageId.charAt(0)) {
        case 'I':
          this.SetMessageShow(message ?? '')
          break
        case 'E':
          this.SetMessageShow(message ?? '')
          break
      }
    },
  },
})
