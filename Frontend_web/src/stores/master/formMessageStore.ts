import type { AbstractApiResponseOfString, DetailError } from '@PKG_API/@types'
import { defineStore } from 'pinia'

/** 認証 */
export const useFormMessageStore = defineStore('formMessage', {
  state: () => ({
    messageId: '',
    formInfo: '',
    formWarm: '',
    formError: '',
    // テーブルやcsvなど複数のエラーを出す用
    errorList: [] as string[],
    warmListTitle: [] as string[],
    warmList: [] as string[],
  }),
  // stateと同じgetterは宣言不要
  getters: {},
  actions: {
    ResetStore() {
      this.messageId = ''
      this.formInfo = ''
      ;(this.formWarm = ''), (this.formError = ''), (this.errorList = []), (this.warmListTitle = []), (this.warmList = [])
    },
    //フロントの情報メッセージを出す場合
    SetFormInfo(message: string) {
      this.formInfo = message
    },
    //フロントの警告メッセージを出す場合
    SetFormWarm(message: string) {
      this.formWarm = message
    },
    //フロントのエラーメッセージを出す場合
    SetFormError(message: string) {
      this.formError = message
    },
    //フロントのエラーリストを出す場合
    SetErrorList(message: string) {
      this.errorList.push(message)
    },
    //フロントの警告リストタイトルを出す場合
    SetWarmListTitle(message: string) {
      this.warmListTitle.push(message)
    },
    //フロントの警告リストを出す場合
    SetWarmList(message: string) {
      this.warmList.push(message)
    },
    SetTableError(detail: { detailError: DetailError; name: string }) {
      if (detail.detailError.row != null) {
        if (detail.name != '') {
          //メッセージに追加
          const value = '明細No.' + detail.detailError.row.toString() + ' ' + detail.name + ' ' + detail.detailError.errorMessage
          this.errorList.push(value)
        }
      } else if (detail.name != '') {
        const value = detail.detailError.errorMessage ?? ''
        this.errorList.push(value)
      }
    },
    SetFormMessage(result: AbstractApiResponseOfString | undefined) {
      // メッセージ初期化
      this.ResetStore()
      if (result === undefined) return
      if (result.messageId == null) {
        return
      }
      // MessageIdをセット
      this.messageId = result.messageId
      // メッセージIDがI99999の場合は表示しない
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
